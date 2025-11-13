using Godot;
using System;
using System.Collections.Generic;

public partial class CambioDeMusica 
{
	public const String CAMBIO = "Cambiar";
	public const String DETENER = "Detener";

	private String tipo;
	private String direccion;

	public CambioDeMusica(String direccion){
		this.tipo = CAMBIO;
		this.direccion = direccion;
	}

	public CambioDeMusica(){
		this.tipo = DETENER;
	}

	public void cambiarMusica(AudioStreamPlayer audioStreamer){
		if(this.tipo == CAMBIO){
			this.cargarCancionNueva(audioStreamer);
		}else{
			this.pararCancion(audioStreamer);
		}
	}

	private void pararCancion(AudioStreamPlayer audioStreamer){
		audioStreamer.Stop();
	}

	private void cargarCancionNueva(AudioStreamPlayer audioStreamer){
		AudioStream miCancion = (AudioStream)ResourceLoader.Load(this.direccion);
		audioStreamer.Stream = miCancion;
		audioStreamer.Play();
	}
}


