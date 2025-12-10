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

	protected CanvasLayer canvas = new CanvasLayer();
	protected NodoMapa hubicacionHovereada;
	protected List<NodoMapa> hubicaciones = new List<NodoMapa>();

	private int index = 0;
	private int caracteresDeOpciones = 0;
	private int caracteresAntesDeLaOpcion = 0;
	private float caracteres = 0;

	public Evento(Dia dia){
		this.dia = dia;
		this.inicializarCajaDeTexto();
	}

	public void comportamiento(double delta){
		this.avanzarCaracteres();
		this.moverSprites();
		this.cambiarFondoLentamente();
		this.moverCamaraAHubicacionHovereada(delta);
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
		//this.dibujarCajaDeTexto(sistema);
	}

	public void moverCamaraAHubicacionHovereada(double delta){
		if(this.hubicacionHovereada == null) return;
		this.getSistema().moverCamara(this.hubicacionHovereada.Position, delta);
		this.QueueRedraw();
	}

	public void inicializarHubicaciones(){
		this.hubicaciones.Add(new NodoMapa("Mapa 1")
				.setPosition(new Vector2(64, 600)));
		this.hubicaciones.Add(new NodoMapa("Mapa 2")
				.setPosition(new Vector2(640, 200)));
		this.hubicaciones.Add(new NodoMapa("Mapa 3")
				.setPosition(new Vector2(200, 500)));
		this.hubicaciones.Add(new NodoMapa("Mapa 4")
				.setPosition(new Vector2(300, 300)));
		this.hubicaciones.Add(new NodoMapa("Mapa 5")
				.setPosition(new Vector2(1000, 500)));

		foreach(NodoMapa nodo in this.hubicaciones){
			this.AddChild(nodo);
		}
	}

	private void subrayarOpcionHovereada(string meta){
		this.removerTextoOpciones();
		this.reescribirOpcionesSubrayadas(meta);
	}

	private void reescribirOpcionesSubrayadas(string meta){
		int index = 0;
		string opcionString = "\n\n";
		foreach(OpcionDialogo opcion in this.opciones){
			if(meta == opcion.getDescripcion().Replace("[","").Replace("]","")){
				opcionString += "[u][url="+opcion.getDescripcion().Replace("[","").Replace("]","")+"][color='red']" + (index + 1) + ".- " + opcion.getDescripcion() + "[/color][/url][/u]\n";
			}else{
				opcionString += "[url="+opcion.getDescripcion().Replace("[","").Replace("]","")+"][color='red']" + (index + 1) + ".- " + opcion.getDescripcion() + "[/color][/url]\n";
			}
			index += 1;
		}
		this.cajaDeTexto.Text += opcionString;
	}

	private void removerTextoOpciones(){
		this.cajaDeTexto.Text = cajaDeTexto.Text.Substring(0,
				this.caracteresAntesDeLaOpcion);
	}

	public void hoverOpcion(string meta){
		this.subrayarOpcionHovereada(meta);
		NodoMapa tempNodo = this.hubicaciones.FirstOrDefault(p => p.nombre == meta);
		if(tempNodo == null) return;
		if(this.hubicacionHovereada != null) this.hubicacionHovereada.setSeleecionado(false);
		this.hubicacionHovereada = tempNodo;
		tempNodo.setSeleecionado(true);
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
		this.cajaDeTexto.Connect("meta_hover_started", new Callable(this, nameof(this.hoverOpcion)));
		this.cajaDeTexto.VisibleCharactersBehavior = TextServer.VisibleCharactersBehavior.CharsAfterShaping;
		this.cajaDeTexto.VisibleCharacters = 0;
		this.cajaDeTexto.MetaUnderlined = false;
		this.cajaDeTexto.SetSize(new Vector2( LONGITUD_HORIZONTAL_RECUADRO - MARGEN*2, LONGITUD_VERTICAL_RECUADRO/10 * 9 - MARGEN));
		this.cajaDeTexto.Position = new Vector2(MARGEN + POSICION_ORIGEN_RECUADRO_X, POSICION_ORIGEN_RECUADRO_Y );
		this.canvas = new CanvasLayer();
		this.canvas.Visible = true;
		this.AddChild(this.canvas);
		this.canvas.AddChild(this.cajaDeTexto);
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
		//opcionesString = opcionesString.Substring(0, opcionesString.Length - 1);
		this.dialogos.Add(new Dialogo(opcionesString));
		this.index++;
		this.calcularCantidadDeCaracteresDeOpciones();
		this.cargarTexto();
		this.caracteres = this.cajaDeTexto.GetParsedText().Length;
	}

	private void calcularCantidadDeCaracteresDeOpciones(){
		this.caracteresAntesDeLaOpcion = this.cajaDeTexto.Text.Length;
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
			this.checkearCambioDeFondo();
		}
	}

	private void checkearCambioDeFondo(){
		this.dialogos[this.index].iniciarCambioDeFondo(this);
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
		this.checkDialogoOpcional(index);
		this.index++;
		this.dialogos[this.index].executeAction();
		this.cargarTexto();
	}

	private void checkDialogoOpcional(int index){
		List<Dialogo> dialogos = this.dialogos[this.index].cargarDialogoOpcional(this);
		if(dialogos == null) return;
		this.dialogos.InsertRange(index + 1, dialogos);
	}

	public void cargarDialogo(List<Dialogo> dialogos){
		this.dialogos.AddRange(dialogos);
	}

	public void iniciarCambioDeFondo(Texture2D textura){
		this.getSistema().iniciarCambioDeFondo(textura);
	}

	public void cambiarFondoLentamente(){
		this.getSistema().cambiarFondoLentamente();
	}

	public void cambiarFondo(Texture2D textura){
		this.getSistema().cambiarFondo(textura);
	}

	public void cambiarFondo(String ruta){
		this.getSistema().cambiarFondo(ruta);
	}

	public void escalarFondo(Vector2 vector){
		this.getSistema().escalarFondo(vector);
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

	private Vector2 getCamaraGlobalPosition(){
		return this.getSistema().getCameraPosition();
	}

	private void dibujarCajaDeTexto(Node2D sistema){
		this.DrawRect(
				new Rect2(this.getCamaraGlobalPosition().X + POSICION_ORIGEN_RECUADRO_X - 640,
					this.getCamaraGlobalPosition().Y + POSICION_ORIGEN_RECUADRO_Y - 320,
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

	public override void _Draw()
	{
		this.dibujarCajaDeTexto(this);
	}
}

