using Godot;
using System;
using System.Collections.Generic;

public partial class CreadorDePersonaje : Node2D
{
    private Sistema sistema;
    private VBoxContainer origenContainer;
    private VBoxContainer arquetipoContainer;
    private Label descripcionLabel;
    private Personaje personaje;

    private Dictionary<string, string> origenesDescripciones = new Dictionary<string, string>
    {
        { "Niño isleño", "Naciste en una isla del sur, con los años la guerra provocó que tuvieras que migrar a la arcología." },
        { "Niño blanco", "Nacido en la arcología, de padre humano y madre nazzadi, su raro albinismo es señal de poderes sobrehumnos aún por descubrir, sabido por el estado, un agente de la NGT (nuevo gobierno terrestre) está atento a ti y tus talentos" },
        { "Niño bajos fondos", "tu padre arquitecto y madre ausente, has tenido un padre ausente que se esfuerza por mantener una calidad de vida decente viviendo en la chatarrería de la zona salina" }
    };

    private Dictionary<string, string> arquetiposDescripciones = new Dictionary<string, string>
    {
        { "Intelectual", "Te enfocas en el conocimiento y la astucia." },
        { "Bruto", "Confías en tu fuerza y resistencia para superar los desafíos." },
        { "Social", "Tu poder reside en tu conexión con los demás y tu intuición." }
    };

    public CreadorDePersonaje(Sistema sistema)
    {
        this.sistema = sistema;
        this.personaje = new Personaje();
    }

    public override void _Ready()
    {
        crearPantallaOrigen();
    }

    private void crearPantallaOrigen()
    {
        origenContainer = new VBoxContainer();
        origenContainer.Position = new Vector2(100, 100);

        descripcionLabel = new Label();
        descripcionLabel.Position = new Vector2(100, 300);
        descripcionLabel.Text = "Selecciona un origen...";
        AddChild(descripcionLabel);

        foreach (var origen in origenesDescripciones)
        {
            Button boton = new Button();
            boton.Text = origen.Key;
            boton.MouseEntered += () => { descripcionLabel.Text = origen.Value; };
            boton.Pressed += () => { seleccionarOrigen(origen.Key); };
            origenContainer.AddChild(boton);
        }

        AddChild(origenContainer);
    }

    private void seleccionarOrigen(string origen)
    {
        personaje.origen = origen;
        origenContainer.Visible = false;
        crearPantallaArquetipo();
    }

    private void crearPantallaArquetipo()
    {
        arquetipoContainer = new VBoxContainer();
        arquetipoContainer.Position = new Vector2(100, 100);

        descripcionLabel.Text = "Selecciona un arquetipo...";

        foreach (var arquetipo in arquetiposDescripciones)
        {
            Button boton = new Button();
            boton.Text = arquetipo.Key;
            boton.MouseEntered += () => { descripcionLabel.Text = arquetipo.Value; };
            boton.Pressed += () => { seleccionarArquetipo(arquetipo.Key); };
            arquetipoContainer.AddChild(boton);
        }

        AddChild(arquetipoContainer);
    }

    private void seleccionarArquetipo(string arquetipo)
    {
        personaje.arquetipo = arquetipo;
        arquetipoContainer.Visible = false;
        iniciarJuego();
    }

    private void iniciarJuego()
    {
        switch (personaje.origen)
        {
            case "Niño isleño":
                sistema.iniciarJuegoIsla(this);
                break;
            case "Niño blanco":
                sistema.iniciarJuegoBlanco(this);
                break;
            case "Niño bajos fondos":
                sistema.iniciarJuegoBajosFondos(this);
                break;
        }
    }
}
