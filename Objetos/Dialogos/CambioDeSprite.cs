using Godot;
using System;
using System.Collections.Generic;

public partial class CambioDeSprite 
{
    SpriteSet sprite;
    String spriteNuevo;
    float transparencia = 0f;

	public CambioDeSprite(SpriteSet sprite, String spriteNuevo){
        this.sprite = sprite;
        this.spriteNuevo = spriteNuevo;
	}

	public void cambiarSprite(){
        if(this.transparencia == 0f) this.sprite.iniciarCambioDeSprites(this.spriteNuevo);
		if(this.transparencia > 1) return;
        this.transparencia += 0.04f;
        sprite.cambiarSprite(this.transparencia);
	}

	private void terminarCambioDeSprite(){
        sprite.cambiarSprite(1);
	}
}


