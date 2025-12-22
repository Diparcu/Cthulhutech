using Godot;
using System;

public class SistemaEstadoMenuPrincipal : SistemaEstado 
{

    public SistemaEstadoMenuPrincipal(Sistema sistema): base(sistema){
    }

    override public void comportamiento(Sistema sistema, double delta){
    }

    override public void dibujar(Sistema sistema){
    }

    override public void input(Sistema sistema, InputEvent @event){
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.P)
        {
            sistema.iniciarPrototipoClases(sistema.getDiaCargado() ?? (Node2D)sistema.GetNode("PantallaDeInicio") ?? (Node2D)sistema);
        }
    }
}



