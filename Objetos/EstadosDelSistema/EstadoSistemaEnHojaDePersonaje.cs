using Godot;
using System;

public class SistemaEstadoEnHojaDePersonaje : SistemaEstado 
{
    public SistemaEstadoEnHojaDePersonaje(Sistema sistema): base(sistema){
    }

    override public void comportamiento(Sistema sistema, double delta){}
    override public void dibujar(Sistema sistema){}
    override public void input(Sistema sistema, InputEvent @event){}
}



