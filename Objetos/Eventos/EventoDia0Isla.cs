using Godot;
using System;

public partial class EventoDia0Isla : Evento
{
    public EventoDia0Isla(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Madre", "¡Despierta, hijo! Es hora de ir a recoger camarones."));
        this.dialogos.Add(new Dialogo("Jugador", "Ya voy, mamá..."));
        this.dialogos.Add(new Dialogo("Narrador", "Te levantas de la cama y te preparas para un nuevo día en la isla.")
                .setFinal());
        this.cargarTexto();
    }
}
