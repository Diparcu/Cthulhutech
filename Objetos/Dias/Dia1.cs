using Godot;
using System.Collections.Generic;

public partial class Dia1 : DiaEstandar
{
    public Dia1(Sistema sistema) : base(sistema)
    {
    }

    public override Evento GetEventoClase()
    {
        return new EventoHistoriaArcologia(this);
    }

    public override Evento GetEventoAlmuerzo()
    {
        return new EventoAlmuerzoVibeCheck(this);
    }
}

public partial class EventoHistoriaArcologia : Evento
{
    public EventoHistoriaArcologia(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_CLASE.jpg"); // Placeholder

        this.dialogos.Add(new Dialogo("Narrador", "El salón de clases está en silencio. El Profesor Sergio entra con paso firme."));
        this.dialogos.Add(new Dialogo("Profesor Sergio", "Bienvenidos. Hoy hablaremos del origen de nuestra Arcología."));
        this.dialogos.Add(new Dialogo("Profesor Sergio", "No siempre vivimos protegidos por estas paredes de bio-acero. Hubo un tiempo... antes de los Primigenios..."));
        this.dialogos.Add(new Dialogo("Jugador", "(Escuchas atentamente, fascinado por la historia de la caída del mundo exterior.)"));
        this.dialogos.Add(new Dialogo("Profesor Sergio", "La lección de hoy es simple: La ignorancia no es felicidad. Es extinción."));

        this.cargarTexto();
    }
}

public partial class EventoAlmuerzoVibeCheck : Evento
{
    public EventoAlmuerzoVibeCheck(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_COMEDOR.jpg"); // Placeholder

        this.dialogos.Add(new Dialogo("Narrador", "Te sientas en una mesa libre con tu bandeja de comida sintética."));
        this.dialogos.Add(new Dialogo("???", "¿Está ocupado?"));
        this.dialogos.Add(new Dialogo("Narrador", "Levantas la vista. Un estudiante con una mirada intensa te observa."));

        Dialogo decision = new Dialogo("Jugador", "...");
        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        OpcionDialogo opA = new OpcionDialogo("No, siéntate.");
        opA.setSiguienteDialogo(new List<Dialogo> {
            new Dialogo("Estudiante", "Gracias. Soy Alex. He oído hablar de ti."),
            new Dialogo("Narrador", "Comen en un silencio cómodo. Sientes que has hecho un aliado.")
        });
        opciones.Add(opA);

        OpcionDialogo opB = new OpcionDialogo("Prefiero comer solo.");
        opB.setSiguienteDialogo(new List<Dialogo> {
            new Dialogo("Estudiante", "Entiendo. Mala vibra."),
            new Dialogo("Narrador", "Se aleja. Quizás perdiste una oportunidad.")
        });
        opciones.Add(opB);

        decision.setDesicion(opciones);
        this.dialogos.Add(decision);

        this.cargarTexto();
    }
}
