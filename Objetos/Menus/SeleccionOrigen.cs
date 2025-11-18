using Godot;
using System;
using System.Collections.Generic;

public partial class SeleccionOrigen : Node2D
{
    private const int LONGITUD_PANTALLA = 1280;
    private const int ALTURA_PANTALLA = 640;
    private const int FONT_SIZE = 24;

    private Sistema sistema;
    private VBoxContainer origenContainer;
    private Label descripcionLabel;
    private TextureRect personajeImagen;
    private Sprite2D fondo;

    private Dictionary<string, string> origenesDescripciones = new Dictionary<string, string>
    {
        { "Niño isleño", "Naciste en una isla del sur, con los años la guerra provocó que tuvieras que migrar a la arcología." },
        { "Niño blanco", "Nacido en la arcología, de padre humano y madre nazzadi, su raro albinismo es señal de poderes sobrehumnos aún por descubrir, sabido por el estado, un agente de la NGT (nuevo gobierno terrestre) está atento a ti y tus talentos" },
        { "Niño bajos fondos", "tu padre arquitecto y madre ausente, has tenido un padre ausente que se esfuerza por mantener una calidad de vida decente viviendo en la chatarrería de la zona salina" }
    };

    public SeleccionOrigen(Sistema sistema)
    {
        this.sistema = sistema;
    }

    public override void _Ready()
    {
        // Fondo
        fondo = new Sprite2D();
        AddChild(fondo);
        CambiarFondo();
        fondo.ZIndex = -1;

        // Panel izquierdo para la lista de orígenes
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

        origenContainer = new VBoxContainer();
        listaPanel.AddChild(origenContainer);

        foreach (var origen in origenesDescripciones)
        {
            Button boton = new Button();
            boton.Text = origen.Key;
            boton.Flat = true;
            boton.AddThemeFontSizeOverride("font_size", FONT_SIZE);
            boton.MouseEntered += () => { descripcionLabel.Text = origen.Value; };
            boton.Pressed += () => { seleccionarOrigen(origen.Key); };
            origenContainer.AddChild(boton);
        }

        // Panel derecho para la imagen del personaje
        PanelContainer imagenPanel = new PanelContainer();
        imagenPanel.SetPosition(new Vector2(LONGITUD_PANTALLA - 450, 100));
        imagenPanel.SetSize(new Vector2(400, 440));
        imagenPanel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(imagenPanel);

        personajeImagen = new TextureRect();
        personajeImagen.Texture = (Texture2D)GD.Load("res://Sprites/Personajes/Shinji.png");
        personajeImagen.Expand = true;
        personajeImagen.StretchMode = TextureRect.StretchMode.KeepAspectCentered;
        personajeImagen.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        imagenPanel.AddChild(personajeImagen);

        // Panel inferior para la descripción
        PanelContainer descripcionPanel = new PanelContainer();
        descripcionPanel.SetPosition(new Vector2(50, ALTURA_PANTALLA - 150));
        descripcionPanel.SetSize(new Vector2(LONGITUD_PANTALLA - 100, 100));
        descripcionPanel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(descripcionPanel);

        descripcionLabel = new Label();
        descripcionLabel.Text = "Selecciona un origen...";
        descripcionLabel.AutowrapMode = TextServer.AutowrapMode.Word;
        descripcionLabel.AddThemeFontSizeOverride("font_size", FONT_SIZE);
        descripcionPanel.AddChild(descripcionLabel);
    }

    private void seleccionarOrigen(string origen)
    {
        sistema.iniciarSeleccionArquetipo(this, origen);
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
