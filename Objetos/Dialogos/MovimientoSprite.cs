using Godot;
using System;
using System.Collections.Generic;

public class MovimientoSprite 
{
    private Sprite2D sprite;
    private Vector2 posicionNueva;
    private Vector2 posicionOriginal;
    private float velocidad;
    private float porcentaje;
    private bool perpetuo = false;

    public MovimientoSprite(
            Sprite2D sprite,
            Vector2 posicion,
            float velocidad){
        this.sprite = sprite;
        this.posicionOriginal = sprite.Position;
        this.posicionNueva = posicion;
        this.velocidad = velocidad;
    }

    public MovimientoSprite(
            Sprite2D sprite,
            Vector2 posicion,
            float velocidad,
            bool perpetuo){
        this.sprite = sprite;
        this.posicionOriginal = sprite.Position;
        this.posicionNueva = posicion;
        this.velocidad = velocidad;
        this.perpetuo = perpetuo;
    }

    public void mover(){
        this.sprite.Position = this.posicionOriginal.Lerp(
                this.posicionNueva, this.porcentaje);
        this.porcentaje += this.velocidad * 0.01f * (this.porcentaje > 1 ? 0 : 1);
    }

    public void terminarMovimiento(){
        if(this.perpetuo) return;
        this.porcentaje = 1;
        this.mover();
    }

    public void pasarMovimientoFondo(Dialogo dialogo){
        if(!this.perpetuo) return;
        dialogo.addMovimiento(this);
    }
}
