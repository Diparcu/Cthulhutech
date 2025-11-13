using Godot;
using System;

public partial class EventoCasaJugadorIsla : Node2D
{
    private Sistema sistema;

    public EventoCasaJugadorIsla(Sistema sistema)
    {
        this.sistema = sistema;
    }

    public override void _Ready()
    {
        // Background
        Sprite2D fondo = new Sprite2D();
        try {
            fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_CASA_ISLA.jpg");
        } catch (Exception) {
            GD.Print("No se pudo cargar el fondo 'FONDO_CASA_ISLA.jpg'. Usando placeholder.");
            fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_ISLA.jpg");
        }
        fondo.Position = new Vector2(1280 / 2, 640 / 2);
        this.AddChild(fondo);

        // Narration
        RichTextLabel narracion = new RichTextLabel();
        narracion.Text = "Llegaste a casa, el silencio se apodera del lugar, ni perros ladran, ni pájaros vuelan cerca, es definitivamente un lugar tranquilo";
        narracion.SetSize(new Vector2(800, 100));
        narracion.Position = new Vector2(240, 450);
        narracion.AddThemeFontSizeOverride("normal_font_size", 24);
        this.AddChild(narracion);

        // Buttons
        VBoxContainer buttonContainer = new VBoxContainer();
        buttonContainer.Position = new Vector2(540, 200);
        buttonContainer.AddThemeConstantOverride("separation", 10);

        Button dormir = new Button { Text = "Ir a dormir" };
        dormir.Pressed += onDormirPressed;
        buttonContainer.AddChild(dormir);

        Button desvelarse = new Button { Text = "Desvelarse" };
        desvelarse.Pressed += onDesvelarsePressed;
        buttonContainer.AddChild(desvelarse);

        Button inventario = new Button { Text = "Ordenar inventario" };
        inventario.Pressed += onInventarioPressed;
        buttonContainer.AddChild(inventario);

        this.AddChild(buttonContainer);
    }

    private void onDormirPressed()
    {
        // Reset modifier if player sleeps well
        this.sistema.getJugador().ModificadorTiradasDiarias = 0;
        this.sistema.avanzarAlSiguienteDia(this);
    }

    private void onDesvelarsePressed()
    {
        this.sistema.getJugador().ModificadorTiradasDiarias = -2;
        // Here you could start another event for the 'desvelarse' action
        // For now, just advance the day with the debuff
        this.sistema.avanzarAlSiguienteDia(this);
    }

    private void onInventarioPressed()
    {
        RichTextLabel inventarioLabel = new RichTextLabel();
        inventarioLabel.Text = "Inventario no implementado.";
        inventarioLabel.SetSize(new Vector2(400, 50));
        inventarioLabel.Position = new Vector2(440, 150);
        this.AddChild(inventarioLabel);
        
        Timer timer = new Timer();
        timer.WaitTime = 2;
        timer.OneShot = true;
        timer.Timeout += () => inventarioLabel.QueueFree();
        this.AddChild(timer);
        timer.Start();
    }
}
