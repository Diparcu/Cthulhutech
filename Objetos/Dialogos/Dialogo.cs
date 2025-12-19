using Godot;
using System;
using System.Collections.Generic;

public class Dialogo 
{
	public const String DEFAULT = "Default";
	public const String DIALOGO = "Dialogo";
	public const String DESICION = "Decision";
	public const String CHECKEO_PASIVO = "Checkeo pasivo";

	private String tipo = DIALOGO;
	private String personaje = "Default";
	private String dialogo = "Default";
	private Type proximoEvento;
	private bool final = false;

	private Action onShow;

	List<MovimientoSprite> movimientos = new List<MovimientoSprite>() ;
	List<OpcionDialogo> opciones = new List<OpcionDialogo>() ;
	List<DialogoOpcional> dialogosOpcionales = new List<DialogoOpcional>();
	CambioDeMusica cambiosDeMusica;
	CambioDeFondo cambioDeFondo;

	int dificultad = 0;

	public Dialogo(String dialogo)
	{
		this.dialogo = dialogo;
	}

	public Dialogo(String personaje,
			String dialogo)
	{
		this.personaje = personaje;
		this.dialogo = dialogo;
	}

	public Dialogo addAction(Action action){
		this.onShow = action;
		return this;
	}

	public void executeAction(){
		this.onShow?.Invoke();
	}

	private void setCheckeoPasivo(int dificultad){
		this.tipo = CHECKEO_PASIVO;
		this.dificultad = dificultad;
	}

	public Dialogo setDesicion(List<OpcionDialogo> opciones){
		this.tipo = DESICION;
		this.opciones = opciones;
		return this;
	}

	public Dialogo setFinal(Type proximoEvento){
		this.final = true;
		this.proximoEvento = proximoEvento;
		return this;
	}

	public Type getProximoEvento(){
		return this.proximoEvento;
	}

	public Dialogo setFinal(){
		this.final = true;
		return this;
	}

	public bool getFinal(){
		return this.final;
	}

	public Dialogo addMovimiento(MovimientoSprite movimiento){
		this.movimientos.Add(movimiento);
		return this;
	}

	public Dialogo addCambioDeMusica(String cancion){
		this.cambiosDeMusica = new CambioDeMusica(cancion);
		return this;
	}

	public Dialogo addPararMusica(Evento evento){
		this.cambiosDeMusica = new CambioDeMusica();
		return this;
	}

	public void cambiarMusica(AudioStreamPlayer audioStream){
		if(this.cambiosDeMusica == null) return;
		this.cambiosDeMusica.cambiarMusica(audioStream);
	}

	public Dialogo addMovimiento(
			Sprite2D sprite,
			Vector2 posicion,
			float velocidad,
			bool perpetuo){
		this.movimientos.Add(new MovimientoSprite(
			sprite, posicion, velocidad, perpetuo));
		return this;
	}

	public Dialogo addMovimiento(
			Sprite2D sprite,
			Vector2 posicion,
			float velocidad){
		this.movimientos.Add(new MovimientoSprite(
			sprite, posicion, velocidad));
		return this;
	}

	public String getDialogo()
	{
		 return this.dialogo; 
	}

	public String getTipo()
	{
		 return this.tipo; 
	}

	public String getPersonaje()
	{
		 return this.personaje; 
	}

	public List<OpcionDialogo> getOpciones()
	{
		 return this.opciones; 
	}

	public void mover()
	{
		foreach(MovimientoSprite movimiento in this.movimientos){
			movimiento.mover();
		}
	}

	public void terminarMovimiento()
	{
		foreach(MovimientoSprite movimiento in this.movimientos){
			movimiento.terminarMovimiento();
		}
	}

	public void pasarMovimiento(Dialogo dialogo)
	{
		foreach(MovimientoSprite movimiento in this.movimientos){
			movimiento.pasarMovimientoFondo(dialogo);
		}
	}

	public Dialogo addCambioDeFondo(String fondo)
	{
		this.cambioDeFondo = new CambioDeFondo(fondo);
		return this;
	}

	public Dialogo addDialogoOpcional(DialogoOpcional dialogo)
	{
		this.dialogosOpcionales.Add(dialogo);
		return this;
	}

	public List<Dialogo> cargarDialogoOpcional(Evento evento){
		List<Dialogo> dialogo = new List<Dialogo>();
		foreach(DialogoOpcional dialogoOpcional in this.dialogosOpcionales){
			dialogo.AddRange(dialogoOpcional.checkeoDeHabilidad(evento.getJugador()));
		}
		return dialogo;
	}

	public void iniciarCambioDeFondo(Evento evento){
		if(this.cambioDeFondo == null) return;
		this.cambioDeFondo.iniciarCambioDeFondo(evento);
	}
}
