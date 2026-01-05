using Godot;
using System.Collections.Generic;
using System;

public abstract class SpriteSetShinji : SpriteSet
{
       
	public const string NEUTRAL = "Neutral";
	public const string FELIZ = "Feliz";

    private Sprite2D neutral = new Sprite2D();
    private Sprite2D feliz = new Sprite2D();

    private string estado = NEUTRAL;

    public SpriteSetShinji(){
        this.inicializarPosicion();
        this.inicializarSprites();
        //this.testearWeas();
    }

    override public void cambiarSprite(string sprite){

        this.estado = sprite;
        this.invisibilizarTodosLosSprites();

        switch(sprite){
            case NEUTRAL:
                this.neutral.Visible = true;
                break;
            case FELIZ:
                this.neutral.Visible = true;
                this.feliz.Visible = true;
                break;
        }
    } 

    public void voltearHorizontalmente(){
    }

    private void invisibilizarTodosLosSprites(){
        this.neutral.Visible = false;
        this.feliz.Visible = false;
    }

    private void visibilizarSprite(){
    }

    private void inicializarPosicion(){
        this.Position = new Vector2(500, 500);
    }

    private void inicializarSprites(){
		this.neutral.Texture = (Texture2D)GD.Load("res://Sprites/Shinji.png");
		this.feliz.Texture = (Texture2D)GD.Load("res://Sprites/Shinji_feliz.png");

        this.neutral.ZIndex = 2;
        this.feliz.ZIndex = 3;

        this.feliz.Position += new Vector2(23, -305);
        this.feliz.Visible = false;

        this.AddChild(neutral);
        this.neutral.AddChild(feliz);
    }

    private void testearWeas(){
        this.neutral.Visible = true;
        this.feliz.Visible = true;
    }

}
