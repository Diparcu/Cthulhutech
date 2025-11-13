using Godot;
using System;
using System.Collections.Generic;

public partial class DiaNormal : Dia
{
	public DiaNormal(Sistema sistema) : base(sistema){
        if (this.getEventoCargado() != null)
        {
            this.getEventoCargado().QueueFree();
        }
        this.setEventoCargado(new EventoNormal(this));
	}

	public override void _Ready()
	{
	}
}
