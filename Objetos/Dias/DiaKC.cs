using Godot;
using System;
using System.Collections.Generic;

public partial class DiaKC : Dia
{
	public DiaKC(Sistema sistema) : base(sistema){
        if (this.getEventoCargado() != null)
        {
            this.getEventoCargado().QueueFree();
        }
        this.setEventoCargado(new EventoKC(this));
	}

	public override void _Ready()
	{
	}
}
