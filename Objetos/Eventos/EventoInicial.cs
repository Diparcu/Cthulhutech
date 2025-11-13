using Godot;
using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class EventoInicial : Evento
{
	public EventoInicial(Dia dia): base(dia){

		Sprite2D fondo = new Sprite2D();
		Sprite2D shinji = new Sprite2D();
		
		// Cargar un fondo aleatorio
		var backgrounds = new List<string>();
		using (var dir = DirAccess.Open("res://Sprites/Inicio"))
		{
			if (dir != null)
			{
				dir.ListDirBegin();
				string fileName = dir.GetNext();
				while (fileName != "")
				{
					if (!dir.CurrentIsDir() && !fileName.EndsWith(".import"))
					{
						backgrounds.Add(fileName);
					}
					fileName = dir.GetNext();
				}
			}
			else
			{
				GD.PrintErr("No se pudo abrir el directorio 'res://Sprites/Inicio'.");
			}
		}

		if (backgrounds.Count > 0)
		{
			var random = new Random();
			int index = random.Next(backgrounds.Count);
			fondo.Texture = (Texture2D)GD.Load("res://Sprites/Inicio/" + backgrounds[index]);
		}
		else
		{
			// Fallback por si no se encuentran imagenes
			fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_UBB.jpg");
		}
		shinji.Texture = (Texture2D)GD.Load("res://Sprites/Personajes/Shinji.png");

		this.AddChild(fondo);
		this.AddChild(shinji);

		shinji.ZIndex = -1;
		shinji.Position = new Vector2(200, 270); 
		shinji.Scale = new Vector2(0.7f, 0.7f);

		fondo.ZIndex = -1;
		fondo.Position = new Vector2(1280/2, 640/2); 
		fondo.Scale = new Vector2(0.5f, 0.43f);
		 
		//this.cargarCancion("res://Musica/katawa_1.mp3");

		this.dialogos.Add(new Dialogo("Esta es una introduccion al juego. (Sip! Bienvenido al tutorial)"));
		this.dialogos.Add(new Dialogo("Narrador: ¿Como andamos, damas y caballeros?."));
		this.dialogos.Add(new Dialogo("Shinji", "Hola Jugador, ¿como te va?."));
		this.dialogos.Add(new Dialogo("Jugador", "Muy bien, Shinji, ¿y a tí?.")
				.addMovimiento(shinji, new Vector2(800, 270), 0.1f));

		Dialogo bifurcacion = new Dialogo("Shinji", "Muy bien tambien, odio al Seba. ¿Y tu?");

		List<OpcionDialogo> opciones = new List<OpcionDialogo>();

		OpcionDialogo opcion1 = new OpcionDialogo("Si lo odio.", 10, OpcionDialogo.TENACIDAD);
		OpcionDialogo opcion2 = new OpcionDialogo("No lo odio.");

		opciones.Add(opcion1);
		opciones.Add(opcion2);
		bifurcacion.setDesicion(opciones);

		List<Dialogo> odioAlSeba = new List<Dialogo>();
		List<Dialogo> noOdioAlSeba = new List<Dialogo>();
		List<Dialogo> opcion1Fallo = new List<Dialogo>();

		opcion1.setSiguienteDialogo(odioAlSeba);
		opcion1.setSiguienteDialogoFallo(opcion1Fallo);
		opcion2.setSiguienteDialogo(noOdioAlSeba);

		odioAlSeba.Add(new Dialogo("Jugador", "Yo, eeeeeh..."));
		odioAlSeba.Add(new Dialogo("Jugador", "Por supuesto que lo odio, todo el mundo lo odia, ¿que podria yo si no añadir mi granito de arena a la montaña de odio hacia el Seba?. Seba qlo, siono.")
				.setFinal());

		opcion1Fallo.Add(new Dialogo("Jugador", "Odiar al Seba?, pero porsu..."));
		opcion1Fallo.Add(new Dialogo("Entonces, sientes como tu garganta se cierra.\nSientes como el mero pensamiento de insultar se te hace imposible."));
		opcion1Fallo.Add(new Dialogo("Jugador", "No lo odio.")
				.setFinal());

		noOdioAlSeba.Add(new Dialogo("Mientete a ti mismo todo lo que quieras, pero a mi no me engañas."));
		noOdioAlSeba.Add(new Dialogo("¿Que no odias al Seba dices?"));
		noOdioAlSeba.Add(new Dialogo("Eso no se lo cree nadie."));
		noOdioAlSeba.Add(new Dialogo("Shinji", "Muy bien tambien, odio al Seba. ¿Y tu?")
				.setDesicion(opciones));

		this.dialogos.Add(bifurcacion);
		this.cargarTexto();
	}
}


