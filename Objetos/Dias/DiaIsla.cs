using Godot;
using System;
using System.Collections.Generic;

public partial class DiaIsla : Dia
{
	public DiaIsla(Sistema sistema) : base(sistema){
        if (this.getEventoCargado() != null)
        {
            this.getEventoCargado().QueueFree();
        }
        this.setEventoCargado(new EventoIsla(this));
	}

	public override void _Ready()
	{
	}
}
