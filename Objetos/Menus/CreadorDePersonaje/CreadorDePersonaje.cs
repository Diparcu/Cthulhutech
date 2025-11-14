using Godot;
using System.Reflection;
using System;
using System.Collections.Generic;

public partial class CreadorDePersonaje : Node2D
{

	private Personaje jugador = new Personaje();
	private Sistema sistema;
	private Dia diaInicial;
	private List<OpcionCreadorDePersonaje> estadisticas = new List<OpcionCreadorDePersonaje>(); 
	private List<OpcionCreadorDePersonaje> habilidades = new List<OpcionCreadorDePersonaje>(); 
	private List<OpcionCreadorDePersonajePerk> perks = new List<OpcionCreadorDePersonajePerk>(); 
	private Font fuente;

	public const int POSICION_INICIO_VERTICAL = 32;
	public int total = 30;
	public int totalHabiliad = 20;

	private int origenSeleccionado = -1; // 0: Isleño, 1: Blanco, 2: Bajomundo
    private int arquetipoSeleccionado = -1; // 0: Intelectual, 1: Emocional, 2: Bruto, 3: Manual


	private Button botonDeContinuar = new Button();
    private Button botonDeVolver = new Button();
    private VBoxContainer vboxOrigen;
    private VBoxContainer vboxArquetipo;

    private Button botonIsleño;
    private Button botonBlanco;
    private Button botonBajomundo;

    private Button botonIntelectual;
    private Button botonEmocional;
    private Button botonBruto;
    private Button botonManual;

    private Panel topBar;
    private Button statsButton;
    private bool statsVisible = false;
    private Label warningLabel;

	private partial class OpcionCreadorDePersonajePerk: Node2D
	{
		public const int SEPARACION_VERTICAL = 256;

		private Button boton = new Button();
		private Font fuente;
		private bool seleccionada = false;

		private int index;
		private string nombre;
		private CreadorDePersonaje creador;
		private List<OpcionCreadorDePersonaje> opcionesPadre = new List<OpcionCreadorDePersonaje>();

		public void anadirPadre(OpcionCreadorDePersonaje padre){
			this.opcionesPadre.Add(padre);
			this.Position += new Vector2(SEPARACION_VERTICAL* 1.2f, 0);
		}

		public void seleccionar(){
			if(this.seleccionada){
				this.creador.totalHabiliad += 2;
				this.seleccionada = !this.seleccionada;
			}else if(this.creador.totalHabiliad >= 2){
				this.creador.totalHabiliad -= 2;
				this.seleccionada = !this.seleccionada;
			}
			this.QueueRedraw();
		}

		public OpcionCreadorDePersonajePerk(string variable,
				Font fuente,
				int index,
				CreadorDePersonaje creador){

			this.Position += new Vector2(0, 48);
			 
			this.index = index;
			this.creador = creador;
			this.nombre = variable;

			this.boton.Visible = true;
			this.boton.Text = "   ";
			this.boton.Flat = true;
			this.boton.AddThemeFontSizeOverride("font_size", 10);
			this.boton.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
			this.boton.Pressed += this.seleccionar;
			this.boton.Position = new Vector2(SEPARACION_VERTICAL*4.3f,
					Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE*(this.index)+9);

			this.AddChild(this.boton);
		}

		public override void _Draw()
		{

			this.DrawString(Sistema.fuente,
					new Vector2(SEPARACION_VERTICAL*3.02f, Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE*(this.index + 1)),
					this.nombre,
					Sistema.FUENTE_ORIENTACION,
					Sistema.FUENTE_ANCHURA,
					Sistema.TAMANO_FUENTE);
			
			if(this.seleccionada){
				this.DrawString(Sistema.fuente,
						new Vector2(SEPARACION_VERTICAL*4.3f, Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE*(this.index + 1)),
						"▣",
						Sistema.FUENTE_ORIENTACION,
						Sistema.FUENTE_ANCHURA,
						Sistema.TAMANO_FUENTE);
			}else{
				this.DrawString(Sistema.fuente,
						new Vector2(SEPARACION_VERTICAL*4.3f, Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE*(this.index + 1)),
						"□",
						Sistema.FUENTE_ORIENTACION,
						Sistema.FUENTE_ANCHURA,
						Sistema.TAMANO_FUENTE);
			}

		}

	}

	private partial class OpcionCreadorDePersonaje: Node2D
	{
		private Personaje jugador;

		public const int SEPARACION_VERTICAL = 256;

		private Button botonAumentar = new Button();
		private Button botonDiminuir = new Button();
		private Label etiqueta = new Label();
		private Font fuente;
		private bool separacionExtra = false;

		private string value;
		private int index;
		private CreadorDePersonaje creador;
		private List<OpcionCreadorDePersonaje> opcionesPadre = new List<OpcionCreadorDePersonaje>();
		private List<OpcionCreadorDePersonaje> opcionesHijo = new List<OpcionCreadorDePersonaje>();

		public OpcionCreadorDePersonaje setearBotonSubida(System.Action accion){
			this.botonAumentar.Pressed += accion;
			return this;
		}

		public OpcionCreadorDePersonaje setearBotonBajada(System.Action accion){
			this.botonDiminuir.Pressed += accion;
			return this;
		}

		public OpcionCreadorDePersonaje anadirJugador(Personaje jugador){
			this.jugador = jugador;
			return this;
		}

		private object getValue(){
			Type tipo = typeof(Personaje);
			PropertyInfo propiedad = tipo.GetProperty(this.value);
			return propiedad.GetValue(this.jugador);
		}

		public void anadirPadre(OpcionCreadorDePersonaje padre){
			this.opcionesPadre.Add(padre);
			this.separacionExtra = true;
			this.botonDiminuir.Position += new Vector2(SEPARACION_VERTICAL/2, 0);
			this.botonAumentar.Position += new Vector2(SEPARACION_VERTICAL/2, 0);
			padre.anadirHijo(this);
			this.Position += new Vector2(SEPARACION_VERTICAL* 1.2f, 0);
		}

		private void anadirHijo(OpcionCreadorDePersonaje hijo){
			this.opcionesHijo.Add(hijo);
		}

		public OpcionCreadorDePersonaje(string variable,
				int index){

			this.value = variable;
			this.Position += new Vector2(0, 48);
			 
			this.index = index;

			this.botonAumentar.Visible = true;
			this.botonAumentar.Text = "   ";
			this.botonAumentar.Flat = true;
			this.botonAumentar.Pressed += this.QueueRedraw;
			this.botonAumentar.AddThemeFontSizeOverride("font_size", 10);
			this.botonAumentar.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
			this.botonAumentar.Position = new Vector2(
					Sistema.TAMANO_FUENTE*1.5f + SEPARACION_VERTICAL - 2,
					Sistema.TAMANO_FUENTE * (index + 1) + 9);

			this.botonDiminuir.Visible = true;
			this.botonDiminuir.Text = "  ";
			this.botonDiminuir.Flat = true;
			this.botonDiminuir.Pressed += this.QueueRedraw;
			this.botonDiminuir.AddThemeFontSizeOverride("font_size", 10);
			this.botonDiminuir.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
			this.botonDiminuir.Position = new Vector2(
					SEPARACION_VERTICAL- 2,
					Sistema.TAMANO_FUENTE*(index + 1) + 9);

			this.etiqueta.Visible = true;
			this.etiqueta.Text = variable;
			this.etiqueta.Position = new Vector2(Sistema.TAMANO_FUENTE, Sistema.TAMANO_FUENTE * (index + 1));
			this.etiqueta.AddThemeFontOverride("font", Sistema.fuente);
			this.etiqueta.AddThemeFontSizeOverride("font_size", Sistema.TAMANO_FUENTE);

			this.AddChild(this.etiqueta);
			this.AddChild(this.botonAumentar);
			this.AddChild(this.botonDiminuir);
		}

		public override void _Draw()
		{

			if(this.getValue().ToString().ToInt() < 10){
				this.DrawString(Sistema.fuente,
						new Vector2(SEPARACION_VERTICAL * (this.separacionExtra ? 1.5f : 1), Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE*(this.index + 1)),
						"-0" + (this.getValue().ToString()) + "+",
						Sistema.FUENTE_ORIENTACION,
						Sistema.FUENTE_ANCHURA,
						Sistema.TAMANO_FUENTE);
			}else{
				this.DrawString(Sistema.fuente,
						new Vector2(SEPARACION_VERTICAL * (this.separacionExtra ? 1.5f : 1), Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE*(this.index + 1)),
						"-"+(this.getValue().ToString()) + "+",
						Sistema.FUENTE_ORIENTACION,
						Sistema.FUENTE_ANCHURA,
						Sistema.TAMANO_FUENTE);
			}

		}

	}

    public CreadorDePersonaje(Sistema sistema, Dia diaInicial = null)
    {
        this.sistema = sistema;
        this.diaInicial = diaInicial;
        this.fuente = Sistema.fuente;

        // Botón Volver
        botonDeVolver = new Button { Text = "Volver" };
        botonDeVolver.Position = new Vector2(50, 580);
        botonDeVolver.Pressed += _on_Volver_pressed;
        AddChild(botonDeVolver);

        // Etiqueta de advertencia
        warningLabel = new Label();
        warningLabel.Position = new Vector2(400, 550);
        warningLabel.Visible = false;
        warningLabel.AddThemeColorOverride("font_color", Colors.Red);
        warningLabel.AddThemeFontSizeOverride("font_size", 24);
        AddChild(warningLabel);

        // Origen
        vboxOrigen = new VBoxContainer();
        vboxOrigen.Position = new Vector2(50, 100);
        AddChild(vboxOrigen);

        botonIsleño = new Button { Text = "Niño isleño" };
        botonBlanco = new Button { Text = "Niño blanco" };
        botonBajomundo = new Button { Text = "Niño bajomundo" };

        vboxOrigen.AddChild(new Label { Text = "Origen" });
        vboxOrigen.AddChild(botonIsleño);
        vboxOrigen.AddChild(botonBlanco);
        vboxOrigen.AddChild(botonBajomundo);

        botonIsleño.Pressed += () => SeleccionarOrigen(0);
        botonBlanco.Pressed += () => SeleccionarOrigen(1);
        botonBajomundo.Pressed += () => SeleccionarOrigen(2);

        // Arquetipo
        vboxArquetipo = new VBoxContainer();
        vboxArquetipo.Position = new Vector2(250, 100);
        AddChild(vboxArquetipo);

        botonIntelectual = new Button { Text = "Intelectual" };
        botonEmocional = new Button { Text = "Emocional" };
        botonBruto = new Button { Text = "Bruto" };
        botonManual = new Button { Text = "Manual" };

        vboxArquetipo.AddChild(new Label { Text = "Arquetipo" });
        vboxArquetipo.AddChild(botonIntelectual);
        vboxArquetipo.AddChild(botonEmocional);
        vboxArquetipo.AddChild(botonBruto);
        vboxArquetipo.AddChild(botonManual);

        botonIntelectual.Pressed += () => SeleccionarArquetipo(0);
        botonEmocional.Pressed += () => SeleccionarArquetipo(1);
        botonBruto.Pressed += () => SeleccionarArquetipo(2);
        botonManual.Pressed += () => SeleccionarArquetipo(3);

		OpcionCreadorDePersonaje agilidad = new OpcionCreadorDePersonaje("Agilidad", 0);
		OpcionCreadorDePersonaje fuerza = new OpcionCreadorDePersonaje("Fuerza", 1);
		OpcionCreadorDePersonaje inteligencia = new OpcionCreadorDePersonaje("Inteligencia", 2);
		OpcionCreadorDePersonaje percepcion = new OpcionCreadorDePersonaje("Percepcion", 3);
		OpcionCreadorDePersonaje presencia = new OpcionCreadorDePersonaje("Presencia", 4);
		OpcionCreadorDePersonaje tenacidad = new OpcionCreadorDePersonaje("Tenacidad", 5);

		estadisticas.Add(agilidad);
		estadisticas.Add(fuerza);
		estadisticas.Add(inteligencia);
		estadisticas.Add(percepcion);
		estadisticas.Add(presencia);
		estadisticas.Add(tenacidad);

		this.AddChild(agilidad);
		this.AddChild(fuerza);
		this.AddChild(inteligencia);
		this.AddChild(percepcion);
		this.AddChild(presencia);
		this.AddChild(tenacidad);

		OpcionCreadorDePersonaje atletismo = new OpcionCreadorDePersonaje("Atletismo", 0);
		OpcionCreadorDePersonaje cienciasOcultas = new OpcionCreadorDePersonaje("CienciasOcultas", 1);
		OpcionCreadorDePersonaje conocimientoRegional = new OpcionCreadorDePersonaje("ConocimientoRegional", 2);
		OpcionCreadorDePersonaje educacion = new OpcionCreadorDePersonaje("Educacion", 3);
		OpcionCreadorDePersonaje investigacion = new OpcionCreadorDePersonaje("Investigacion", 4);
		OpcionCreadorDePersonaje labia = new OpcionCreadorDePersonaje("Labia", 5);
		OpcionCreadorDePersonaje leptoescritura = new OpcionCreadorDePersonaje("Leptoescritura", 6);
		OpcionCreadorDePersonaje medicina = new OpcionCreadorDePersonaje("Medicina", 7);
		OpcionCreadorDePersonaje saivorFeire = new OpcionCreadorDePersonaje("SaivorFeire", 8);
		OpcionCreadorDePersonaje sigilo = new OpcionCreadorDePersonaje("Sigilo", 9);
		OpcionCreadorDePersonaje supervivencia = new OpcionCreadorDePersonaje("Supervivencia", 10);
		OpcionCreadorDePersonaje tasacion = new OpcionCreadorDePersonaje("Tasacion", 11);


		agilidad.anadirJugador(this.jugador).
			setearBotonSubida(sigilo.QueueRedraw).
			setearBotonBajada(sigilo.QueueRedraw).
			setearBotonSubida(this.jugador.subirAgilidad).
			setearBotonBajada(this.jugador.bajarAgilidad);

		fuerza.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirFuerza).
			setearBotonSubida(atletismo.QueueRedraw).
			setearBotonBajada(this.jugador.bajarFuerza).
			setearBotonBajada(atletismo.QueueRedraw);

		inteligencia.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirInteligencia).
			setearBotonBajada(this.jugador.bajarInteligencia).
			setearBotonSubida(medicina.QueueRedraw).
			setearBotonBajada(medicina.QueueRedraw).
			setearBotonSubida(tasacion.QueueRedraw).
			setearBotonBajada(tasacion.QueueRedraw).
			setearBotonSubida(leptoescritura.QueueRedraw).
			setearBotonBajada(leptoescritura.QueueRedraw).
			setearBotonSubida(conocimientoRegional.QueueRedraw).
			setearBotonBajada(conocimientoRegional.QueueRedraw).
			setearBotonSubida(cienciasOcultas.QueueRedraw).
			setearBotonBajada(cienciasOcultas.QueueRedraw).
			setearBotonSubida(educacion.QueueRedraw).
			setearBotonBajada(educacion.QueueRedraw).
			setearBotonSubida(investigacion.QueueRedraw).
			setearBotonBajada(investigacion.QueueRedraw);

		percepcion.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirPercepcion).
			setearBotonBajada(this.jugador.bajarPercepcion);

		presencia.anadirJugador(this.jugador).
			setearBotonSubida(saivorFeire.QueueRedraw).
			setearBotonBajada(saivorFeire.QueueRedraw).
			setearBotonSubida(labia.QueueRedraw).
			setearBotonBajada(labia.QueueRedraw).
			setearBotonSubida(this.jugador.subirPresencia).
			setearBotonBajada(this.jugador.bajarPresencia);

		tenacidad.anadirJugador(this.jugador).
			setearBotonSubida(supervivencia.QueueRedraw).
			setearBotonBajada(supervivencia.QueueRedraw).
			setearBotonSubida(this.jugador.subirTenacidad).
			setearBotonBajada(this.jugador.bajarTenacidad);


		atletismo.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirAtletismo).
			setearBotonBajada(this.jugador.bajarAtletismo);

		cienciasOcultas.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirCienciasOcultas).
			setearBotonBajada(this.jugador.bajarCienciasOcultas);

		conocimientoRegional.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirConocimientoRegional).
			setearBotonBajada(this.jugador.bajarConocimientoRegional);

		educacion.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirEducacion).
			setearBotonBajada(this.jugador.bajarEducacion);

		investigacion.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirInvestigacion).
			setearBotonBajada(this.jugador.bajarInvestigacion);

		labia.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirLabia).
			setearBotonBajada(this.jugador.bajarLabia);

		leptoescritura.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirLeptoescritura).
			setearBotonBajada(this.jugador.bajarLeptoescritura);

		medicina.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirMedicina).
			setearBotonBajada(this.jugador.bajarMedicina);

		saivorFeire.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirSaivorFeire).
			setearBotonBajada(this.jugador.bajarSaivorFeire);

		sigilo.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirSigilo).
			setearBotonBajada(this.jugador.bajarSigilo);

		supervivencia.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirSupervivencia).
			setearBotonBajada(this.jugador.bajarSupervivencia);

		tasacion.anadirJugador(this.jugador).
			setearBotonSubida(this.jugador.subirSupervivencia).
			setearBotonBajada(this.jugador.bajarSupervivencia);


		atletismo.anadirPadre(fuerza);
		cienciasOcultas.anadirPadre(inteligencia);
		conocimientoRegional.anadirPadre(inteligencia);
		educacion.anadirPadre(inteligencia);
		investigacion.anadirPadre(inteligencia);
		labia.anadirPadre(presencia);
		leptoescritura.anadirPadre(inteligencia);
		medicina.anadirPadre(inteligencia);
		saivorFeire.anadirPadre(presencia);
		sigilo.anadirPadre(agilidad);
		supervivencia.anadirPadre(tenacidad);
		tasacion.anadirPadre(inteligencia);

		habilidades.Add(atletismo);
		habilidades.Add(cienciasOcultas);
		habilidades.Add(conocimientoRegional);
		habilidades.Add(educacion);
		habilidades.Add(investigacion);
		habilidades.Add(labia);
		habilidades.Add(leptoescritura);
		habilidades.Add(medicina);
		habilidades.Add(saivorFeire);
		habilidades.Add(sigilo);
		habilidades.Add(supervivencia);
		habilidades.Add(tasacion);

		this.AddChild(atletismo);
		this.AddChild(cienciasOcultas);
		this.AddChild(conocimientoRegional);
		this.AddChild(educacion);
		this.AddChild(investigacion);
		this.AddChild(labia);
		this.AddChild(leptoescritura);
		this.AddChild(medicina);
		this.AddChild(saivorFeire);
		this.AddChild(sigilo);
		this.AddChild(supervivencia);
		this.AddChild(tasacion);

		OpcionCreadorDePersonajePerk perkDeTesteo = new OpcionCreadorDePersonajePerk("Perk De Testeo",
				fuente,
				0,
				this);

		this.perks.Add(perkDeTesteo);
		this.AddChild(perkDeTesteo);

		this.botonDeContinuar.Visible = true;
		this.botonDeContinuar.Text = "Terminar";
		this.botonDeContinuar.Flat = true;
		this.botonDeContinuar.AddThemeFontSizeOverride("font_size", 28);
		this.botonDeContinuar.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
		this.botonDeContinuar.Pressed += this.terminarPersonaje;
		this.botonDeContinuar.Position = new Vector2(
				1000,
				580);

		this.AddChild(this.botonDeContinuar);
		
	}

    private void _on_Volver_pressed()
    {
        this.sistema.volverAlMenuPrincipalDesdeMenu(this);
    }

	private void terminarPersonaje(){
		if (origenSeleccionado != -1 && arquetipoSeleccionado != -1)
        {
            this.sistema.inicializarJugador(this.jugador);
            switch (origenSeleccionado)
            {
                case 0:
                    this.sistema.cargarEscena(new Dia0Isla(this.sistema));
                    break;
                case 1:
                    this.sistema.cargarEscena(new Dia0NinoBlanco(this.sistema));
                    break;
                case 2:
                    this.sistema.cargarEscena(new Dia0NinoBajomundo(this.sistema));
                    break;
            }
        }
        else
        {
            warningLabel.Text = "Debes seleccionar un origen y un arquetipo.";
            warningLabel.Visible = true;
        }
	}

	private void SeleccionarOrigen(int origen)
    {
        origenSeleccionado = origen;
        switch (origen)
        {
            case 0: // Isleño
                jugador.Dinero = 10;
                botonIsleño.Disabled = true;
                botonBlanco.Disabled = false;
                botonBajomundo.Disabled = false;
                break;
            case 1: // Blanco
                jugador.Dinero = 50;
                botonIsleño.Disabled = false;
                botonBlanco.Disabled = true;
                botonBajomundo.Disabled = false;
                break;
            case 2: // Bajomundo
                jugador.Dinero = 100;
                botonIsleño.Disabled = false;
                botonBlanco.Disabled = false;
                botonBajomundo.Disabled = true;
                break;
        }
    }

    private void SeleccionarArquetipo(int arquetipo)
    {
        arquetipoSeleccionado = arquetipo;
        jugador.resetearStats();

        switch (arquetipo)
        {
            case 0: // Intelectual
                jugador.Inteligencia = 15;
                jugador.Fuerza = 5;
                jugador.Presencia = 5;
                botonIntelectual.Disabled = true;
                botonEmocional.Disabled = false;
                botonBruto.Disabled = false;
                break;
            case 1: // Emocional
                jugador.Presencia = 15;
                jugador.Inteligencia = 5;
                jugador.Fuerza = 5;
                botonIntelectual.Disabled = false;
                botonEmocional.Disabled = true;
                botonBruto.Disabled = false;
                break;
            case 2: // Bruto
                jugador.Fuerza = 15;
                jugador.Inteligencia = 5;
                jugador.Presencia = 5;
                botonIntelectual.Disabled = false;
                botonEmocional.Disabled = false;
                botonBruto.Disabled = true;
                botonManual.Disabled = false;
                break;
            case 3: // Manual
                botonIntelectual.Disabled = false;
                botonEmocional.Disabled = false;
                botonBruto.Disabled = false;
                botonManual.Disabled = true;
                break;
        }
        QueueRedraw();
    }


	public override void _Ready()
	{
	}

	public override void _Input(InputEvent @event)
	{
		this.QueueRedraw();
	}

	public override void _Draw()
	{
		this.DrawString(Sistema.fuente,
				new Vector2(Sistema.TAMANO_FUENTE, Sistema.TAMANO_FUENTE),
				"Estadisticas:",
				Sistema.FUENTE_ORIENTACION,
				Sistema.FUENTE_ANCHURA,
				Sistema.TAMANO_FUENTE);

		this.DrawString(Sistema.fuente,
				new Vector2(Sistema.TAMANO_FUENTE, Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE),
				"Puntos:               " + this.jugador.PuntosDeEstadistica,
				Sistema.FUENTE_ORIENTACION,
				Sistema.FUENTE_ANCHURA,
				Sistema.TAMANO_FUENTE);

        this.DrawString(Sistema.fuente,
				new Vector2(Sistema.TAMANO_FUENTE, Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE * 2),
				"XP: " + this.jugador.XP,
				Sistema.FUENTE_ORIENTACION,
				Sistema.FUENTE_ANCHURA,
				Sistema.TAMANO_FUENTE);

		this.DrawString(Sistema.fuente,
				new Vector2(Sistema.TAMANO_FUENTE + OpcionCreadorDePersonaje.SEPARACION_VERTICAL*1.2f,  Sistema.TAMANO_FUENTE),
				"Habilidades y perks: ",
				Sistema.FUENTE_ORIENTACION,
				Sistema.FUENTE_ANCHURA,
				Sistema.TAMANO_FUENTE);

		this.DrawString(Sistema.fuente,
				new Vector2(Sistema.TAMANO_FUENTE + OpcionCreadorDePersonaje.SEPARACION_VERTICAL*1.2f, Sistema.TAMANO_FUENTE + Sistema.TAMANO_FUENTE),
				"Puntos:                               " + this.jugador.PuntosDeHabilidad,
				Sistema.FUENTE_ORIENTACION,
				Sistema.FUENTE_ANCHURA,
				Sistema.TAMANO_FUENTE);

		this.DrawString(Sistema.fuente,
				new Vector2(Sistema.TAMANO_FUENTE + OpcionCreadorDePersonaje.SEPARACION_VERTICAL*2.9f,  Sistema.TAMANO_FUENTE*2),
				"Perks:",
				Sistema.FUENTE_ORIENTACION,
				Sistema.FUENTE_ANCHURA,
				Sistema.TAMANO_FUENTE);

	}

	public override void _Process(double delta)
	{
	}
}
