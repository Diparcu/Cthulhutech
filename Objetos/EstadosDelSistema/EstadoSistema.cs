using Godot;
using System;

public class SistemaEstado 
{
    protected Sistema sistema;
    public bool hojaDePersonajeAbrible = false;

    public SistemaEstado(Sistema sistema){
        this.sistema = sistema;
    }

    protected void cambiarEstado(Sistema sistema, SistemaEstado estado){
        sistema.setEstado(estado);
    }

    virtual public void comportamiento(Sistema sistema, double delta){}
    virtual public void dibujar(Sistema sistema){}
    virtual public void input(Sistema sistema, InputEvent @event){}
}


