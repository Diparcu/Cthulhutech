using Godot;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract partial class SpriteSet : Node2D
{
	private const int largoPantalla = 1280;
	private const int altoPantalla = 640;

	public static readonly Vector2 HORIZONTAL_CENTRO = new Vector2(largoPantalla/3, 0); 
	public static readonly Vector2 HORIZONTAL_DERECHA = new Vector2(largoPantalla/6 * 3, 0); 
	public static readonly Vector2 HORIZONTAL_IZQUIERDA = new Vector2(largoPantalla/6, 0); 
	public static readonly Vector2 VERTICAL_ARRIBA = new Vector2(0, altoPantalla/3); 
	public static readonly Vector2 VERTICAL_ABAJO = new Vector2(0, (altoPantalla/3)*2); 

	public const string NEUTRAL = "Neutral";
	public const string INVISIBLE = "Invisible";

	protected string spriteCargado = INVISIBLE; 
	protected string spritePrevio = INVISIBLE; 
	protected Dictionary<string, List<Sprite2D>> sprites = new Dictionary<string, List<Sprite2D>>{
		{ INVISIBLE, new List<Sprite2D>() }
	}; 

	protected void inicializarPosicion(){
		this.Position += HORIZONTAL_CENTRO;
	}

	public void iniciarCambioDeSprites(string spriteNuevo){
		this.spritePrevio = this.spriteCargado;
		this.spriteCargado = spriteNuevo;
	}

	public void cambiarSprite(float transparencia){
		List<Sprite2D> spritesCargados;
		this.sprites.TryGetValue(this.spriteCargado, out spritesCargados);

		foreach(Sprite2D sprite in spritesCargados){
			sprite.Modulate = new Color(1, 1, 1, transparencia);
		}

		List<Sprite2D> spritesPrevios;
		this.sprites.TryGetValue(this.spritePrevio, out spritesPrevios);
		foreach(Sprite2D sprite in spritesPrevios){
			sprite.Modulate = new Color(1, 1, 1, 1f - transparencia);
		}


	}

	public SpriteSet setPosicionInicial(Vector2 posicion){
		this.Position = posicion;
		this.Position += VERTICAL_ARRIBA;
		return this;
	}

	public void voltearHorizontalmente(){
	}

}
