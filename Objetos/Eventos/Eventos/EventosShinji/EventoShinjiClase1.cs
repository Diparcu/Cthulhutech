using Godot;
using System;

public partial class EventoShinjiClase1 : Evento
{
    public EventoShinjiClase1(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Jugador", "Wena wn, como te llamai?.")
                .addFlagUpdate(Flags.CONOCE_A_SHINJI));
        this.dialogos.Add(new Dialogo("Shinji", "Wena wn, me llamo shinji.")
                .setFinal(typeof(EventoGenericoAlmuerzo)));
    }
}
