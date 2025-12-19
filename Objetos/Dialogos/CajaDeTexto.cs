using Godot;
using System;

public partial class CajaDeTexto : RichTextLabel
{

	private const int POSICION_ORIGEN_RECUADRO_X = (1280/3)*2;
	private const int POSICION_ORIGEN_RECUADRO_Y = 32;
	private const int LONGITUD_HORIZONTAL_RECUADRO = (1280/3);
	private const int LONGITUD_VERTICAL_RECUADRO = 640;

	public void comportamiento(double delta){
	}

	public void control(InputEvent @event){
	}

	public void dibujar(Node2D sistema){
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Draw()
	{
        GD.Print("");
		this.DrawRect(
				new Rect2(POSICION_ORIGEN_RECUADRO_X,
					POSICION_ORIGEN_RECUADRO_Y,
					LONGITUD_HORIZONTAL_RECUADRO,
					LONGITUD_VERTICAL_RECUADRO),
				Colors.Black
				);     
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
