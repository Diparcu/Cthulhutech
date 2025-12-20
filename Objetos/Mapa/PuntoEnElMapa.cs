using Godot;
using System;

public partial class NodoMapa : Node2D
{
    private Sprite2D spriteDefecto = new Sprite2D();
    private Sprite2D spriteSeleccionado = new Sprite2D();
    public String nombre;

	public NodoMapa(String nombre){
        this.nombre = nombre;
        this.ZIndex = -1;
        this.spriteSeleccionado.Texture = (Texture2D)GD.Load("res://Sprites/Iconos/CosoDelMapaSeleccionado.png");
        this.spriteSeleccionado.Visible = false;
        this.spriteDefecto.Texture = (Texture2D)GD.Load("res://Sprites/Iconos/CosoDelMapa.png");
        this.AddChild(spriteSeleccionado);
        this.AddChild(spriteDefecto);
	}

    public void setSeleecionado(bool seleccionado){
        this.spriteDefecto.Visible = !seleccionado;
        this.spriteSeleccionado.Visible = seleccionado;
    }

    public NodoMapa setPosition(Vector2 posicion){
        this.Position = posicion;
        return this;
    }

}
