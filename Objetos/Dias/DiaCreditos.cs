using Godot;
using System;
using System.Collections.Generic;

public partial class DiaCreditos : Dia
{
    public DiaCreditos(Sistema sistema) : base(sistema){
        if (this.getEventoCargado() != null)
        {
            this.getEventoCargado().QueueFree();
        }
        this.setEventoCargado(new EventoCreditos(this));
    }
}
