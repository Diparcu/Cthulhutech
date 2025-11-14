using Godot;
using System;

public partial class Dia0NinoBajomundo : Dia
{
    public Dia0NinoBajomundo(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoDia0NinoBajomundo(this));
    }
}
