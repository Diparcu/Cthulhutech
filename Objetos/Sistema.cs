using Godot;
using System;
using System.Collections.Generic;

public partial class Sistema : Node2D
{
	public static Font fuente;

	private SistemaEstado estado;
	private Evento eventoCargado;
	private Dia diaCargado;
	private BarraSuperiorUI barraSuperior;
	public List<Button> botonesPausa = new List<Button>();
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

	private void crearBotonesDeMenuDePausa(){
		Button botonGuardadoRapido = new Button();
		Button botonCargadoRapido = new Button();
		Button botonGuardado = new Button();
		Button botonCargar = new Button();
		Button botonOpciones = new Button();
		Button botonMenuPrincipal = new Button();
		Button botonSalida = new Button();

		botonGuardadoRapido.Text = "Guardado rapido";
		botonCargadoRapido.Text = "Cargado rapido";
		botonGuardado.Text = "Guardar partida";
		botonCargar.Text = "Cargar partida";
		botonOpciones.Text = "Opciones";
		botonMenuPrincipal.Text = "Menu principal";
		botonSalida.Text = "Cerrar juego";

		botonMenuPrincipal.Pressed += () => this.GetTree().ReloadCurrentScene();
		botonSalida.Pressed += () => this.cerrarLaWea();

		this.botonesPausa.Add(botonGuardadoRapido);
		this.botonesPausa.Add(botonCargadoRapido);
		this.botonesPausa.Add(botonGuardado);
		this.botonesPausa.Add(botonCargar);
		this.botonesPausa.Add(botonOpciones);
		this.botonesPausa.Add(botonMenuPrincipal);
		this.botonesPausa.Add(botonSalida);

		this.ordenarBotonesDeMenuDePausa();
	}

	private void cerrarLaWea()
	{
		this.GetTree().Quit();
	}

	private void ordenarBotonesDeMenuDePausa(){
		int index = 0;
		foreach(Button buton in this.botonesPausa){
			this.AddChild(buton);
			buton.Visible = false;
			buton.Position = new Vector2(16, 48*(index + 1));
			index++;
		}
	}

	public void setVisibilidadBotonesDePausa(bool siONo){
		foreach(Button buton in this.botonesPausa){
			buton.Visible = siONo;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.crearBotonesDeMenuDePausa();
		this.estado = new SistemaEstadoMenuPrincipal(this);

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
			case "Manual":
				personaje.Fuerza += 5;
				break;
			case "Emocional":
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
				this.AddChild(new Dia0Blanco());
				break;
			case "Niño bajos fondos":
				this.AddChild(new Dia0BajosFondos());
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
		this.AddChild(new Dia0Blanco());
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
		this.AddChild(new Dia0BajosFondos());
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
		if(!this.estado.hojaDePersonajeAbrible) return;
		this.setEstado(new SistemaEstadoEnHojaDePersonaje(this));
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
