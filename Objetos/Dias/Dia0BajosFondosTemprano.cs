using Godot;
using System;
using System.Collections.Generic;

public partial class Dia0BajosFondosTemprano : Dia
{
    public Dia0BajosFondosTemprano(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoDia0BajosFondosTemprano(this));
    }
}
