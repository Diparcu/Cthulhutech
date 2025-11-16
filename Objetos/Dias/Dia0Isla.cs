using Godot;
using System;
using System.Collections.Generic;

public partial class Dia0Isla : Dia
{
    public Dia0Isla(Sistema sistema) : base(sistema)
    {
        this.eventoCargado = new EventoDia0Isla(this);
        this.AddChild(this.eventoCargado);
    }
}

public partial class EventoDia0Isla : Evento
{
    public EventoDia0Isla(Dia dia) : base(dia)
    {
        TextureRect fondo = new TextureRect();
        fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_CASA_PLAYA.jpg");
        fondo.SetSize(new Vector2(1280, 640));
        fondo.Position = new Vector2(-300, 0);
        fondo.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        this.AddChild(fondo);
        fondo.ZIndex = -1;

        this.dialogos.Add(new Dialogo("Narrador", "Lunes, Dia 0. Temprano"));
        this.dialogos.Add(new Dialogo("Narrador", "Tu vieja casa roñosa (está en las afueras de la ciudad de castro en la isla de chiloe casa con aparatos tecnológicos pero antigua y roñosa)"));
        this.dialogos.Add(new Dialogo("Narrador", "los pájaros cantan en la mañana mientras escuchas a los bichos y aves cantando, perros ladrando y ovejas y cabras balando afuera"));
        this.dialogos.Add(new Dialogo("Narrador", "Escuchas a tu vieja puerta llena de posters y campanas sonar [sonido de puerta de tu casa abriéndose.mp3]"));
        this.dialogos.Add(new Dialogo("Mama", "(abre la puerta) Hijo despierta tienes que ayudarme a recoger camarones"));
        this.dialogos.Add(new Dialogo("Jugador", "Ohhhhh [sonidos de estirarse.mp3]"));

        this.dialogos.Add(new Dialogo("Narrador", "Tirada oculta"));

        Dialogo decisionInicial = new Dialogo("Jugador", "...");

        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        OpcionDialogo opcionA = new OpcionDialogo("Claro mama, me levanto altiro");
        opciones.Add(opcionA);

        OpcionDialogo opcionB = new OpcionDialogo("mama me siento muy enfermo de verdad amaneci mal", 9, "Labia");
        opciones.Add(opcionB);

        if (realizarChequeoOculto("SaivorFeire", 10))
        {
            this.dialogos.Add(new Dialogo("Jugador", "(mmmm siento que mi mama se ve preocupada, noto algo raro en ella)"));
            OpcionDialogo opcionC = new OpcionDialogo("mamá te ves rara todo bien?", 12, "Presencia");
            opciones.Add(opcionC);

            List<Dialogo> exitoC = new List<Dialogo>();
            exitoC.Add(new Dialogo("Mama", "oh hijo estoy algo preocupada no escuche a los pájaros cantar como siempre esta mañana, aunque sé que no es nada, me siento inquieta"));

            if(realizarChequeoOculto("SaivorFeire", 11))
            {
                exitoC.Add(new Dialogo("Narrador", "De verdad siente que es una preocupación tonta"));
            }
            else
            {
                exitoC.Add(new Dialogo("Narrador", "No sabes si está bromeando o no"));
            }
            opcionC.setSiguienteDialogo(exitoC);

            List<Dialogo> falloC = new List<Dialogo>();
            falloC.Add(new Dialogo("Mama", "si todo bien hijo mio estoy cansada solamente"));
            opcionC.setSiguienteDialogoFallo(falloC);
        }

        decisionInicial.setDesicion(opciones);

        List<Dialogo> dialogoA = new List<Dialogo>();
        dialogoA.Add(new Dialogo("Narrador", "Pasaste la mañana recogiendo camarones con tu madre").addAction(() => {
            this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
            this.getSistema().cargarEscena(new TardeTrabajoIslaIntelectual(this.getSistema()));
        }));
        opcionA.setSiguienteDialogo(dialogoA);

        List<Dialogo> exitoB = new List<Dialogo>();
        exitoB.Add(new Dialogo("Mama", "esta bien hijo mío querido descanse pero tendrás que trabajar toda la tarde"));
        exitoB.Add(new Dialogo("Jugador", "oh no (cara triste y de flojera)").addAction(() => this.getSistema().cargarEscena(new TardeIslaIntelectual(this.getSistema()))));
        opcionB.setSiguienteDialogo(exitoB);

        List<Dialogo> falloB = new List<Dialogo>();
        falloB.Add(new Dialogo("Mama", "nada de quedarse acostado cabro a mi no me engañai levantate mierda"));
        falloB.Add(new Dialogo("Narrador", "Pasaste la mañana recogiendo camarones con tu madre").addAction(() => {
            this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
            this.getSistema().cargarEscena(new TardeTrabajoIslaIntelectual(this.getSistema()));
        }));
        opcionB.setSiguienteDialogoFallo(falloB);

        this.dialogos.Add(decisionInicial);

        this.cargarTexto();
    }

    private bool realizarChequeoOculto(string habilidad, int dificultad)
    {
        Personaje jugador = this.getJugador();
        int valorHabilidad = (int)jugador.GetType().GetProperty(habilidad).GetValue(jugador);
        var rand = new Random();
        int tirada = rand.Next(1, 21); // Tirada de 1d20
        return tirada + valorHabilidad >= dificultad;
    }
}
