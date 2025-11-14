using Godot;
using System;

public partial class Dia0Isla : Dia
{
    public Dia0Isla(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoDia0Isla(this));
    }
}
