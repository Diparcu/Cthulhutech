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

        // Camino A: Ir con la madre
        List<Dialogo> dialogoA = new List<Dialogo>();
        dialogoA.Add(new Dialogo("Narrador", "Decides levantarte. Tu madre te sonríe, aunque (Tirada Oculta de Perspicacia) podrías notar que su sonrisa no llega a sus ojos.").addAction(() => {
            this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
        }));
        dialogoA.Add(new Dialogo("Narrador", "Caminan juntos por la orilla. El aire está cargado de una estática extraña y el mar murmura de una forma que no reconoces."));
        dialogoA.Add(new Dialogo("Mama", "Qué raro... los camarones usualmente están más escondidos a esta hora. Hoy están por todas partes."));
        dialogoA.Add(new Dialogo("Narrador", "Te agachas para mirar más de cerca. (Tirada Oculta de Percepción) te permite ver una escena grotesca: un grupo de camarones está devorando con avidez el cadáver de un pájaro marino."));
        dialogoA.Add(new Dialogo("Narrador", "Tu madre desvía la mirada, carraspea y te apura."));
        dialogoA.Add(new Dialogo("Mama", "Bueno, a lo nuestro. Atrapa todos los que puedas."));
        dialogoA.Add(new Dialogo("Narrador", "[MINIJUEGO: Recolectar camarones]"));
        dialogoA.Add(new Dialogo("Narrador", "Luego de un rato, con los baldes llenos, regresan a casa. La extraña tensión en el aire no parece disiparse.").addAction(() => {
            this.getSistema().cargarEscena(new TardeTrabajoIslaIntelectual(this.getSistema()));
        }));
        opcionA.setSiguienteDialogo(dialogoA);

        // Camino B: Quedarse en casa (Éxito en la tirada de Labia)
        List<Dialogo> exitoB = new List<Dialogo>();
        exitoB.Add(new Dialogo("Mama", "Está bien, hijo mío querido, descansa... pero tendrás que trabajar toda la tarde."));
        exitoB.Add(new Dialogo("Narrador", "Tu madre suspira y sale de la casa. Te quedas solo, el silencio solo roto por el lejano sonido de las olas y un viento que huele... raro."));

        Dialogo decisionCasa = new Dialogo("Narrador", "Te levantas y decides qué hacer:");
        List<OpcionDialogo> opcionesCasa = new List<OpcionDialogo>();
        OpcionDialogo opcionFotos = new OpcionDialogo("Revisar las fotos de la repisa");
        OpcionDialogo opcionAfuera = new OpcionDialogo("Mirar por la ventana hacia afuera");
        opcionesCasa.Add(opcionFotos);
        opcionesCasa.Add(opcionAfuera);
        decisionCasa.setDesicion(opcionesCasa);
        exitoB.Add(decisionCasa);
        opcionB.setSiguienteDialogo(exitoB);

        // Sub-camino B1: Revisar fotos
        List<Dialogo> dialogoFotos = new List<Dialogo>();
        dialogoFotos.Add(new Dialogo("Narrador", "Te acercas a la vieja repisa de madera. Hay varias fotos tuyas y de tu madre. En una están en la playa, construyendo un castillo de arena. En otra, están junto a más gente del pueblo frente a un templo de piedra que se adentra en el mar. En otra más, están en la boca de una cueva oscura, sonriendo."));
        dialogoFotos.Add(new Dialogo("Narrador", "(Tirada Oculta de Inteligencia) caes en cuenta de algo. En ninguna de las fotos, ni siquiera en las grupales, aparece tu padre. Nunca te habías fijado."));
        dialogoFotos.Add(new Dialogo("Narrador", "El pensamiento te deja una sensación extraña. Vuelves a la cama, esperando que tu madre regrese.").addAction(() => this.getSistema().cargarEscena(new TardeIslaIntelectual(this.getSistema()))));
        opcionFotos.setSiguienteDialogo(dialogoFotos);

        // Sub-camino B2: Mirar afuera
        List<Dialogo> dialogoAfuera = new List<Dialogo>();
        dialogoAfuera.Add(new Dialogo("Narrador", "Miras por la ventana. Tu casa es la más alejada del resto del pueblo. Para visitar a tus amigos tendrías que caminar un buen trecho, y tu madre siempre se enoja si te alejas tanto solo."));
        dialogoAfuera.Add(new Dialogo("Narrador", "Bajas la vista al suelo que rodea la casa. Está adornado con cientos de conchas marinas. Desde aquí no puedes apreciarlo, pero (Tirada Oculta de Arcanos) si pudieras volar, verías que forman un enorme e intrincado círculo de protección. Sientes una calma extraña al mirarlas."));
        dialogoAfuera.Add(new Dialogo("Narrador", "[MINIJUEGO: Recolectar conchas]"));
        dialogoAfuera.Add(new Dialogo("Narrador", "El tiempo pasa volando. Decides volver a la cama antes de que tu madre llegue.").addAction(() => this.getSistema().cargarEscena(new TardeIslaIntelectual(this.getSistema()))));
        opcionAfuera.setSiguienteDialogo(dialogoAfuera);

        // Camino B: Quedarse en casa (Fallo en la tirada de Labia)
        List<Dialogo> falloB = new List<Dialogo>();
        falloB.Add(new Dialogo("Mama", "¡Nada de quedarse acostado! A mí no me engañas. ¡Levántate ahora mismo!"));
        // Si falla, se le fuerza a ir, así que se usa una versión del diálogo A.
        falloB.Add(new Dialogo("Narrador", "Te levantas a regañadientes. Tu madre te apura, y (Tirada Oculta de Perspicacia) podrías notar que su apuro parece más nerviosismo que enojo.").addAction(() => {
            this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
        }));
        falloB.Add(new Dialogo("Narrador", "Caminan juntos por la orilla. El aire está cargado de una estática extraña y el mar murmura de una forma que no reconoces."));
        falloB.Add(new Dialogo("Mama", "Qué raro... los camarones usualmente están más escondidos a esta hora. Hoy están por todas partes."));
        falloB.Add(new Dialogo("Narrador", "Te agachas para mirar más de cerca. (Tirada Oculta de Percepción) te permite ver una escena grotesca: un grupo de camarones está devorando con avidez el cadáver de un pájaro marino."));
        falloB.Add(new Dialogo("Narrador", "Tu madre desvía la mirada, carraspea y te apura."));
        falloB.Add(new Dialogo("Mama", "Bueno, a lo nuestro. Atrapa todos los que puedas."));
        falloB.Add(new Dialogo("Narrador", "[MINIJUEGO: Recolectar camarones]"));
        falloB.Add(new Dialogo("Narrador", "Luego de un rato, con los baldes llenos, regresan a casa. La extraña tensión en el aire no parece disiparse.").addAction(() => {
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
