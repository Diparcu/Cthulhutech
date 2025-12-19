using Godot;
using System;
using System.Collections.Generic;

public partial class DiaChud : Dia
{
    public DiaChud(Sistema sistema) : base(sistema)
    {
        this.eventoCargado = new EventoDia0Chud(this);
        this.AddChild(this.eventoCargado);
    }
}

public partial class EventoDia0Chud : Evento
{
    public EventoDia0Chud(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_ORDENADOR.png");
        this.dialogos.Add(new Dialogo("Hola buenas tardes 1."));
        this.dialogos.Add(new Dialogo("Hola buenas tardes 2."));
        this.dialogos.Add(new Dialogo("Hola buenas tardes 3.")
                .setFinal(typeof(EventoDia1Chud)));
    }
}

public partial class EventoDia1Chud : Evento
{
    public EventoDia1Chud(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_ORDENADOR.png");
        this.dialogos.Add(new Dialogo("Hola buenas tardes 9."));
        this.dialogos.Add(new Dialogo("Hola buenas tardes 10."));
        this.dialogos.Add(new Dialogo("Hola buenas tardes 11.")
                .setFinal(typeof(EventoDia1Chud)));
    }
}
