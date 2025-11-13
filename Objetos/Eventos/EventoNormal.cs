using Godot;
using System;
using System.Collections.Generic;

public partial class EventoNormal : Evento 
{
	public EventoNormal(Dia dia): base(dia){

        Sprite2D fondo = new Sprite2D();
        fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_MEDIA.jpg");
        fondo.ZIndex = -1;
		fondo.Position = new Vector2(1280/2, 640/2);
        this.AddChild(fondo);

		string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                            "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                            "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                            "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

		this.dialogos.Add(new Dialogo(loremIpsum));
        this.dialogos.Add(new Dialogo("").addAction(() => { this.dia.getSistema().volverAlMenuPrincipal(); }));

		this.cargarTexto();
	}
}
