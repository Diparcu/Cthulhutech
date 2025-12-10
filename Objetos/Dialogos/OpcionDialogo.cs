using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public partial class OpcionDialogo: Node2D
{
	public const String PASIVO = "Pasiva";
	public const String ACTIVO = "Activa";

	public const String FUERZA = "Fuerza";
	public const String TENACIDAD = "Tenacidad";

	private Button boton = new Button();
	private Evento evento;

	int dificultad = 0;
	private bool repetible = true;
	private bool visto = false;
	private String tipo = PASIVO;
	private String descripcion = "Default";
	private String habilidad = "Defecto";
	private String reemplazable;
	private List<Dialogo> siguienteDialogo;
	private List<Dialogo> siguienteDialogoFallo;

	public OpcionDialogo(String descripcion){
		this.descripcion = descripcion;
		//this.crearBoton();
	}

	public OpcionDialogo(String descripcion,
			int dificultad,
			String habilidad){

		this.dificultad = dificultad;
		this.habilidad = habilidad;
		this.tipo = ACTIVO;
		this.descripcion = "[" + habilidad + " " + dificultad +"] " +descripcion;
		//this.crearBoton();
	}

	public void addOpcion(RichTextLabel label, int index){
		index += 1;
		label.Text += "\n[url="+this.descripcion.Replace("[","").Replace("]","")+"][color='red']" + index + ".- " + this.descripcion + "[/color][/url]";
		label.VisibleCharacters += ("\n[url="+this.descripcion.Replace("[","").Replace("]","")+"][color='red']" + index + ".- " + this.descripcion + "[/color][/url]").Length;
	}

	public string getDescripcion(){
		return this.descripcion;
	}

	public void setReemplazable(string reemplazable){
		this.reemplazable = reemplazable;
	}

	public string getReemplazable(){
		return this.reemplazable;
	}

	public void setEvento(Evento evento){
		this.evento = evento;
	}

	public void avanzarDialogo(string meta){
		if(this.descripcion.Replace("[","").Replace("]","") != meta) return;
		this.visto = true;
		if(this.tipo == ACTIVO)
			this.checkeoHabilidad();
		else
			this.evento.reemplazarDialogo(this.siguienteDialogo);
	}

	private void checkeoHabilidad(){

		Personaje jugador = this.evento.getJugador();
        ResultadoTirada tirada = new ResultadoTirada();

        tirada.checkeoHabilidad(jugador, this.habilidad, this.dificultad);

		List<Dialogo> dialogo = generarMensajeDeTirada(tirada.SumaCombinacion, tirada.DadosLanzados);
		this.evento.reemplazarDialogo(dialogo);
	}

	private List<Dialogo> generarMensajeDeTirada(int resultado, List<int> dados){
		List<Dialogo> dialogo; 
		String mensaje = "Dados: -";

		foreach(int i in dados){
			mensaje = mensaje + i + " - ";
		}

		mensaje += "\n";
		mensaje += "Total: " + resultado;

		if(this.dificultad <= resultado){
			dialogo = new List<Dialogo>(this.siguienteDialogo); 
			mensaje += "\nExito.";
		}else{
			dialogo = new List<Dialogo>(this.siguienteDialogoFallo); 
			mensaje += "\nFallo.";
		}

		dialogo.Insert(0, new Dialogo(mensaje));
		return dialogo;
	}

	private void crearBoton(){
		StyleBoxFlat style = new StyleBoxFlat();
		style.BgColor = Colors.Black;

		this.boton.Text = this.descripcion;
		this.boton.Visible = true;
		this.boton.AddThemeStyleboxOverride("normal", style);
		this.boton.AddThemeStyleboxOverride("hover", style);
		this.boton.AddThemeFontSizeOverride("font_size", 32);
		this.boton.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
	}

	public Button getBoton(){
		return this.boton;
	}

	public bool getRepetible(){
		return this.repetible;
	}

	public bool getVisto(){
		return this.visto;
	}

	public OpcionDialogo setSiguienteDialogo(List<Dialogo> dialogo){
		this.siguienteDialogo = dialogo;
		return this;
	}

	public OpcionDialogo setSiguienteDialogoFallo(List<Dialogo> dialogo){
		this.siguienteDialogoFallo = dialogo;
		return this;
	}

	public OpcionDialogo setRepetible(bool repetible){
		this.repetible = repetible;
		return this;
	}
}
