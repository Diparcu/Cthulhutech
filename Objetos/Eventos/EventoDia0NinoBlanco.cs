using Godot;
using System;

public partial class EventoDia0NinoBlanco : Evento
{
    public EventoDia0NinoBlanco(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Narrador", "Inicio del día para el Niño Blanco. (Placeholder)")
                .setFinal());
        this.cargarTexto();
    }
}
