using Godot;
using System;

public partial class BarraSuperiorUI : CanvasLayer
{
    private Sistema sistema;
    private Label diaLabel;
    private Label momentoLabel;
    private Label saludLabel;
    private Label ruachLabel;
    private Label xpLabel;
    private Button hojaBtn;

    public BarraSuperiorUI(Sistema sistema)
    {
        this.sistema = sistema;
    }

    public override void _Ready()
    {
        // Contenedor principal para la barra superior
        var panel = new PanelContainer
        {
            AnchorRight = 1.0f,
            SizeFlagsHorizontal = Control.SizeFlags.ExpandFill
        };
        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0.1f, 0.1f, 0.1f, 0.8f),
            BorderWidthBottom = 2,
            BorderColor = new Color(0.5f, 0.5f, 0.5f)
        };
        panel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(panel);

        var hBox = new HBoxContainer
        {
            Alignment = HBoxContainer.AlignmentMode.Center,
            SizeFlagsHorizontal = Control.SizeFlags.ExpandFill
        };
        panel.AddChild(hBox);

        // Crear y añadir etiquetas
        diaLabel = new Label();
        momentoLabel = new Label();
        saludLabel = new Label();
        ruachLabel = new Label();
        xpLabel = new Label();

        hBox.AddChild(new Label { Text = "Día: " });
        hBox.AddChild(diaLabel);
        hBox.AddChild(new VSeparator());
        hBox.AddChild(momentoLabel);
        hBox.AddChild(new VSeparator());
        hBox.AddChild(new Label { Text = "Salud: " });
        hBox.AddChild(saludLabel);
        hBox.AddChild(new VSeparator());
        hBox.AddChild(new Label { Text = "Ruach: " });
        hBox.AddChild(ruachLabel);
        hBox.AddChild(new VSeparator());
        hBox.AddChild(new Label { Text = "XP: " });
        hBox.AddChild(xpLabel);
        hBox.AddChild(new VSeparator());

        // Botón Hoja de Personaje
        hojaBtn = new Button { Text = "Hoja" };
        hojaBtn.Pressed += () => sistema.mostrarHojaDePersonaje();
        hBox.AddChild(hojaBtn);

        ActualizarUI();
    }

    public void ActualizarUI()
    {
        var personaje = sistema.getJugador();
        var dia = sistema.getDiaCargado();

        if (personaje != null && dia != null)
        {
            diaLabel.Text = dia.NumeroDia.ToString();
            momentoLabel.Text = dia.getPeriodoDelDia();
            saludLabel.Text = personaje.Vitalidad.ToString();
            ruachLabel.Text = personaje.Orgon.ToString();
            xpLabel.Text = personaje.XP.ToString();
        }
    }
}
