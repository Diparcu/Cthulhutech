using Godot;
using System;

public partial class Sistema : Node2D
{
	public static Font fuente;

	private SistemaEstado estado;
	private Evento eventoCargado;
	private Dia diaCargado;

	private Personaje jugador;
	private AudioStreamPlayer audioStream;
	private TextureRect fondo;
	 
	public const int TAMANO_FUENTE = 32;
	public static readonly HorizontalAlignment FUENTE_ORIENTACION = 0;
	public static readonly int FUENTE_ANCHURA = -1;
	 
	public AudioStreamPlayer getAudioStreamer(){
		return this.audioStream;
	}

	public void setEstado(SistemaEstado estado){
		this.estado = estado;
	}

	public void cargarEscena(Dia dia){
		if(this.diaCargado != null) this.diaCargado.QueueFree();
		this.diaCargado = dia;
		this.AddChild(this.diaCargado);
	}

	public void cambiarFondo(string rutaFondo){
		fondo.Texture = GD.Load<Texture2D>(rutaFondo);
	}

	public void inicializarAudioStreamer(){
		this.audioStream = new AudioStreamPlayer();
		this.AddChild(this.audioStream);
	}

	public void inicializarFuente(){
		fuente = GD.Load<Font>("res://Fuentes/Roboto.ttf");
	}

	public void inicializarJugador(Personaje personaje){
		this.jugador = personaje;
	}

	public Personaje getJugador(){
		return this.jugador;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.fondo = new TextureRect();
		this.fondo.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
		this.AddChild(this.fondo);

		this.inicializarFuente();
		this.inicializarAudioStreamer();
		this.AddChild(new PantallaDeInicio(fuente, this));
	}

	public void irAPantallaDeInicio(Node2D nodo){
		nodo.QueueFree();
		this.AddChild(new PantallaDeInicio(fuente, this));
	}

	public void volverAlMenuPrincipal(){
		if(this.diaCargado != null) this.diaCargado.QueueFree();
		this.diaCargado = null;
		this.AddChild(new PantallaDeInicio(fuente, this));
	}

	public void volverAlMenuPrincipalDesdeMenu(Node2D nodo){
		nodo.QueueFree();
		this.AddChild(new PantallaDeInicio(fuente, this));
	}

	public void iniciarCreadorDePersonaje(Node2D nodo){
		nodo.QueueFree();
		this.AddChild(new CreadorDePersonaje(this));
	}

	public void iniciarJuegoIsla(Node2D nodo){
		nodo.QueueFree();
		Personaje personaje = new Personaje();
		personaje.Dinero = 10;
		this.inicializarJugador(personaje);
		this.cargarEscena(new Dia0Isla(this));
	}

	public void iniciarJuegoNormal(Node2D nodo){
		nodo.QueueFree();
		Personaje personaje = new Personaje();
		personaje.Dinero = 50;
		this.inicializarJugador(personaje);
		this.cargarEscena(new DiaNormal(this));
	}

	public void iniciarJuegoPudiente(Node2D nodo){
		nodo.QueueFree();
		Personaje personaje = new Personaje();
		personaje.Dinero = 100;
		this.inicializarJugador(personaje);
		this.cargarEscena(new DiaPudiente(this));
	}

	public void iniciarJuegoBlanco(Node2D nodo){
		nodo.QueueFree();
		Personaje personaje = new Personaje();
		this.inicializarJugador(personaje);
		this.AddChild(new Dia0Blanco());
	}

	public void iniciarJuegoBajosFondos(Node2D nodo){
		nodo.QueueFree();
		Personaje personaje = new Personaje();
		this.inicializarJugador(personaje);
		this.AddChild(new Dia0BajosFondos());
	}

	public void iniciarJuegoMegan(Node2D nodo){
		nodo.QueueFree();
		this.inicializarJugador(new Personaje());
		this.cargarEscena(new DiaMegan(this));
	}
		public void iniciarJuegoKC(Node2D nodo){
		nodo.QueueFree();
		this.inicializarJugador(new Personaje());
		this.cargarEscena(new DiaKC(this));
	}

	public void iniciarCreditos(Node2D nodo){
		nodo.QueueFree();
		this.cargarEscena(new DiaCreditos(this));
	}

	public void iniciarOpciones(Node2D nodo){
		nodo.QueueFree();
		this.AddChild(new PantallaDeOpciones(fuente, this));
	}

	public void iniciarCasaJugadorIsla(Node2D nodoActual){
		if(nodoActual != null) nodoActual.QueueFree();
		this.AddChild(new EventoCasaJugadorIsla(this));
	}

	public void avanzarAlSiguienteDia(Node2D nodoActual){
		nodoActual.QueueFree();
		this.cargarEscena(new DiaIsla(this));
	}

	public void GameOver(){
		if(this.diaCargado != null) this.diaCargado.QueueFree();
		this.diaCargado = null;
		this.AddChild(new PantallaDeInicio(fuente, this));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.QueueRedraw();
		if(this.diaCargado != null) this.diaCargado.comportamiento(delta);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.IsReleased()) return;
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.IsReleased()) return;
		if(this.diaCargado != null) this.diaCargado.control(@event);
	}

	public override void _Draw(){
		if(this.diaCargado != null) this.diaCargado.dibujar(this);
	}
}
