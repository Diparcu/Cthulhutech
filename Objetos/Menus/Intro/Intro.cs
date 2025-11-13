using Godot;
using System;
using System.Collections.Generic;

public partial class Inicio : Node2D 
{
	private const int LONGITUD_PANTALLA = 1280;
	private const int ALTURA_PANTALLA = 640;
	private const int FONT_SIZE = 64;

	private const int CONTADOR_MAXIMO = 10;

	private const int ESTADO_PANTALLA_NEGRO_INICIAL = 0;
	private const int ESTADO_PRIMER_ICONO_FADE_IN = 1;
	private const int ESTADO_PRIMER_ICONO_SOLIDO = 2;
	private const int ESTADO_PRIMER_ICONO_FADE_OUT = 3;
	private const int ESTADO_PANTALLA_NEGRO_INTERMEDIA = 4;
	private const int ESTADO_SEGUNDO_ICONO_PRIMER_TEXTO_FADE_IN = 5;
	private const int ESTADO_SEGUNDO_ICONO_FADE_IN = 6;
	private const int ESTADO_SEGUNDO_ICONO_SOLIDO = 7;
	private const int ESTADO_SEGUNDO_ICONO_FADE_OUT = 8;
	private const int ESTADO_PANTALLA_NEGRO_FINAL = 9;

	private float contador = 0;
	private int estado = 0;

	private Sprite2D icono1 = new Sprite2D();
	private Sprite2D icono2 = new Sprite2D();
	private Color color = new Color();
	private Color color2 = new Color();
	private Sistema sistema;

	public Inicio(Sistema sistema){
		this.sistema = sistema;
	}

	public override void _Ready()
	{
		this.icono1.Texture = (Texture2D)GD.Load("res://Sprites/Icono1.svg");
		this.icono2.Texture = (Texture2D)GD.Load("res://Sprites/Icono2.png");

		this.icono1.Position = new Vector2(LONGITUD_PANTALLA/2, ALTURA_PANTALLA/2);
		this.icono2.Position = new Vector2(LONGITUD_PANTALLA/2, ALTURA_PANTALLA/2);

		this.icono1.Modulate = new Color(1, 1, 1, 0.0f);
		this.icono2.Modulate = new Color(1, 1, 1, 0.0f);

		this.AddChild(icono1);
		this.AddChild(icono2);
	}

	public override void _Process(double delta)
	{
		this.contador += 0.5f;
		this.avanzarEstado();
		this.QueueRedraw();
	}

	private void avanzarEstado(){
		if(contador >= CONTADOR_MAXIMO){
			this.estado++;
			this.contador = 0;
			if(estado == 9) this.sistema.irAPantallaDeInicio(this);
		}
	}

	public override void _Draw(){
		this.dibujarFondo();
	}


	private void dibujarFondo(){
		this.DrawRect(
				new Rect2(0,
					0,
					LONGITUD_PANTALLA,
					ALTURA_PANTALLA),
				Colors.Black
				);     
		switch(this.estado){
			case ESTADO_PRIMER_ICONO_FADE_IN:
				this.color = new Color(1, 1, 1, this.contador/CONTADOR_MAXIMO);
				this.icono1.Modulate = this.color;
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("BioBio Gaming presenta", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) + 256),
						 "BioBio Gaming presenta",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color);
				break;
			case ESTADO_PRIMER_ICONO_SOLIDO:
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("BioBio Gaming presenta", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) + 256),
						 "BioBio Gaming presenta",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color);
				break;
			case ESTADO_PRIMER_ICONO_FADE_OUT:
				this.color = new Color(1, 1, 1, (CONTADOR_MAXIMO - this.contador)/CONTADOR_MAXIMO);
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("BioBio Gaming presenta", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) + 256),
						 "BioBio Gaming presenta",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color);
				this.icono1.Modulate = this.color;
				break;
			case ESTADO_SEGUNDO_ICONO_PRIMER_TEXTO_FADE_IN:
				this.color2 = new Color(1, 1, 1, this.contador/CONTADOR_MAXIMO);
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("Una obra de", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) - 192),
						 "Una obra de",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color2);
				break;
			case ESTADO_SEGUNDO_ICONO_FADE_IN:
				this.color = new Color(1, 1, 1, this.contador/CONTADOR_MAXIMO);
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("Una obra de", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) - 192),
						 "Una obra de",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color2);
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("Team espectro", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) + 192),
						 "Team espectro",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color);
				this.icono2.Modulate = this.color;
				break;
			case ESTADO_SEGUNDO_ICONO_SOLIDO:
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("Una obra de", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) - 192),
						 "Una obra de",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color2);
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("Team espectro", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) + 192),
						 "Team espectro",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color);
				break;
			case ESTADO_SEGUNDO_ICONO_FADE_OUT:
				this.color2 = new Color(1, 1, 1, (CONTADOR_MAXIMO - this.contador)/CONTADOR_MAXIMO);
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("Una obra de", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) - 192),
						 "Una obra de",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color2);
				this.color = new Color(1, 1, 1, (CONTADOR_MAXIMO - this.contador)/CONTADOR_MAXIMO);
				 DrawString(Sistema.fuente,
						 new Vector2(LONGITUD_PANTALLA/2 - Sistema.fuente.GetStringSize("Team espectro", HorizontalAlignment.Center, -1, FONT_SIZE).X/2,
							 (ALTURA_PANTALLA/2) + 192),
						 "Team espectro",
						 HorizontalAlignment.Center,
						 -1,
						 FONT_SIZE,
						 this.color);
				this.icono2.Modulate = this.color;
				break;
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.IsReleased()) return;
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.IsReleased()) return;

		switch(@event.AsText()){
			case "Enter": 
			case "Space": 
			case "Left Mouse Button":
				this.contador = 99;
			break;
		}
	}
}


