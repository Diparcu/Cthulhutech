using Godot;
using System;
using System.Collections.Generic;

public partial class EventoMegan : Evento 
{
	public EventoMegan(Dia dia): base(dia){

		Sprite2D fondo = new Sprite2D();
		Sprite2D megan = new Sprite2D();
		
		fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_RAVE.png");
		megan.Texture = (Texture2D)GD.Load("res://Sprites/Personajes/Megan.png");

		this.AddChild(fondo);
		this.AddChild(megan);

		megan.ZIndex = -1;
		megan.Position = new Vector2(-500, 500);
		megan.Scale = new Vector2(0.7f, 0.7f);

		fondo.ZIndex = -1;
		fondo.Position = new Vector2(1280/2, 640/2); 
		fondo.Scale = new Vector2(1, 1);
		 
		this.dialogos.Add(new Dialogo("Entras a la disco. La música electrónica resuena en tus oídos y las luces de neón te ciegan."));
		this.dialogos.Add(new Dialogo("Una figura imponente se abre paso entre la multitud y se acerca a ti."));
		this.dialogos.Add(new Dialogo("Es una mujer con orejas y cola de animal. Su cuerpo es musculoso y sus uñas de los pies son como garras."));
		this.dialogos.Add(new Dialogo("Megan", "¡Hola! ¿Eres nuevo por aquí?")
            .addMovimiento(megan, new Vector2(200, 500), 0.1f));
		this.dialogos.Add(new Dialogo("Jugador", "Sí, es mi primera vez."));
		this.dialogos.Add(new Dialogo("Megan", "¡Genial! Soy Megan. ¡Bienvenido a la fiesta!"));
        this.dialogos.Add(new Dialogo("Jugador", "Gracias. ¿Qué es este lugar?"));
        this.dialogos.Add(new Dialogo("Megan", "Es el 'Nexus', el mejor club de la ciudad. Aquí puedes encontrar de todo."));

        Dialogo bifurcacion = new Dialogo("Megan", "Y dime, ¿qué te trae por aquí? ¿Buscas algo en particular?");

        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        OpcionDialogo opcion1 = new OpcionDialogo("Vengo a hacer contactos.", 10, "Labia");
        OpcionDialogo opcion2 = new OpcionDialogo("Solo estoy mirando.");

        opciones.Add(opcion1);
        opciones.Add(opcion2);
        bifurcacion.setDesicion(opciones);

        List<Dialogo> exito = new List<Dialogo>();
        List<Dialogo> fallo = new List<Dialogo>();
        List<Dialogo> mirando = new List<Dialogo>();

        opcion1.setSiguienteDialogo(exito);
        opcion1.setSiguienteDialogoFallo(fallo);
        opcion2.setSiguienteDialogo(mirando);

        exito.Add(new Dialogo("Megan", "Je, un negociante. Me gusta. Conozco a alguien que te puede interesar.").setFinal());

        fallo.Add(new Dialogo("Megan", "Ya veo. Bueno, si cambias de opinión, búscame.").setFinal());

        mirando.Add(new Dialogo("Megan", "Aburrido. Bueno, diviértete.").setFinal());

        this.dialogos.Add(bifurcacion);

		this.cargarTexto();
	}
}
