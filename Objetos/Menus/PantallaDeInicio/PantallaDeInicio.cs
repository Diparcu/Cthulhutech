using Godot;
using System;


[Tool]
public partial class PantallaDeInicio : Node2D
{
    private const int LONGITUD_PANTALLA = 1280;
    private const int ALTURA_PANTALLA = 640;
    private const int FONT_SIZE = 32;

    private Sistema sistema;
    private Font fuente;

    private VBoxContainer menuPrincipalContainer;
    private VBoxContainer menuNuevaPartidaContainer;
    private VBoxContainer menuEventosContainer;
    private Label etiquetaEvento;

    public PantallaDeInicio(Font fuente, Sistema sistema)
    {
        this.fuente = fuente;
        this.sistema = sistema;
    }

    public override void _Ready()
    {
        this.crearMenuPrincipal();
        this.crearMenuNuevaPartida();
        this.crearMenuEventos();
        this.crearEtiquetaEvento();
    }

    private void _on_NuevaPartida_pressed()
    {
        menuPrincipalContainer.Visible = false;
        menuNuevaPartidaContainer.Visible = true;
    }

    private void _on_Eventos_pressed()
    {
        menuPrincipalContainer.Visible = false;
        menuEventosContainer.Visible = true;
    }

    private void _on_Volver_pressed()
    {
        menuPrincipalContainer.Visible = true;
        menuNuevaPartidaContainer.Visible = false;
        menuEventosContainer.Visible = false;
        etiquetaEvento.Visible = false;
    }

    private void iniciarJuegoPersonalizado()
    {
        this.sistema.iniciarCreadorDePersonaje(this);
    }

    private void iniciarJuegoMegan()
    {
        this.sistema.iniciarJuegoMegan(this);
    }

    private void iniciarJuegoKC()
    {
        this.sistema.iniciarJuegoKC(this);
    }

    private void cerrarLaWea()
    {
        this.GetTree().Quit();
    }

    private void crearMenuPrincipal()
    {
        menuPrincipalContainer = new VBoxContainer();
        menuPrincipalContainer.Position = new Vector2(LONGITUD_PANTALLA / 5 * 2, ALTURA_PANTALLA / 2);
        menuPrincipalContainer.AddThemeConstantOverride("separation", -2);

        Button nuevaPartida = this.crearBoton("Nueva partida");
        nuevaPartida.Pressed += this._on_NuevaPartida_pressed;

        Button eventos = this.crearBoton("Eventos");
        eventos.Pressed += this._on_Eventos_pressed;

        Button cargar = this.crearBoton("Cargar partida");
        Button opciones = this.crearBoton("Opciones");
        opciones.Pressed += () => this.sistema.iniciarOpciones(this);

        Button creditos = this.crearBoton("Creditos");
        creditos.Pressed += () => this.sistema.iniciarCreditos(this);

        Button salir = this.crearBoton("Salir del juego");
        salir.Pressed += this.cerrarLaWea;

        menuPrincipalContainer.AddChild(nuevaPartida);
        menuPrincipalContainer.AddChild(eventos);
        menuPrincipalContainer.AddChild(cargar);
        menuPrincipalContainer.AddChild(opciones);
        menuPrincipalContainer.AddChild(creditos);
        menuPrincipalContainer.AddChild(salir);

        this.AddChild(menuPrincipalContainer);
    }

    private void crearMenuNuevaPartida()
    {
        menuNuevaPartidaContainer = new VBoxContainer();
        menuNuevaPartidaContainer.Position = new Vector2(LONGITUD_PANTALLA / 5 * 2, ALTURA_PANTALLA / 2);
        menuNuevaPartidaContainer.AddThemeConstantOverride("separation", -2);
        menuNuevaPartidaContainer.Visible = false;

        Button isla = this.crearBoton("Isla");
        isla.Pressed += () => this.sistema.iniciarJuegoIsla(this);

        Button normal = this.crearBoton("Normal");
        normal.Pressed += () => this.sistema.iniciarJuegoNormal(this);

        Button pudiente = this.crearBoton("Pudiente");
        pudiente.Pressed += () => this.sistema.iniciarJuegoPudiente(this);

        Button personalizado = this.crearBoton("Personalizado");
        personalizado.Pressed += this.iniciarJuegoPersonalizado;

        Button volver = this.crearBoton("Volver");
        volver.Pressed += this._on_Volver_pressed;

        menuNuevaPartidaContainer.AddChild(isla);
        menuNuevaPartidaContainer.AddChild(normal);
        menuNuevaPartidaContainer.AddChild(pudiente);
        menuNuevaPartidaContainer.AddChild(personalizado);
        menuNuevaPartidaContainer.AddChild(volver);

        this.AddChild(menuNuevaPartidaContainer);
    }

    private void crearMenuEventos()
    {
        menuEventosContainer = new VBoxContainer();
        menuEventosContainer.Position = new Vector2(LONGITUD_PANTALLA / 5 * 2, ALTURA_PANTALLA / 2);
        menuEventosContainer.AddThemeConstantOverride("separation", -2);
        menuEventosContainer.Visible = false;

        Button eventoInicial = this.crearBoton("Evento Inicial (Shinji)");
        eventoInicial.Pressed += this.iniciarJuegoPersonalizado;

        Button eventoMegan = this.crearBoton("Evento Disco (Megan)");
        eventoMegan.Pressed += this.iniciarJuegoMegan;

        Button eventoKC = this.crearBoton("Evento Armas (KC)");
        eventoKC.Pressed += this.iniciarJuegoKC;

        Button volver = this.crearBoton("Volver");
        volver.Pressed += this._on_Volver_pressed;

        menuEventosContainer.AddChild(eventoInicial);
        menuEventosContainer.AddChild(eventoMegan);
        menuEventosContainer.AddChild(eventoKC);
        menuEventosContainer.AddChild(volver);

        this.AddChild(menuEventosContainer);
    }

    private void crearEtiquetaEvento()
    {
        etiquetaEvento = new Label();
        etiquetaEvento.Position = new Vector2(LONGITUD_PANTALLA / 2, ALTURA_PANTALLA - 100);
        etiquetaEvento.Text = "";
        etiquetaEvento.Visible = false;
        etiquetaEvento.AddThemeFontSizeOverride("font_size", 48);
        this.AddChild(etiquetaEvento);
    }

    private Button crearBoton(string nombreBoton)
    {
        Button boton = new Button();
        boton.Flat = true;
        boton.Text = nombreBoton;
        boton.AddThemeFontSizeOverride("font_size", 32);
        boton.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
        return boton;
    }

    private void dibujarTitulo()
    {
        DrawMultilineString(fuente,
                new Vector2(LONGITUD_PANTALLA / 4,
                        ALTURA_PANTALLA / 4),
                "       Arcologia BioBio \n(Nombre final pendiente)",
                HorizontalAlignment.Center,
                -1,
                FONT_SIZE);
    }

    public override void _Draw()
    {
        this.dibujarTitulo();
    }
}