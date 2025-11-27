using Godot;
using System;
using System.Collections.Generic;

public partial class EventoDia0BajosFondosTarde : Evento
{
    public EventoDia0BajosFondosTarde(Dia dia) : base(dia)
    {
        this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CASA_SALINA.jpg");

        this.dialogos.Add(new Dialogo("Narrador", "Llega la tarde en la zona salina. El sol comienza a bajar, proyectando sombras largas sobre los montones de chatarra."));

        // Placeholder implementation
        this.dialogos.Add(new Dialogo("Narrador", "Continuará..."));

        this.cargarTexto();
    }
}
