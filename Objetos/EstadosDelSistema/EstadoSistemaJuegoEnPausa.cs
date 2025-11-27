using Godot;
using System;

public class SistemaEstadoJuegoEnPausa : SistemaEstado 
{
    private SistemaEstado estadoAnterior;

    public SistemaEstadoJuegoEnPausa(Sistema sistema,
            SistemaEstado estadoAnterior): base(sistema){
        this.estadoAnterior = estadoAnterior;
        sistema.setVisibilidadBotonesDePausa(true);
    }

    override public void comportamiento(Sistema sistema, double delta){}

    override public void dibujar(Sistema sistema){}

    override public void input(Sistema sistema, InputEvent @event){
		switch(@event.AsText()){
			case "Escape": 
			case "Right Mouse Button":
                this.sistema.setEstado(this.estadoAnterior);
                sistema.setVisibilidadBotonesDePausa(false);
			break;
		}
    }
}



