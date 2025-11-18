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
        this.dialogos.Add(new Dialogo("Narrador", "Tu vieja casa roñosa se encuentra en las afueras del pueblo en la Isla Mocha. De un aspecto antiguo y desgastado, en su interior se pueden encontrar radios y teles viejas, teléfonos baratos de hace un par de décadas y un microondas que ha sido reparado una y otra vez."));
        this.dialogos.Add(new Dialogo("Narrador", "los pájaros cantan en la mañana mientras escuchas a los bichos y aves cantando, perros ladrando y ovejas y cabras balando afuera"));
        this.dialogos.Add(new Dialogo("Narrador", "Escuchas a tu vieja puerta llena de posters y campanas sonar [color=darkgreen][sonido de puerta de tu casa abriéndose.mp3][/color]"));
        this.dialogos.Add(new Dialogo("Mama", "[color=cyan](abre la puerta) Hijo despierta tienes que ayudarme a recoger camarones[/color]"));
        this.dialogos.Add(new Dialogo("Jugador", "Ohhhhh [sonidos de estirarse.mp3]"));

        Dialogo decisionInicial = new Dialogo("Jugador", "...");

        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        OpcionDialogo opcionA = new OpcionDialogo("Claro mama, me levanto altiro");
        opciones.Add(opcionA);

        OpcionDialogo opcionB = new OpcionDialogo("mama me siento muy enfermo de verdad amaneci mal", 9, "Labia");
        opciones.Add(opcionB);

        ResultadoTirada tiradaSavoirFaire = realizarTiradaCombinacion("SaviorFaire", "Presencia", 10);
        string textoTiradaSavoirFaire = $"[color=darkgreen][Tirada Oculta - Savior-Faire diff 10: {string.Join("-", tiradaSavoirFaire.DadosLanzados)} | {(tiradaSavoirFaire.Exito ? "Éxito" : "Fallo")}][/color]";
        this.dialogos.Add(new Dialogo("Narrador", textoTiradaSavoirFaire));

        if (tiradaSavoirFaire.Exito)
        {
            this.dialogos.Add(new Dialogo("Jugador", "(mmmm siento que mi mama se ve preocupada, noto algo raro en ella)"));
            OpcionDialogo opcionC = new OpcionDialogo("mamá te ves rara todo bien?", 12, "Presencia");
            opciones.Add(opcionC);

            List<Dialogo> exitoC = new List<Dialogo>();
            exitoC.Add(new Dialogo("Mama", "[color=cyan]oh hijo estoy algo preocupada no escuche a los pájaros cantar como siempre esta mañana, aunque sé que no es nada, me siento inquieta[/color]"));

            ResultadoTirada tiradaSavoirFaire2 = realizarTiradaCombinacion("SaviorFaire", "Presencia", 11);
            string textoTiradaSavoirFaire2 = $"[color=darkgreen][Tirada Oculta - Savior-Faire diff 11: {string.Join("-", tiradaSavoirFaire2.DadosLanzados)} | {(tiradaSavoirFaire2.Exito ? "Éxito" : "Fallo")}][/color]";
            exitoC.Add(new Dialogo("Narrador", textoTiradaSavoirFaire2));
            if(tiradaSavoirFaire2.Exito)
            {
                exitoC.Add(new Dialogo("Narrador", "De verdad siente que es una preocupación tonta"));
            }
            else
            {
                exitoC.Add(new Dialogo("Narrador", "No sabes si está bromeando o no"));
            }

            exitoC.Add(new Dialogo("Mama", "[color=cyan]Bueno, no desvíes el tema. Tienes que levantarte para ir a recoger camarones.[/color]"));
            Dialogo decisionVuelta = new Dialogo("Jugador", "...");
            List<OpcionDialogo> opcionesVuelta = new List<OpcionDialogo>();
            opcionesVuelta.Add(opcionA);
            opcionesVuelta.Add(opcionB);
            decisionVuelta.setDesicion(opcionesVuelta);
            exitoC.Add(decisionVuelta);

            opcionC.setSiguienteDialogo(exitoC);

            List<Dialogo> falloC = new List<Dialogo>();
            falloC.Add(new Dialogo("Mama", "[color=cyan]si todo bien hijo mio estoy cansada solamente[/color]"));
            opcionC.setSiguienteDialogoFallo(falloC);
        }

        decisionInicial.setDesicion(opciones);

        // Camino A: Ir con la madre
        List<Dialogo> dialogoA = new List<Dialogo>();
        ResultadoTirada tiradaSonrisa = realizarTiradaCombinacion("SaviorFaire", "Presencia", 10);
        string textoTiradaSonrisa = $"[color=darkgreen][Tirada Oculta - Savior-Faire diff 10: {string.Join("-", tiradaSonrisa.DadosLanzados)} | {(tiradaSonrisa.Exito ? "Éxito" : "Fallo")}][/color]";

        if (tiradaSonrisa.Exito)
        {
            dialogoA.Add(new Dialogo("Narrador", "Decides levantarte. Tu madre te sonríe, aunque notas que su sonrisa no llega a sus ojos. " + textoTiradaSonrisa).addAction(() => {
                this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
            }));
        }
        else
        {
            dialogoA.Add(new Dialogo("Narrador", "Decides levantarte. Tu madre te sonríe. " + textoTiradaSonrisa).addAction(() => {
                this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
            }));
        }
        dialogoA.AddRange(getDialogoCamarones());
        opcionA.setSiguienteDialogo(dialogoA);

        // Camino B: Quedarse en casa (Éxito en la tirada de Labia)
        List<Dialogo> exitoB = new List<Dialogo>();
        exitoB.Add(new Dialogo("Mama", "[color=cyan]Está bien, hijo mío querido, descansa... pero tendrás que trabajar toda la tarde.[/color]"));
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

        ResultadoTirada tiradaInvestigacion = realizarTiradaCombinacion("Investigacion", "Inteligencia", 10);
        string textoTiradaInvestigacion = $"[color=darkgreen][Tirada Oculta - Investigación diff 10: {string.Join("-", tiradaInvestigacion.DadosLanzados)} | {(tiradaInvestigacion.Exito ? "Éxito" : "Fallo")}][/color]";
        dialogoFotos.Add(new Dialogo("Narrador", textoTiradaInvestigacion));

        if (tiradaInvestigacion.Exito)
        {
            dialogoFotos.Add(new Dialogo("Narrador", "Caes en cuenta de algo. En ninguna de las fotos, ni siquiera en las grupales, aparece tu padre. Nunca te habías fijado."));
        }
        else
        {
            dialogoFotos.Add(new Dialogo("Narrador", "Las fotos te traen buenos recuerdos, pero no notas nada inusual en ellas."));
        }
        dialogoFotos.Add(new Dialogo("Narrador", "El pensamiento te deja una sensación extraña. Vuelves a la cama, esperando que tu madre regrese.").addAction(() => this.getSistema().cargarEscena(new TardeIslaIntelectual(this.getSistema()))));
        opcionFotos.setSiguienteDialogo(dialogoFotos);

        // Sub-camino B2: Mirar afuera
        List<Dialogo> dialogoAfuera = new List<Dialogo>();
        dialogoAfuera.Add(new Dialogo("Narrador", "Miras por la ventana. Tu casa es la más alejada del resto del pueblo. Para visitar a tus amigos tendrías que caminar un buen trecho, y tu madre siempre se enoja si te alejas tanto solo."));

        ResultadoTirada tiradaCienciasOcultas = realizarTiradaCombinacion("CienciasOcultas", "Inteligencia", 14);
        string textoTiradaCienciasOcultas = $"[color=darkgreen][Tirada Oculta - Ciencias Ocultas diff 14: {string.Join("-", tiradaCienciasOcultas.DadosLanzados)} | {(tiradaCienciasOcultas.Exito ? "Éxito" : "Fallo")}][/color]";
        dialogoAfuera.Add(new Dialogo("Narrador", textoTiradaCienciasOcultas));

        if (tiradaCienciasOcultas.Exito)
        {
            dialogoAfuera.Add(new Dialogo("Narrador", "Bajas la vista al suelo que rodea la casa. Está adornado con cientos de conchas marinas. Desde aquí no puedes apreciarlo, pero si pudieras volar, verías que forman un enorme e intrincado círculo de protección. Sientes una calma extraña al mirarlas."));
        }
        else
        {
            dialogoAfuera.Add(new Dialogo("Narrador", "Bajas la vista al suelo que rodea la casa. Está adornado con cientos de conchas marinas. Te parecen bonitas, sin más."));
        }
        dialogoAfuera.Add(new Dialogo("Narrador", "[MINIJUEGO: Recolectar conchas]"));
        dialogoAfuera.Add(new Dialogo("Narrador", "El tiempo pasa volando. Decides volver a la cama antes de que tu madre llegue.").addAction(() => this.getSistema().cargarEscena(new TardeIslaIntelectual(this.getSistema()))));
        opcionAfuera.setSiguienteDialogo(dialogoAfuera);

        // Camino B: Quedarse en casa (Fallo en la tirada de Labia)
        List<Dialogo> falloB = new List<Dialogo>();
        falloB.Add(new Dialogo("Mama", "[color=cyan]¡Nada de quedarse acostado! A mí no me engañas. ¡Levántate ahora mismo![/color]"));
        // Si falla, se le fuerza a ir, así que se usa una versión del diálogo A.
        ResultadoTirada tiradaSonrisaFallo = realizarTiradaCombinacion("SaviorFaire", "Presencia", 10);
        string textoTiradaSonrisaFallo = $"[color=darkgreen][Tirada Oculta - Savior-Faire diff 10: {string.Join("-", tiradaSonrisaFallo.DadosLanzados)} | {(tiradaSonrisaFallo.Exito ? "Éxito" : "Fallo")}][/color]";

        if (tiradaSonrisaFallo.Exito)
        {
            falloB.Add(new Dialogo("Narrador", "Te levantas a regañadientes. Tu madre te apura, y notas que su apuro parece más nerviosismo que enojo. " + textoTiradaSonrisaFallo).addAction(() => {
                this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
            }));
        }
        else
        {
            falloB.Add(new Dialogo("Narrador", "Te levantas a regañadientes. Tu madre te apura. " + textoTiradaSonrisaFallo).addAction(() => {
                this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CAMARONES.jpg");
            }));
        }
        falloB.AddRange(getDialogoCamarones());
        opcionB.setSiguienteDialogoFallo(falloB);

        this.dialogos.Add(decisionInicial);

        this.cargarTexto();
    }

}

public partial class EventoDia0Isla
{
    private List<Dialogo> getDialogoCamarones()
    {
        List<Dialogo> dialogo = new List<Dialogo>();

        dialogo.Add(new Dialogo("Narrador", "Caminan juntos por la orilla. El aire está cargado de una estática extraña y el mar murmura de una forma que no reconoces."));
        dialogo.Add(new Dialogo("Mama", "[color=cyan]Qué raro... los camarones usualmente están más escondidos a esta hora. Hoy están por todas partes.[/color]"));

        ResultadoTirada tiradaObservar = realizarTiradaCombinacion("Observar", "Percepcion", 12);
        string textoTiradaObservar = $"[color=darkgreen][Tirada Oculta - Observar diff 12: {string.Join("-", tiradaObservar.DadosLanzados)} | {(tiradaObservar.Exito ? "Éxito" : "Fallo")}][/color]";
        dialogo.Add(new Dialogo("Narrador", textoTiradaObservar));

        if (tiradaObservar.Exito)
        {
            dialogo.Add(new Dialogo("Narrador", "Te agachas para mirar más de cerca. Te permite ver una escena grotesca: un grupo de camarones está devorando con avidez el cadáver de un pájaro marino."));
            dialogo.Add(new Dialogo("Narrador", "Tu madre desvía la mirada, carraspea y te apura."));
        }
        else
        {
            dialogo.Add(new Dialogo("Narrador", "Te agachas para mirar más de cerca, pero no notas nada fuera de lo común."));
        }
        dialogo.Add(new Dialogo("Mama", "[color=cyan]Bueno, a lo nuestro. Atrapa todos los que puedas.[/color]"));
        dialogo.Add(new Dialogo("Narrador", "El agua está inusualmente oscura y fría al tacto. Los camarones, por su parte, se mueven de forma errática, casi frenética. Son más fáciles de atrapar de lo normal."));
        dialogo.Add(new Dialogo("Narrador", "Mientras recoges los camarones, te parece escuchar un murmullo grave y profundo que proviene del mar, pero el sonido de las olas lo ahoga antes de que puedas estar seguro."));
        dialogo.Add(new Dialogo("Narrador", "[MINIJUEGO: Recolectar camarones]"));
        dialogo.Add(new Dialogo("Narrador", "Luego de un rato, con los baldes llenos, regresan a casa. La extraña tensión en el aire no parece disiparse.").addAction(() => {
            this.getSistema().cargarEscena(new TardeTrabajoIslaIntelectual(this.getSistema()));
        }));

        return dialogo;
    }
}
