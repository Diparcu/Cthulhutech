using Godot;
using System;

public partial class EventoShinjiEntrenamientoArmas1 : Evento
{
    public EventoShinjiEntrenamientoArmas1(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Jugador", "Wena wn, Shinji, ¿tambien entrenando armas?."));
        this.dialogos.Add(new Dialogo("Shinji", "Si wn, me estoy preparando para hacer el tiroteo.")
                .setFinal(typeof(EventoGenericoDespuesDeClase)));
    }
}
