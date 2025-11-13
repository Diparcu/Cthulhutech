using Godot;
using System;

public partial class PantallaDeOpciones : Node2D
{
    private const int LONGITUD_PANTALLA = 1280;
    private const int ALTURA_PANTALLA = 640;
    private const int FONT_SIZE = 48;

    private Sistema sistema;
    private Font fuente;

    public PantallaDeOpciones(Font fuente, Sistema sistema)
    {
        this.fuente = fuente;
        this.sistema = sistema;
    }

    public override void _Ready()
    {
        VBoxContainer container = new VBoxContainer();
        container.Position = new Vector2(LONGITUD_PANTALLA / 3, ALTURA_PANTALLA / 3);
        container.AddThemeConstantOverride("separation", 20);

        // Game Volume
        HBoxContainer gameVolumeContainer = new HBoxContainer();
        Label gameVolumeLabel = new Label();
        gameVolumeLabel.Text = "Volumen del juego";
        gameVolumeLabel.AddThemeFontSizeOverride("font_size", 32);
        HSlider gameVolumeSlider = new HSlider();
        gameVolumeSlider.MinValue = 0;
        gameVolumeSlider.MaxValue = 100;
        gameVolumeSlider.Value = 100;
        gameVolumeSlider.CustomMinimumSize = new Vector2(200, 0);
        gameVolumeContainer.AddChild(gameVolumeLabel);
        gameVolumeContainer.AddChild(gameVolumeSlider);
        container.AddChild(gameVolumeContainer);

        // Music Volume
        HBoxContainer musicVolumeContainer = new HBoxContainer();
        Label musicVolumeLabel = new Label();
        musicVolumeLabel.Text = "Volumen de música";
        musicVolumeLabel.AddThemeFontSizeOverride("font_size", 32);
        HSlider musicVolumeSlider = new HSlider();
        musicVolumeSlider.MinValue = 0;
        musicVolumeSlider.MaxValue = 100;
        musicVolumeSlider.Value = 100;
        musicVolumeSlider.CustomMinimumSize = new Vector2(200, 0);
        musicVolumeContainer.AddChild(musicVolumeLabel);
        musicVolumeContainer.AddChild(musicVolumeSlider);
        container.AddChild(musicVolumeContainer);

        // Back Button
        Button volver = new Button();
        volver.Text = "Volver";
        volver.Flat = true;
        volver.AddThemeFontSizeOverride("font_size", 32);
        volver.Pressed += () => this.sistema.volverAlMenuPrincipalDesdeMenu(this);
        container.AddChild(volver);

        this.AddChild(container);
    }

    public override void _Draw()
    {
        DrawString(Sistema.fuente,
            new Vector2(LONGITUD_PANTALLA / 2 - 100,
                    ALTURA_PANTALLA / 4),
            "Opciones",
            HorizontalAlignment.Center,
            -1,
            FONT_SIZE);
    }
}
