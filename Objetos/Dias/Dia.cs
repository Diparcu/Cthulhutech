using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Dia : Node2D
{

	public static List<string> FASES_DEL_DIA = new List<string> { 
		"Clase",
		"Almuerzo",
		"Entrenamiento",
		"Tarde",
		"Noche"
	};

	private Dictionary<string, string> CONVERSION_FASES = new Dictionary<string, string> {
		{ TablaDeEventos.CLASE, TablaDeEventos.PREVIO_CLASE},
		{ TablaDeEventos.ALMUERZO, TablaDeEventos.PREVIO_ALMUERZO},
		{ TablaDeEventos.ENTRENAMIENTO, TablaDeEventos.PREVIO_ENTRENAMIENTO},
		{ TablaDeEventos.TARDE, TablaDeEventos.PREVIO_TARDE},
		{ TablaDeEventos.NOCHE, TablaDeEventos.PREVIO_NOCHE}
	};

	public int NumeroDia { get; set; } = 0;
	protected int faseDelDiaActual = 0;
	private EstadoDia estado;
	private Sistema sistema;

	protected TablaDeEventos eventos = new TablaDeEventos();

	protected Evento eventoCargado;
	
	public Dia(Sistema sistema){
		this.sistema = sistema;
		this.estado = new EstadoDiaManana(this);
	}

	public string getFaseDiaActual(){
		return FASES_DEL_DIA[this.faseDelDiaActual];
	}

	private string getFaseDiaConvertida(){
		return this.CONVERSION_FASES[FASES_DEL_DIA[this.faseDelDiaActual]];
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

	public void agregarEventoAPool(AgregarEntradaEvento entradaEvento){
		this.eventos.agregarEvento(entradaEvento.Entrada, entradaEvento.Pool);
	}

	private void resetFlagDiarias(){
		this.getFlags().resetFlagsDiarias();
	}

	public void avanzarDia(string subFase){
		if(subFase != null){
			this.cambioDeEvento(subFase);
			return;
		}
		this.faseDelDiaActual++;
		if(this.faseDelDiaActual >= FASES_DEL_DIA.Count){
			this.resetFlagDiarias();
			this.faseDelDiaActual = 0;
			this.NumeroDia++;
		}
		this.avanzarEvento();
	}

	public void avanzarEvento(){
		Type proximoEvento = this.eventoCargado.getProximoEvento();
		if(proximoEvento == null) proximoEvento = this.getProximoEvento(this.getFaseDiaConvertida());
		this.instanciarEventoProximo(proximoEvento);
	}

	public void cambioDeEvento(string subFase){
		if(subFase == null) subFase = this.getFaseDiaActual();
		Type proximoEvento = this.eventoCargado.getProximoEvento();
		if(proximoEvento == null) proximoEvento = this.getProximoEvento(subFase);
		this.eventoCargado.cambiarEventoPredeterminado(proximoEvento);
	}

	private Type getProximoEvento(string subFase){
		return this.eventos.getProximoEvento(this.getFlags(),
				subFase,
				this.NumeroDia);
	}

	public void cambiarEvento(Evento evento){
		this.eventoCargado = evento;
	}

	public void instanciarEventoProximo(Type proximoEvento){
		//GD.Print(proximoEvento);
		this.eventoCargado.QueueFree();
		this.eventoCargado = (Evento)Activator.CreateInstance(
				proximoEvento,
				this);
		this.AddChild(this.eventoCargado);
	}

	public void iniciarAvanzeFaseDelDia(String subFase){
		this.sistema.setEstado(new EstadoSistemaTransicionDia(
					this.sistema,
					subFase,
					this.generarMensajeDia(this.faseDelDiaActual),
					this.generarMensajeDia(this.faseDelDiaActual + 1)
					));
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
