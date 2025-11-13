using Godot;
using System;

public class SistemaEstado 
{
    protected void cambiarEstado(Sistema sistema, SistemaEstado estado){
        sistema.setEstado(estado);
    }

    virtual public void inicializar(Sistema sistema){}
    virtual public void comportamiento(Sistema sistema){}
    virtual public void dibujar(Sistema sistema){}
    virtual public void input(Sistema sistema, InputEvent @event){}
}


