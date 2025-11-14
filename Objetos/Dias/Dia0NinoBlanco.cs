using Godot;
using System;

public partial class Dia0NinoBlanco : Dia
{
    public Dia0NinoBlanco(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoDia0NinoBlanco(this));
    }
}
