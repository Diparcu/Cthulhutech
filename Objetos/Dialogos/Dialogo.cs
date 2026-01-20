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
	private string subFase;
	private bool final = false;
	private bool avanzarDia = true;
	private bool cambioDeEvento = false;

	public bool CambioDeEvento { get { return this.cambioDeEvento;} } 
	public string SubFase { get { return this.subFase;} } 

	private Action onShow;

	private List<UpdateFlag> updateFlag = new List<UpdateFlag>();
	List<MovimientoSprite> movimientos = new List<MovimientoSprite>() ;
	List<OpcionDialogo> opciones = new List<OpcionDialogo>() ;
	List<DialogoOpcional> dialogosOpcionales = new List<DialogoOpcional>();
	List<CambioDeSprite> cambiosDeSprite = new List<CambioDeSprite>();
	List<AgregarEntradaEvento> entradasEvento = new List<AgregarEntradaEvento>();
	CambioDeMusica cambiosDeMusica;
	MovimientoCamara movimientoDeCamara;
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

	public Dialogo addDesicion(List<OpcionDialogo> opciones){
		this.setDesicion(opciones);
		return this;
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

	public Dialogo addMovimientoDeCamara(MovimientoCamara movimiento){
		this.movimientoDeCamara = movimiento;
		return this;
	}

	public Dialogo addCambioDeEvento(Type proximoEvento){
		this.cambioDeEvento = true;
		this.proximoEvento = proximoEvento;
		return this;
	}

	public Dialogo addCambioDeEvento(){
		this.cambioDeEvento = true;
		return this;
	}

	public Type getProximoEvento(){
		return this.proximoEvento;
	}

	public Dialogo setFinalNoAvanzarDia(){
		this.final = true;
		this.avanzarDia = false;
		return this;
	}

	public Dialogo addCambioDeEvento(string subFase){
		this.cambioDeEvento = true;
		this.subFase = subFase;
		return this;
	}

	public Dialogo setFinal(){
		this.final = true;
		return this;
	}

	public Dialogo setFinal(string subFase){
		this.final = true;
		this.subFase = subFase;
		return this;
	}

	public bool getAvanzarDia(){
		return this.avanzarDia;
	}

	public bool getFinal(){
		return this.final;
	}

	public Dialogo addCambioDeSprite(SpriteSet sprite, String spriteNuevo){
		this.cambiosDeSprite.Add(new CambioDeSprite(sprite, spriteNuevo));
		return this;
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

	public void cambiarSprites()
	{
		foreach(CambioDeSprite sprite in this.cambiosDeSprite){
			sprite.cambiarSprite();
		}
	}

	public void moverCamara(Sistema sistema)
	{
		if(this.movimientoDeCamara == null) return;
		this.movimientoDeCamara.mover(sistema);
	}

	public void updateFlags(Personaje jugador){
		foreach(UpdateFlag flag in this.updateFlag){
			jugador.updateFlag(flag.Flag);
		}
	}


	public void mover()
	{
		foreach(MovimientoSprite movimiento in this.movimientos){
			movimiento.mover();
		}
	}

	public void terminarMovimientoCamara(Sistema sistema)
	{
		if(this.movimientoDeCamara == null) return;
		this.movimientoDeCamara.terminarMovimiento(sistema);
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

	public Dialogo addFlagUpdate(String flag){
		this.updateFlag.Add(new UpdateFlag(flag));
		return this;
	}

	public Dialogo addFlagUpdate(String flag,
			bool value){
		this.updateFlag.Add(new UpdateFlag(flag, value));
		return this;
	}

	public List<AgregarEntradaEvento> getAgregarEntradaEventos(){
		return this.entradasEvento;
	}

	public Dialogo agregarEntradaEvento(string pool,
			EntradaTablaDeEventos entrada){
		this.entradasEvento.Add(new AgregarEntradaEvento(pool, entrada));
		return this;
	}
}
