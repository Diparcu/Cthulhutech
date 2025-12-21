using Godot;
using System;

public class EstadoSistemaTransicionDia : SistemaEstado 
{

    private CanvasLayer canvas; 
    private TransicionDia pantallaNegra; 

    public EstadoSistemaTransicionDia(Sistema sistema, String mensaje1, String mensaje2): base(sistema){
        this.canvas = new CanvasLayer();
        this.canvas.Layer = 1000;
        // Concatenamos mensajes para compatibilidad con nuevo constructor
        this.pantallaNegra = new TransicionDia($"{mensaje1} - {mensaje2}");
        sistema.AddChild(this.canvas);
        this.canvas.AddChild(this.pantallaNegra);
    }

    override public void comportamiento(Sistema sistema, double delta){
        this.pantallaNegra.comportamiento(sistema, delta);
    }

    override public void dibujar(Sistema sistema){
    }

    override public void input(Sistema sistema, InputEvent @event){
    }

}

