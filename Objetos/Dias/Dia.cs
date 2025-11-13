using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Dia : Node2D
{
	private EstadoDia estado;
	private Sistema sistema;

	protected Evento eventoCargado;
	protected Evento eventoManana;//El evento de la mañana, me da miedo poner ñ's en el codigo.

	//Pull de eventos de la tarde:
	protected List<Evento> pullEventosTiendaCartas = new List<Evento>();
	protected List<Evento> pullEventosBarGabriel = new List<Evento>();

	//Pull de eventos de la noche:
	
	protected Dia proximoDia;

	    public Dia(Sistema sistema){
			this.sistema = sistema;
			this.estado = new EstadoDiaManana(this);
		}
		public void cargarMapa(){
		this.eventoCargado.QueueFree();
	}

	public Evento getEventoCargado(){
		return this.eventoCargado;
	}

	public void setEventoCargado(Evento evento){
		this.eventoCargado = evento;
		this.AddChild(evento);
	}

	public void setEstado(EstadoDia estado){
		this.estado = estado;
	}

	public void avanzarDia(){
		this.estado.avanzarDia();
	}

	public Personaje getJugador(){
		return this.sistema.getJugador();
	}

	public AudioStreamPlayer getAudioStreamer(){
		return this.sistema.getAudioStreamer();
	}

	public Sistema getSistema(){
		return this.sistema;
	}

	public void comportamiento(double delta){
		this.estado.comportamiento(delta);
	}

	public void control(InputEvent @event){
		this.estado.control(@event);
	}

	public void dibujar(Node2D sistema){
		this.estado.dibujar(sistema);
	}
}
