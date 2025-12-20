using Godot;
using System;

public class SistemaEstadoJugando : SistemaEstado 
{

    public SistemaEstadoJugando(Sistema sistema): base(sistema){
        this.hojaDePersonajeAbrible = true;
    }

    override public void comportamiento(Sistema sistema, double delta){
        this.sistema.comportamientoDiaCargado(delta);
    }

    override public void dibujar(Sistema sistema){
        this.sistema.dibujarDiaCargado();
    }

    override public void input(Sistema sistema, InputEvent @event){
		switch(@event.AsText()){
			case "Right Mouse Button":
                this.sistema.setEstado(new SistemaEstadoJuegoEnPausa(
                            this.sistema,
                            this));
            return;
		}

        this.sistema.inputDiaCargado(@event);
    }
}



