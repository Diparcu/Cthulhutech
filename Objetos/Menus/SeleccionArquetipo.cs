using Godot;
using System;
using System.Collections.Generic;

public partial class SeleccionArquetipo : Node2D
{
    private const int LONGITUD_PANTALLA = 1280;
    private const int ALTURA_PANTALLA = 640;
    private const int FONT_SIZE = 24;

    private Sistema sistema;
    private string origenSeleccionado;
    private VBoxContainer arquetipoContainer;
    private Label descripcionLabel;
    private TextureRect personajeImagen;
    private Sprite2D fondo;

    private Dictionary<string, string> arquetiposDescripciones = new Dictionary<string, string>
    {
        { "Intelectual", "Te enfocas en el conocimiento y la astucia." },
        { "Manual", "Confías en tu fuerza y resistencia para superar los desafíos." },
        { "Emocional", "Tu poder reside en tu conexión con los demás y tu intuición." }
    };

    public SeleccionArquetipo(Sistema sistema, string origen)
    {
        this.sistema = sistema;
        this.origenSeleccionado = origen;
    }

    public override void _Ready()
    {
        // Fondo
        fondo = new Sprite2D();
        AddChild(fondo);
        CambiarFondo();
        fondo.ZIndex = -1;

        // Panel izquierdo para la lista de arquetipos
        PanelContainer listaPanel = new PanelContainer();
        listaPanel.SetPosition(new Vector2(50, 100));
        listaPanel.SetSize(new Vector2(400, 440));
        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0, 0, 0, 0.5f),
            CornerRadiusTopLeft = 10, CornerRadiusTopRight = 10,
            CornerRadiusBottomLeft = 10, CornerRadiusBottomRight = 10
        };
        listaPanel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(listaPanel);

        arquetipoContainer = new VBoxContainer();
        listaPanel.AddChild(arquetipoContainer);

        foreach (var arquetipo in arquetiposDescripciones)
        {
            Button boton = new Button();
            boton.Text = arquetipo.Key;
            boton.Flat = true;
            boton.AddThemeFontSizeOverride("font_size", FONT_SIZE);
            boton.MouseEntered += () => { descripcionLabel.Text = arquetipo.Value; };
            boton.Pressed += () => { seleccionarArquetipo(arquetipo.Key); };
            arquetipoContainer.AddChild(boton);
        }

        // Panel derecho para la imagen del personaje
        PanelContainer imagenPanel = new PanelContainer();
        imagenPanel.SetPosition(new Vector2(LONGITUD_PANTALLA - 450, 100));
        imagenPanel.SetSize(new Vector2(400, 440));
        imagenPanel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(imagenPanel);

        personajeImagen = new TextureRect();
        personajeImagen.Texture = (Texture2D)GD.Load("res://Sprites/Personajes/Shinji.png");
        personajeImagen.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;
        personajeImagen.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        imagenPanel.AddChild(personajeImagen);

        // Panel inferior para la descripción
        PanelContainer descripcionPanel = new PanelContainer();
        descripcionPanel.SetPosition(new Vector2(50, ALTURA_PANTALLA - 150));
        descripcionPanel.SetSize(new Vector2(LONGITUD_PANTALLA - 100, 100));
        descripcionPanel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(descripcionPanel);

        descripcionLabel = new Label();
        descripcionLabel.Text = "Selecciona un arquetipo...";
        descripcionLabel.AutowrapMode = TextServer.AutowrapMode.Word;
        descripcionLabel.AddThemeFontSizeOverride("font_size", FONT_SIZE);
        descripcionPanel.AddChild(descripcionLabel);
    }

    private void seleccionarArquetipo(string arquetipo)
    {
        sistema.iniciarJuego(this, origenSeleccionado, arquetipo);
    }

    private void CambiarFondo()
    {
        var texture = (Texture2D)GD.Load("res://Sprites/Fondos/callejon.png");
        fondo.Texture = texture;
        float scaleX = LONGITUD_PANTALLA / (float)texture.GetWidth();
        float scaleY = ALTURA_PANTALLA / (float)texture.GetHeight();
        fondo.Scale = new Vector2(scaleX, scaleY);
        fondo.Position = new Vector2(LONGITUD_PANTALLA / 2, ALTURA_PANTALLA / 2);
    }
}
