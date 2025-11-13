using Godot;
using System;
using System.Collections.Generic;

public partial class DiaMegan : Dia
{
	public DiaMegan(Sistema sistema) : base(sistema){
        if (this.getEventoCargado() != null)
        {
            this.getEventoCargado().QueueFree();
        }
        this.setEventoCargado(new EventoMegan(this));
	}

	public override void _Ready()
	{
	}
}
