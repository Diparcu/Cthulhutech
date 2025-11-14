using Godot;
using System;

public partial class EventoDia0NinoBajomundo : Evento
{
    public EventoDia0NinoBajomundo(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Narrador", "Inicio del día para el Niño Bajomundo. (Placeholder)")
                .setFinal());
        this.cargarTexto();
    }
}
