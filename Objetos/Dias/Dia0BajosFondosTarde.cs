using Godot;
using System;

public partial class Dia0BajosFondosTarde : Dia
{
    public Dia0BajosFondosTarde(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoDia0BajosFondosTarde(this));
    }
}
