using Godot;
using System;

public partial class Sistema : Node2D
{
	public static Font fuente;

	private SistemaEstado estado;
	private Evento eventoCargado;
	private Dia diaCargado;
	private BarraSuperiorUI barraSuperior;
	private int numeroDia = 1;

	private Personaje jugador;
	private AudioStreamPlayer audioStream;
	private TextureRect fondo;
	 
	public const int TAMANO_FUENTE = 32;
	public static readonly HorizontalAlignment FUENTE_ORIENTACION = 0;
	public static readonly int FUENTE_ANCHURA = -1;
	 
	public void dibujarDiaCargado(){
		this.diaCargado.dibujar(this);
	}

	public void inputDiaCargado(InputEvent @event){
		this.diaCargado.control(@event);
	}

	public void comportamientoDiaCargado(double delta){
		this.diaCargado.comportamiento(delta);
	}

	public AudioStreamPlayer getAudioStreamer(){
		return this.audioStream;
	}

	public void setEstado(SistemaEstado estado){
		this.estado = estado;
	}

	public void cargarEscena(Dia dia){
		if(this.diaCargado != null) this.diaCargado.QueueFree();
		this.diaCargado = dia;
		this.diaCargado.NumeroDia = this.numeroDia;
		this.AddChild(this.diaCargado);
		if (this.barraSuperior != null)
		{
			this.barraSuperior.ActualizarUI();
		}
	}

	public void cambiarFondo(string rutaFondo){
		fondo.Texture = GD.Load<Texture2D>(rutaFondo);
	}

	public void cambiarFondoEscalado(string rutaFondo, TextureRect.StretchModeEnum stretchMode)
	{
		fondo.Texture = GD.Load<Texture2D>(rutaFondo);
		fondo.StretchMode = stretchMode;
	}

	public void restaurarModoFondo()
	{
		fondo.StretchMode = TextureRect.StretchModeEnum.Scale;
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

	public Dia getDiaCargado()
	{
		return this.diaCargado;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.estado = new SistemaEstadoMenuPrincipal(this);

		this.fondo = new TextureRect();
		this.fondo.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		this.fondo.StretchMode = TextureRect.StretchModeEnum.Scale;
		this.fondo.ZIndex = -1;
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
		if(this.barraSuperior != null)
		{
			this.barraSuperior.QueueFree();
			this.barraSuperior = null;
		}
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

	public void iniciarSeleccionOrigen(Node2D nodo){
		nodo.QueueFree();
		this.AddChild(new SeleccionOrigen(this));
	}

	public void iniciarSeleccionArquetipo(Node2D nodo, string origen){
		nodo.QueueFree();
		this.AddChild(new SeleccionArquetipo(this, origen));
	}

	public void iniciarJuego(Node2D nodo, string origen, string arquetipo){
		nodo.QueueFree();
		this.estado = new SistemaEstadoJugando(this);
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
		Personaje personaje = new Personaje();
		personaje.origen = origen;
		personaje.arquetipo = arquetipo;

		// Aplicar bonificaciones de estadísticas
		switch (origen)
		{
			case "Niño isleño":
				personaje.Supervivencia += 5;
				break;
			case "Niño blanco":
				personaje.CienciasOcultas += 5;
				break;
			case "Niño bajos fondos":
				personaje.Sigilo += 5;
				break;
		}

		switch (arquetipo)
		{
			case "Intelectual":
				personaje.Inteligencia += 5;
				break;
			case "Bruto":
				personaje.Fuerza += 5;
				break;
			case "Social":
				personaje.Presencia += 5;
				break;
		}

		this.inicializarJugador(personaje);

		// Iniciar el día 0 correspondiente
		switch (origen)
		{
			case "Niño isleño":
				this.cargarEscena(new Dia0Isla(this));
				break;
			case "Niño blanco":
				this.cargarEscena(new Dia0Blanco(this));
				break;
			case "Niño bajos fondos":
				this.cargarEscena(new Dia0BajosFondosTemprano(this));
				break;
		}
	}

	public void iniciarJuegoIsla(Node2D nodo){
		nodo.QueueFree();
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
		Personaje personaje = new Personaje();
		personaje.Dinero = 10;
		this.inicializarJugador(personaje);
		this.cargarEscena(new Dia0Isla(this));
	}

	public void iniciarJuegoNormal(Node2D nodo){
		nodo.QueueFree();
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
		Personaje personaje = new Personaje();
		personaje.Dinero = 50;
		this.inicializarJugador(personaje);
		this.cargarEscena(new DiaNormal(this));
	}

	public void iniciarJuegoPudiente(Node2D nodo){
		nodo.QueueFree();
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
		Personaje personaje = new Personaje();
		personaje.Dinero = 100;
		this.inicializarJugador(personaje);
		this.cargarEscena(new DiaPudiente(this));
	}

	public void iniciarJuegoBlanco(Node2D nodo){
		nodo.QueueFree();
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
		Personaje personaje = new Personaje();
		this.inicializarJugador(personaje);
		this.cargarEscena(new Dia0Blanco(this));
	}

	public void iniciarJuegoBajosFondos(Node2D nodo){
		nodo.QueueFree();
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
		Personaje personaje = new Personaje();
		this.inicializarJugador(personaje);
		this.cargarEscena(new Dia0BajosFondosTemprano(this));
	}

	public void iniciarJuegoMegan(Node2D nodo){
		nodo.QueueFree();
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
		this.inicializarJugador(new Personaje());
		this.cargarEscena(new DiaMegan(this));
	}
		public void iniciarJuegoKC(Node2D nodo){
		nodo.QueueFree();
		if (this.barraSuperior == null)
		{
			this.barraSuperior = new BarraSuperiorUI(this);
			this.AddChild(this.barraSuperior);
		}
		this.numeroDia = 1;
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
		this.numeroDia++;
		this.cargarEscena(new DiaIsla(this));
	}

	public void mostrarHojaDePersonaje(){
		var hoja = new HojaDePersonaje(this);
		hoja.TreeExiting += () => {
			if (this.barraSuperior != null)
			{
				this.barraSuperior.ActualizarUI();
			}
		};
		this.AddChild(hoja);
	}

	public void GameOver(){
		if(this.diaCargado != null) this.diaCargado.QueueFree();
		this.diaCargado = null;
		this.AddChild(new PantallaDeInicio(fuente, this));
	}

	public override void _Process(double delta)
	{
		this.QueueRedraw();
		this.estado.comportamiento(this, delta);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.IsReleased()) return;
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.IsReleased()) return;
		this.estado.input(this, @event);
	}

	public override void _Draw(){
		this.estado.dibujar(this);
	}
}
