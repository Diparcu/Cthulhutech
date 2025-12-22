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
            Node2D nodoAEliminar = null;
            // Buscamos cuál es la pantalla activa iterando los hijos
            foreach(Node child in sistema.GetChildren()){
                // Lista de posibles vistas del menú que queremos limpiar
                if(child is PantallaDeInicio || child is CreadorDePersonaje ||
                   child is SeleccionOrigen || child is SeleccionArquetipo ||
                   child is Inicio) // Agrega otros si es necesario
                {
                    nodoAEliminar = (Node2D)child;
                    break;
                }
            }
            if(nodoAEliminar != null){
                GD.Print("Iniciando Prototipo Clases... Eliminando: " + nodoAEliminar.Name);
                sistema.iniciarPrototipoClases(nodoAEliminar);
            } else {
                GD.PrintErr("Error Debug: No se encontró una vista válida para eliminar.");
            }
        }
    }
}



