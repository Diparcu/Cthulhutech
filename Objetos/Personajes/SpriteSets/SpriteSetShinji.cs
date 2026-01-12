using Godot;
using System.Collections.Generic;
using System;

public partial class SpriteSetShinji : SpriteSet
{
	   
	public const string FELIZ = "Feliz";

	public SpriteSetShinji(Evento evento){
		this.inicializarSprites(evento);
	}

	private void inicializarSprites(Evento evento){
		evento.AddChild(this);
		this.Position += HORIZONTAL_CENTRO;
		this.Position += VERTICAL_ARRIBA;

		Sprite2D neutral = new Sprite2D();
		Sprite2D sonriza = new Sprite2D();

		neutral.Texture = (Texture2D)GD.Load("res://Sprites/Personajes/Shinji/Shinji.png");
		sonriza.Texture = (Texture2D)GD.Load("res://Sprites/Personajes/Shinji/Shinji_sonriza.png");

		neutral.ZIndex = 2;
		sonriza.ZIndex = 3;

		neutral.Modulate = new Color(1, 1, 1, 0);
		sonriza.Modulate = new Color(1, 1, 1, 0);

		neutral.Position += new Vector2(0, 350);
		sonriza.Position += new Vector2(23, -305);
		sonriza.Position += new Vector2(0, 350);

		Sprite2D neutral2 = (Sprite2D)neutral.Duplicate();

		sprites.Add( NEUTRAL, new List<Sprite2D>{ neutral });
		sprites.Add( FELIZ, new List<Sprite2D>{ neutral2, sonriza });

		this.AddChild(neutral);
		this.AddChild(neutral2);
		this.AddChild(sonriza);
	}

}
