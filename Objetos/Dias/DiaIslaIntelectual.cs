using Godot;
using System;

public partial class DiaIslaIntelectual : Dia
{
    public DiaIslaIntelectual(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoIslaIntelectual(this));
    }
}
