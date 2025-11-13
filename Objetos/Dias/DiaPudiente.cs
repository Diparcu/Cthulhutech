using Godot;
using System;
using System.Collections.Generic;

public partial class DiaPudiente : Dia
{
	public DiaPudiente(Sistema sistema) : base(sistema){
        if (this.getEventoCargado() != null)
        {
            this.getEventoCargado().QueueFree();
        }
        this.setEventoCargado(new EventoPudiente(this));
	}

	public override void _Ready()
	{
	}
}
