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
    }
}
