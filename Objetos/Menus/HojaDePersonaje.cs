using Godot;
using System;

public partial class HojaDePersonaje : Node2D
{
    private Sistema sistema;
    private Personaje personaje;

    public HojaDePersonaje(Sistema sistema)
    {
        this.sistema = sistema;
        this.personaje = sistema.getJugador();
    }

    public override void _Ready()
    {
        // Panel principal
        PanelContainer panel = new PanelContainer();
        panel.SetPosition(new Vector2(100, 100));
        panel.SetSize(new Vector2(1080, 440));
        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0, 0, 0, 0.8f),
            CornerRadiusTopLeft = 10, CornerRadiusTopRight = 10,
            CornerRadiusBottomLeft = 10, CornerRadiusBottomRight = 10
        };
        panel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(panel);

        // Contenedor para las estadísticas
        VBoxContainer statsContainer = new VBoxContainer();
        panel.AddChild(statsContainer);

        // Título
        Label titulo = new Label();
        titulo.Text = "Hoja de Personaje";
        titulo.HorizontalAlignment = HorizontalAlignment.Center;
        titulo.AddThemeFontSizeOverride("font_size", 32);
        statsContainer.AddChild(titulo);

        // Añadir las estadísticas
        AddStat("Origen", personaje.origen);
        AddStat("Arquetipo", personaje.arquetipo);
        AddStat("Agilidad", personaje.Agilidad.ToString());
        AddStat("Fuerza", personaje.Fuerza.ToString());
        AddStat("Inteligencia", personaje.Inteligencia.ToString());
        AddStat("Percepción", personaje.Percepcion.ToString());
        AddStat("Presencia", personaje.Presencia.ToString());
        AddStat("Tenacidad", personaje.Tenacidad.ToString());
        AddStat("Supervivencia", personaje.Supervivencia.ToString());
        AddStat("Ciencias Ocultas", personaje.CienciasOcultas.ToString());
        AddStat("Sigilo", personaje.Sigilo.ToString());

        // Botón para cerrar
        Button cerrarBtn = new Button();
        cerrarBtn.Text = "Cerrar";
        cerrarBtn.Pressed += () => this.QueueFree();
        statsContainer.AddChild(cerrarBtn);
    }

    private void AddStat(string name, string value)
    {
        HBoxContainer statBox = new HBoxContainer();

        Label nameLabel = new Label();
        nameLabel.Text = name + ": ";
        statBox.AddChild(nameLabel);

        Label valueLabel = new Label();
        valueLabel.Text = value;
        statBox.AddChild(valueLabel);

        GetNode<VBoxContainer>("PanelContainer/VBoxContainer").AddChild(statBox);
    }
}
