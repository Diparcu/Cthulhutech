using Godot;
using System;
using System.Collections.Generic;

public partial class EstadoDiaManana : EstadoDia
{
	public EstadoDiaManana(Dia dia): base(dia)
	{
		this.periodoDelDia = EstadoDia.PERIODO_MANANA;
	}

	public override void avanzarDia(){
		this.dia.cargarMapa();
		this.dia.setEstado(new EstadoDiaTarde(this.dia));
	}

	public override void comportamiento(double delta){
		this.dia.getEventoCargado().comportamiento(delta);
	}

	public override void control(InputEvent @event){
		this.dia.getEventoCargado().control(@event);
	}

	public override void dibujar(Node2D sistema){
		this.dia.getEventoCargado().dibujar(sistema);
	}
}
