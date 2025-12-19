using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Dia : Node2D
{
	public static List<string> FASES_DEL_DIA = new List<string> { 
		"Clase",
		"Almuerzo",
		"Entrenamiento",
		"Despues de clase",
		"Tarde",
		"Noche"
    };

	public int NumeroDia { get; set; } = 0;
	private int faseDelDiaActual = 0;
	private EstadoDia estado;
	private Sistema sistema;

	protected TablaDeEventos eventos = new TablaDeEventos();

	protected Evento eventoCargado;
	protected Evento eventoManana;//El evento de la mañana, me da miedo poner ñ's en el codigo.

	//Pull de eventos de la tarde:
	protected List<Evento> pullEventosTiendaCartas = new List<Evento>();
	protected List<Evento> pullEventosBarGabriel = new List<Evento>();

	//Pull de eventos de la noche:
	
	public Dia(Sistema sistema){
		this.sistema = sistema;
		this.estado = new EstadoDiaManana(this);
	}

	public string getFaseDiaActual(){
		return FASES_DEL_DIA[this.faseDelDiaActual];
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

	public String getPeriodoDelDia()
	{
		return FASES_DEL_DIA[this.faseDelDiaActual];
	}

	public void avanzarDia(){
		this.eventos.getProximoEvento(this.getFlags());
		this.faseDelDiaActual++;
		if(this.faseDelDiaActual >= FASES_DEL_DIA.Count){
			this.faseDelDiaActual = 0;
			this.NumeroDia++;
		}
	}

	public void iniciarAvanzeFaseDelDia(){
		this.eventoCargado.QueueFree();
		this.eventoCargado = (Evento)Activator.CreateInstance(
				this.eventoCargado.getProximoEvento(),
				this);
		this.sistema.setEstado(new EstadoSistemaTransicionDia(
					this.sistema,
					this.generarMensajeDia(this.faseDelDiaActual),
					this.generarMensajeDia(this.faseDelDiaActual + 1)
					));
		this.AddChild(this.eventoCargado);
	}

	private string generarMensajeDia(int faseDelDiaActual){
		if(faseDelDiaActual >= FASES_DEL_DIA.Count){
			faseDelDiaActual = 0;
			return "Día " + (this.NumeroDia + 1)+ ", " + FASES_DEL_DIA[faseDelDiaActual] + ".";
		}else{
			return "Día " + this.NumeroDia + ", " + FASES_DEL_DIA[faseDelDiaActual] + ".";
		}
	}

	public Personaje getJugador(){
		return this.sistema.getJugador();
	}

	public Flags getFlags(){
		return this.sistema.getJugador().Flags;
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
