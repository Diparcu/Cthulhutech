using Godot;
using System;
using System.Collections.Generic;

public partial class DiaClaseMVP : Dia
{
	public DiaClaseMVP(Sistema sistema) : base(sistema)
	{
		this.cargarEvento(new EventoSeleccionAsiento(this));
		this.setEstado(new EstadoDiaEnEvento(this));
	}

	private void cargarEvento(Evento evento)
	{
		if(this.eventoCargado != null) this.eventoCargado.QueueFree();
		this.eventoCargado = evento;
		this.AddChild(this.eventoCargado);
	}
}
