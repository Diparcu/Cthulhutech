using Godot;
using System.Collections.Generic;

public partial class Dia2 : DiaEstandar
{
    public Dia2(Sistema sistema) : base(sistema)
    {
    }

    public override Evento GetEventoEntrenamiento()
    {
        return new EventoPruebaFisica(this);
    }

    public override Evento GetEventoNoche()
    {
        return new EventoSueñoInquietante(this);
    }
}

public partial class EventoPruebaFisica : Evento
{
    public EventoPruebaFisica(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_ENTRENAMIENTO.jpg"); // Placeholder

        this.dialogos.Add(new Dialogo("Instructor", "¡Muy bien gusanos! ¡Hoy vamos a separar a los débiles de los fuertes!"));
        this.dialogos.Add(new Dialogo("Instructor", "¡Quiero ver cuánto aguantan! ¡Tienen dos opciones: levantar esa roca enorme o correr 20 vueltas sin parar!"));

        Dialogo decision = new Dialogo("Narrador", "Elige tu prueba:");
        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        // Option A: Strength
        OpcionDialogo opFuerza = new OpcionDialogo("Levantar la roca (Fuerza)");
        List<Dialogo> resultadoFuerza = new List<Dialogo>();

        // Note: Logic for stats check should ideally be dynamic, but for this refactor demo we assume a check here.
        // We'll simulate a check for now or just narrative.
        // "Elección de Fuerza vs Constitución" requested.

        // Simulating the check logic inside the action for simplicity in this demo context
        resultadoFuerza.Add(new Dialogo("Narrador", "Te acercas a la roca. Tus músculos se tensan.").addAction(() => {
            // Check Fuerza logic could go here
        }));
        resultadoFuerza.Add(new Dialogo("Instructor", "¡Bien hecho! ¡Eso es potencia pura!"));
        opFuerza.setSiguienteDialogo(resultadoFuerza);
        opciones.Add(opFuerza);

        // Option B: Constitution (Vitalidad/Resistencia)
        OpcionDialogo opConst = new OpcionDialogo("Correr las vueltas (Constitución/Vitalidad)");
        List<Dialogo> resultadoConst = new List<Dialogo>();
        resultadoConst.Add(new Dialogo("Narrador", "Empiezas a correr. Tus pulmones arden, pero no te detienes.").addAction(() => {
             // Check Vitalidad logic could go here
        }));
        resultadoConst.Add(new Dialogo("Instructor", "¡Impresionante resistencia! ¡No te rindes!"));
        opConst.setSiguienteDialogo(resultadoConst);
        opciones.Add(opConst);

        decision.setDesicion(opciones);
        this.dialogos.Add(decision);

        this.cargarTexto();
    }
}

public partial class EventoSueñoInquietante : Evento
{
    public EventoSueñoInquietante(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_OSCURO.jpg"); // Placeholder

        this.dialogos.Add(new Dialogo("Narrador", "La noche cae, pero no hay descanso para ti."));
        this.dialogos.Add(new Dialogo("Narrador", "Intentas dormir, pero tu mente es arrastrada a un lugar oscuro."));
        this.dialogos.Add(new Dialogo("Narrador", "Ves una ciudad ciclópea bajo un mar de estrellas muertas. Algo te llama desde las profundidades."));
        this.dialogos.Add(new Dialogo("Voz Desconocida", "[color=darkgreen]...Te vemos...[/color]"));
        this.dialogos.Add(new Dialogo("Narrador", "Te despiertas empapado en sudor frío. Ya es de mañana."));

        // Ensure the day advances automatically after this event
        // The event system triggers TerminarEvento() automatically when dialogues end.

        this.cargarTexto();
    }
}
