using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class Evento : Node2D 
{
	private const int MARGEN = 16;
	private const int POSICION_ORIGEN_RECUADRO_X = (1280/3)*2;
	private const int POSICION_ORIGEN_RECUADRO_Y = 32;
	private const int LONGITUD_HORIZONTAL_RECUADRO = (1280/3);
	private const int LONGITUD_VERTICAL_RECUADRO = 640;
	private const int FONT_SIZE = 24;
	private const int FUENTE_ANCHURA = -1;
	private HorizontalAlignment FUENTE_ORIENTACION = 0;

	protected List<Sprite2D> sprites = new List<Sprite2D>() ;
	protected List<Dialogo> dialogos = new List<Dialogo>(); 
	protected List<Dialogo> historialDialogos = new List<Dialogo>(); 
	protected RichTextLabel cajaDeTexto = new RichTextLabel();
	private List<OpcionDialogo> opciones = new List<OpcionDialogo>();
	protected Dia dia;

	private int index = 0;
	private float caracteres = 0;

	public Evento(Dia dia){
		this.dia = dia;
		this.inicializarCajaDeTexto();
	}

	public void comportamiento(double delta){
		this.avanzarCaracteres();
		this.moverSprites();
	}

	public void control(InputEvent @event){
		switch(@event.AsText()){
			case "Enter": 
			case "Space": 
			case "Left Mouse Button":
                if(GetGlobalMousePosition().Y < 32) return;
				this.avanzarDialogo();
			break;
		}

	}

	public void dibujar(Node2D sistema){
		//this.dibujarNombreDePersonaje(sistema);
		this.dibujarCajaDeTexto(sistema);
	}

	public void clickearOpcion(string meta){
		foreach(OpcionDialogo opcion in this.opciones){
			opcion.avanzarDialogo(meta);
			this.cajaDeTexto.Text = this.cajaDeTexto.Text.Replace(opcion.getReemplazable(), "");
			this.cajaDeTexto.Text = this.cajaDeTexto.Text.Replace("[/url]", "");
		}
	}

	private void inicializarCajaDeTexto(){
		this.cajaDeTexto.AddThemeFontOverride("normal_font", Sistema.fuente);
		this.cajaDeTexto.AddThemeFontSizeOverride("normal_font_size", FONT_SIZE);
		this.cajaDeTexto.BbcodeEnabled = true;
		this.cajaDeTexto.ScrollActive = true;
		this.cajaDeTexto.ScrollFollowing = true;
		this.cajaDeTexto.AutowrapMode = TextServer.AutowrapMode.Word;
		this.cajaDeTexto.Visible = true;
		this.cajaDeTexto.Connect("meta_clicked", new Callable(this, nameof(this.clickearOpcion)));
		this.cajaDeTexto.VisibleCharactersBehavior = TextServer.VisibleCharactersBehavior.CharsAfterShaping;
		this.cajaDeTexto.VisibleCharacters = 0;
		this.cajaDeTexto.SetSize(new Vector2( LONGITUD_HORIZONTAL_RECUADRO - MARGEN*2, LONGITUD_VERTICAL_RECUADRO/10 * 9 - MARGEN));
		this.cajaDeTexto.Position = new Vector2(MARGEN + POSICION_ORIGEN_RECUADRO_X, POSICION_ORIGEN_RECUADRO_Y );
		this.AddChild(this.cajaDeTexto);
	}

	private Color getColorForPersonaje(string personaje){
		switch(personaje){
			case "Shinji":
				return new Color(0.529f, 0.808f, 0.922f); // LightSkyBlue
			case "Jugador":
				return new Color(0.565f, 0.933f, 0.565f); // LightGreen
			default:
				return Colors.White;
		}
	}

	private string getColorNameForPersonaje(string personaje){
		switch(personaje){
			case "Shinji":
				return "lightblue";
			case "Jugador":
				return "lightgreen";
			default:
				return "white";
		}
	}

	protected void cargarTexto(){
		string personaje = this.dialogos[this.index].getPersonaje();
		string dialogo = this.dialogos[this.index].getDialogo();
		string colorName = getColorNameForPersonaje(personaje);
		this.cajaDeTexto.Text += $"\n\n[color={colorName}]{(personaje == Dialogo.DEFAULT ? "" : personaje + ": ")}{dialogo}[/color]";
		if(personaje != Dialogo.DEFAULT) this.caracteres += personaje.Length;
	}

	private void avanzarCaracteres(){
		if(this.cajaDeTexto.GetParsedText().Length < this.cajaDeTexto.VisibleCharacters) return;
		this.caracteres += 0.5f;
		this.cajaDeTexto.VisibleCharacters = (int)this.caracteres;
	}

	    public Personaje getJugador(){
			return this.dia.getJugador();
		}
	
	    public Sistema getSistema(){
	        return this.dia.getSistema();
	    }
		public AudioStreamPlayer getAudioStreamer(){
		return this.dia.getAudioStreamer();
	}

	private void mostrarOpciones(){
		if(this.dialogos[this.index].getTipo() != Dialogo.DESICION) return;
		if(this.cajaDeTexto.VisibleCharacters < this.dialogos[this.index].getDialogo().Length) return;
		if(this.opciones.Count != 0) return;
		this.crearDialogoDeOpciones();
	}

	private void crearDialogoDeOpciones(){
		int botonIndex = 0;
		string opcionesString = "";
		foreach(OpcionDialogo opcion in this.dialogos[this.index].getOpciones()){
			if(opcion.getVisto() && !opcion.getRepetible()) continue;
			opcion.setEvento(this);
			this.opciones.Add(opcion);
			opcionesString += "[url="+opcion.getDescripcion().Replace("[","").Replace("]","")+"][color='red']" + (botonIndex + 1) + ".- " + opcion.getDescripcion() + "[/color][/url]\n";
			opcion.setReemplazable("[url="+opcion.getDescripcion().Replace("[","").Replace("]","")+"]");
			botonIndex++;
		}
		opcionesString = opcionesString.Substring(0, opcionesString.Length - 1);
		this.dialogos.Add(new Dialogo(opcionesString));
		this.index++;
		this.cargarTexto();
		this.caracteres = this.cajaDeTexto.GetParsedText().Length;
	}

	private void addOpcion(OpcionDialogo opcion, int index){
		opcion.addOpcion(this.cajaDeTexto, index);
	}

	private void avanzarDialogo(){
		bool isTextRevealing = this.cajaDeTexto.VisibleCharacters < this.cajaDeTexto.GetParsedText().Length;

		if (isTextRevealing)
		{
			this.cajaDeTexto.VisibleCharacters = this.cajaDeTexto.GetParsedText().Length;
			this.caracteres = this.cajaDeTexto.GetParsedText().Length;
		}
		else
		{
			//if(this.dialogos[this.index].getTipo() == Dialogo.DESICION) return;
			this.terminarDeMoverSprites();
			this.comprobarFinalDeDialogo();
			this.cambiarMusica();
		}
	}

	private void comprobarFinalDeDialogo(){
		if(this.dialogos[index].getFinal()){
			this.dia.avanzarDia();
			return;
		}
		this.continuarDialogo();
	}

	private void continuarDialogo(){
		if(this.dialogos.Count - 1 == this.index){
			this.mostrarOpciones();
			return;
		}
		this.index++;
		this.dialogos[this.index].executeAction();
		this.cargarTexto();
	}

	public void reemplazarDialogoPostCheckeo(List<Dialogo> dialogos){
		this.cajaDeTexto.VisibleCharacters += dialogos[0].getDialogo().Length;
		this.reemplazarDialogo(dialogos);
	}

	public void reemplazarDialogo(List<Dialogo> dialogos){
		this.opciones = new List<OpcionDialogo>();
		this.dialogos[index].pasarMovimiento(dialogos[0]);
		this.dialogos.AddRange(dialogos); 
		this.index++;
		this.dialogos[this.index].executeAction();
		this.cargarTexto();
	}

	private void dibujarCajaDeTexto(Node2D sistema){
		sistema.DrawRect(
				new Rect2(POSICION_ORIGEN_RECUADRO_X,
					POSICION_ORIGEN_RECUADRO_Y,
					LONGITUD_HORIZONTAL_RECUADRO,
					LONGITUD_VERTICAL_RECUADRO),
				Colors.Black
				);     
	}

	private void dibujarNombreDePersonaje(Node2D sistema){
		String personaje = this.dialogos[this.index].getPersonaje();
		if(personaje == Dialogo.DEFAULT) return;

		Color color = getColorForPersonaje(personaje);

		sistema.DrawRect(new Rect2(POSICION_ORIGEN_RECUADRO_X, POSICION_ORIGEN_RECUADRO_Y - FONT_SIZE - MARGEN,
					(personaje.Length * FONT_SIZE * 0.65f) + MARGEN,
					FONT_SIZE + MARGEN),
					Colors.Black);     

		sistema.DrawString(Sistema.fuente,
				new Vector2(POSICION_ORIGEN_RECUADRO_X + MARGEN, POSICION_ORIGEN_RECUADRO_Y - FONT_SIZE/2),
				personaje,
				FUENTE_ORIENTACION,
				FUENTE_ANCHURA,
				FONT_SIZE,
				color);
	}
	 
	private void moverSprites(){
		this.dialogos[index].mover();
	}

	private void terminarDeMoverSprites(){
		this.dialogos[index].terminarMovimiento();
		if(this.index < this.dialogos.Count - 1)
			this.dialogos[index].pasarMovimiento(this.dialogos[index + 1]);
	}

	public void cargarCancion(String cancion){
		AudioStream miCancion = (AudioStream)ResourceLoader.Load(cancion);
		this.getAudioStreamer().Stream = miCancion;
		this.getAudioStreamer().Play();
	}

	private void cambiarMusica(){
		this.dialogos[index].cambiarMusica(this.getAudioStreamer());
	}

    public ResultadoTirada realizarTiradaCombinacion(string habilidad, string atributo, int dificultad)
    {
        Personaje jugador = this.getJugador();
        ResultadoTirada resultado = new ResultadoTirada();
        var rand = new Random();

        int valorHabilidad = (int)jugador.GetType().GetProperty(habilidad).GetValue(jugador);
        int valorAtributo = (int)jugador.GetType().GetProperty(atributo).GetValue(jugador);

        int numeroDados = (int)Math.Ceiling(valorAtributo / 2.0) + valorHabilidad;

        for (int i = 0; i < numeroDados; i++)
        {
            resultado.DadosLanzados.Add(rand.Next(1, 11));
        }
        resultado.DadosLanzados.Sort();

        // --- Lógica de Combinaciones ---
        List<ResultadoTirada> combinacionesEncontradas = new List<ResultadoTirada>();

        // 1. Buscar Pares, Tríos, Cuartetos
        var grupos = resultado.DadosLanzados.GroupBy(d => d).Where(g => g.Count() >= 2).ToList();
        foreach (var grupo in grupos)
        {
            ResultadoTirada comb = new ResultadoTirada();
            if (grupo.Count() >= 4) { comb.CombinacionNombre = "Cuarteto"; comb.DadosEnCombinacion = grupo.Take(4).ToList(); }
            else if (grupo.Count() >= 3) { comb.CombinacionNombre = "Trío"; comb.DadosEnCombinacion = grupo.Take(3).ToList(); }
            else if (grupo.Count() >= 2) { comb.CombinacionNombre = "Par"; comb.DadosEnCombinacion = grupo.Take(2).ToList(); }
            comb.SumaCombinacion = comb.DadosEnCombinacion.Sum();
            combinacionesEncontradas.Add(comb);
        }

        // 2. Buscar Escaleras
        List<int> unicos = resultado.DadosLanzados.Distinct().ToList();
        for (int i = 0; i < unicos.Count; i++)
        {
            List<int> escaleraActual = new List<int> { unicos[i] };
            for (int j = i + 1; j < unicos.Count; j++)
            {
                if (unicos[j] == escaleraActual.Last() + 1)
                {
                    escaleraActual.Add(unicos[j]);
                }
                else
                {
                    break;
                }
            }
            if (escaleraActual.Count >= 3)
            {
                ResultadoTirada comb = new ResultadoTirada();
                comb.CombinacionNombre = "Escalera";
                comb.DadosEnCombinacion = escaleraActual;
                comb.SumaCombinacion = comb.DadosEnCombinacion.Sum();
                combinacionesEncontradas.Add(comb);
            }
        }

        // --- Calcular Resultado ---
        if (combinacionesEncontradas.Count > 0)
        {
            ResultadoTirada mejorCombinacion = combinacionesEncontradas.OrderByDescending(c => c.SumaCombinacion).First();
            resultado.CombinacionNombre = mejorCombinacion.CombinacionNombre;
            resultado.SumaCombinacion = mejorCombinacion.SumaCombinacion;
            resultado.DadosEnCombinacion = mejorCombinacion.DadosEnCombinacion;
        }
        else
        {
            resultado.CombinacionNombre = "Dado más alto";
            resultado.SumaCombinacion = resultado.DadosLanzados.Count > 0 ? resultado.DadosLanzados.Max() : 0;
            if (resultado.SumaCombinacion > 0)
                resultado.DadosEnCombinacion.Add(resultado.SumaCombinacion);
        }

        resultado.Exito = resultado.SumaCombinacion >= dificultad;
        return resultado;
    }
}
