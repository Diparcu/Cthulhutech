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

    private PanelContainer menuPrincipalPanel;
    private VBoxContainer menuPrincipalContainer;
    private PanelContainer menuEventosPanel;
    private VBoxContainer menuEventosContainer;
    private Label etiquetaEvento;
    private Sprite2D fondo;
    private PanelContainer tituloPanel;

    public PantallaDeInicio(Font fuente, Sistema sistema)
    {
        this.fuente = fuente;
        this.sistema = sistema;
    }

    public override void _Ready()
    {
        fondo = new Sprite2D();
        this.AddChild(fondo);
        CambiarFondo();

        fondo.ZIndex = -1; // Asegurarse de que el fondo este detras de los menus

        this.crearTitulo();
        this.crearMenuPrincipal();
        this.crearMenuEventos();
        this.crearEtiquetaEvento();

        Button cambiarFondoBtn = new Button();
        cambiarFondoBtn.Text = "Cambiar Fondo";
        cambiarFondoBtn.Position = new Vector2(LONGITUD_PANTALLA - 150, ALTURA_PANTALLA - 50);
        cambiarFondoBtn.Pressed += CambiarFondo;
        this.AddChild(cambiarFondoBtn);
    }

    private void CambiarFondo()
    {
        var backgrounds = new System.Collections.Generic.List<string>();
        var validExtensions = new System.Collections.Generic.List<string> { ".png", ".jpg", ".jpeg" };

        using (var dir = DirAccess.Open("res://Sprites/Inicio"))
        {
            if (dir != null)
            {
                dir.ListDirBegin();
                string fileName = dir.GetNext();
                while (fileName != "")
                {
                    if (!dir.CurrentIsDir())
                    {
                        string extension = System.IO.Path.GetExtension(fileName).ToLower();
                        if (validExtensions.Contains(extension) && !fileName.EndsWith(".import"))
                        {
                            backgrounds.Add(fileName);
                        }
                    }
                    fileName = dir.GetNext();
                }
            }
            else
            {
                GD.PrintErr("No se pudo abrir el directorio 'res://Sprites/Inicio'.");
            }
        }

        if (backgrounds.Count > 0)
        {
            var random = new Random();
            int index = random.Next(backgrounds.Count);
            var texture = (Texture2D)GD.Load("res://Sprites/Inicio/" + backgrounds[index]);
            fondo.Texture = texture;

            float scaleX = LONGITUD_PANTALLA / (float)texture.GetWidth();
            float scaleY = ALTURA_PANTALLA / (float)texture.GetHeight();
            fondo.Scale = new Vector2(scaleX, scaleY);
        }

        fondo.Position = new Vector2(LONGITUD_PANTALLA / 2, ALTURA_PANTALLA / 2);
    }

    private void _on_NuevaPartida_pressed()
    {
        this.sistema.iniciarSeleccionOrigen(this);
    }

    private void _on_Eventos_pressed()
    {
        menuPrincipalPanel.Visible = false;
        menuEventosPanel.Visible = true;
    }

    private void _on_Volver_pressed()
    {
        menuPrincipalPanel.Visible = true;
        menuEventosPanel.Visible = false;
        etiquetaEvento.Visible = false;
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

    private void crearTitulo()
    {
        CenterContainer centerContainer = new CenterContainer();
        centerContainer.SetSize(new Vector2(LONGITUD_PANTALLA, ALTURA_PANTALLA / 2));
        centerContainer.SetPosition(new Vector2(0, ALTURA_PANTALLA / 4 - centerContainer.Size.Y / 2));

        tituloPanel = new PanelContainer();
        var styleBox = new StyleBoxFlat
        {
            BgColor = new Color(0, 0, 0, 0.5f),
            CornerRadiusTopLeft = 10,
            CornerRadiusTopRight = 10,
            CornerRadiusBottomLeft = 10,
            CornerRadiusBottomRight = 10,
            ContentMarginLeft = 10,
            ContentMarginRight = 10,
            ContentMarginTop = 5,
            ContentMarginBottom = 5
        };
        tituloPanel.AddThemeStyleboxOverride("panel", styleBox);

        Label tituloLabel = new Label
        {
            Text = "Arcologia BioBio\n(Nombre final pendiente)",
            HorizontalAlignment = HorizontalAlignment.Center
        };
        tituloLabel.AddThemeFontSizeOverride("font_size", FONT_SIZE);

        tituloPanel.AddChild(tituloLabel);
        centerContainer.AddChild(tituloPanel);
        this.AddChild(centerContainer);
    }

    private void crearMenuPrincipal()
    {
        menuPrincipalPanel = new PanelContainer();
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Color(0, 0, 0, 0.5f);
        styleBox.CornerRadiusTopLeft = 10;
        styleBox.CornerRadiusTopRight = 10;
        styleBox.CornerRadiusBottomLeft = 10;
        styleBox.CornerRadiusBottomRight = 10;
        menuPrincipalPanel.AddThemeStyleboxOverride("panel", styleBox);

        menuPrincipalPanel.Position = new Vector2(LONGITUD_PANTALLA / 5 * 2, ALTURA_PANTALLA / 2);

        menuPrincipalContainer = new VBoxContainer();
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

        menuPrincipalPanel.AddChild(menuPrincipalContainer);
        this.AddChild(menuPrincipalPanel);
    }

    private void crearMenuEventos()
    {
        menuEventosPanel = new PanelContainer();
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Color(0, 0, 0, 0.5f);
        styleBox.CornerRadiusTopLeft = 10;
        styleBox.CornerRadiusTopRight = 10;
        styleBox.CornerRadiusBottomLeft = 10;
        styleBox.CornerRadiusBottomRight = 10;
        menuEventosPanel.AddThemeStyleboxOverride("panel", styleBox);

        menuEventosPanel.Position = new Vector2(LONGITUD_PANTALLA / 5 * 2, ALTURA_PANTALLA / 2);
        menuEventosPanel.Visible = false;

        menuEventosContainer = new VBoxContainer();
        menuEventosContainer.AddThemeConstantOverride("separation", -2);

        Button eventoMegan = this.crearBoton("Evento Disco (Megan)");
        eventoMegan.Pressed += this.iniciarJuegoMegan;

        Button eventoKC = this.crearBoton("Evento Armas (KC)");
        eventoKC.Pressed += this.iniciarJuegoKC;

        Button eventoIsla = this.crearBoton("Evento Isla");
        eventoIsla.Pressed += () => this.sistema.iniciarJuegoIsla(this);

        Button eventoNormal = this.crearBoton("Evento Normal");
        eventoNormal.Pressed += () => this.sistema.iniciarJuegoNormal(this);

        Button eventoPudiente = this.crearBoton("Evento Pudiente");
        eventoPudiente.Pressed += () => this.sistema.iniciarJuegoPudiente(this);

        Button volver = this.crearBoton("Volver");
        volver.Pressed += this._on_Volver_pressed;

        menuEventosContainer.AddChild(eventoMegan);
        menuEventosContainer.AddChild(eventoKC);
        menuEventosContainer.AddChild(eventoIsla);
        menuEventosContainer.AddChild(eventoNormal);
        menuEventosContainer.AddChild(eventoPudiente);
        menuEventosContainer.AddChild(volver);

        menuEventosPanel.AddChild(menuEventosContainer);
        this.AddChild(menuEventosPanel);
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
}
