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
		List<int> dados = tirarDados(jugador);
		int resultado = getResultado(dados);

		List<Dialogo> dialogo = generarMensajeDeTirada(resultado, dados);
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

	private List<int> tirarDados(Personaje jugador){

		int cantidadDeDados = this.getValorHabilidad(jugador);
		List<int> resultados = new List<int>();

		var rand = new Random();

		for(int i = 0; i < cantidadDeDados; i++){
			resultados.Add(rand.Next(10) + 1);
		}

		resultados.Sort();
		return resultados;
	}

	private int getResultado(List<int> resultados){
		int resultadoMasAlto = 0;

		resultadoMasAlto = Math.Max(
				this.getResultadoNumeroRepetido(resultados),
				this.getResultadoEscala(resultados));

		return resultadoMasAlto;
	}

	private int getResultadoEscala(List<int> resultados){
		List<int> lista = resultados.Distinct().ToList();
		int total = 0, totalTemporal = 0, iteraciones = 0;

		for(int i = 0; i < lista.Count - 1; i++){
			totalTemporal = lista[i];
			iteraciones = 1;
			for(int j = i; j < lista.Count - 1; j++){
				if(lista[j] + 1 == lista[j + 1]){
					totalTemporal += lista[j + 1];
					iteraciones++;
				}else{
					break;
				}
			}
			if(total < totalTemporal && iteraciones >= 3) total = totalTemporal;
		}

		return total;
	}

	private int getResultadoNumeroRepetido(List<int> resultados){
		int totalTemporal, total = 0;
		for(int i = 1; i <= 10; i++){
			totalTemporal = this.getResultadoNumeroRepetidoSumatoria(resultados, i);
			if(total < totalTemporal) total = totalTemporal;
		}
		return total;
	}

	private int getResultadoNumeroRepetidoSumatoria(List<int> resultados, int numero){
		int total = 0;
		foreach(int i in resultados){
			if(numero == i) total += i;
		}
		return total;
	}

	private int getValorHabilidad(Personaje jugador){
		Type tipo = typeof(Personaje);
		PropertyInfo propiedad = tipo.GetProperty(this.habilidad);
		return (int)Math.Ceiling((double)propiedad.GetValue(jugador).ToString().ToInt());
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
