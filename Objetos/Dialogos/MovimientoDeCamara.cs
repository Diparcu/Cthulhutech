using Godot;
using System;
using System.Collections.Generic;

public class MovimientoCamara 
{
    private Vector2 posicionNueva;

    private float velocidad;
    private bool perpetuo = false;

    public MovimientoCamara(
            Vector2 posicion,
            float velocidad){
        this.posicionNueva = posicion;
        this.velocidad = velocidad;
    }

    public MovimientoCamara(
            Vector2 posicion,
            float velocidad,
            bool perpetuo){
        this.posicionNueva = posicion;
        this.velocidad = velocidad;
        this.perpetuo = perpetuo;
    }

    public void mover(Sistema sistema){
        sistema.moverCamara(this.posicionNueva, velocidad);
    }

    public void terminarMovimiento(Sistema sistema){
        if(this.perpetuo) return;
        sistema.setPosicionCamara(this.posicionNueva);
    }

    public void pasarMovimientoFondo(Dialogo dialogo){
        if(!this.perpetuo) return;
        dialogo.addMovimientoDeCamara(this);
    }
}
