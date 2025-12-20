using Godot;
using System;
using System.Collections.Generic;

public partial class CambioDeFondo 
{
    private Texture2D fondo;

	public CambioDeFondo(String direccion){
		this.fondo = GD.Load<Texture2D>(direccion);
	}

	public void iniciarCambioDeFondo(Evento evento){
        evento.iniciarCambioDeFondo(this.fondo);
	}

	public void cambiarFondoLentamente(Evento evento){
        evento.cambiarFondoLentamente();
	}

	public void terminarDeCambiarFondo(Evento evento){
        evento.cambiarFondo(this.fondo);
	}
}


