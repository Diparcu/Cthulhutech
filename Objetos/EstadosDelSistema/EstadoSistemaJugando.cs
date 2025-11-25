using Godot;
using System;

public class SistemaEstadoJugando : SistemaEstado 
{

    public SistemaEstadoJugando(Sistema sistema): base(sistema){
    }

    override public void comportamiento(Sistema sistema, double delta){
        this.sistema.comportamientoDiaCargado(delta);
    }

    override public void dibujar(Sistema sistema){
        this.sistema.dibujarDiaCargado();
    }

    override public void input(Sistema sistema, InputEvent @event){
        this.sistema.inputDiaCargado(@event);
    }
}



