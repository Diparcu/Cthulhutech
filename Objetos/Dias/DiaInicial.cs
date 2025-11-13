using Godot;
using System;
using System.Collections.Generic;

public partial class DiaInicial : Dia
{
	public DiaInicial(Sistema sistema) : base(sistema){
		this.setEventoCargado(new EventoInicial(this));
	}

	public override void _Ready()
	{
	}
}
