using Godot;
using System;
using System.Collections.Generic;

public abstract class Perk 
{
	protected String nombre;
	protected String descripcion;
	protected int nivel = 1;//Recordar cambiar estos valores si el perk lo amerita.
	protected int nivelMaximo = 1;

	public String Nombre {
		get {
			return this.nombre;
		}
	}

	protected Dictionary<string, int> bonoEstadisticas = new Dictionary<string, int>{
			{ "N/A", 0 }
		};


	protected virtual void efectoCombate(){
	}

	public int getBono(String nombre){
		if (this.bonoEstadisticas.ContainsKey(nombre)){
			return this.bonoEstadisticas[nombre] * this.nivel;
		}
		return 0;
	}

	public void subirNivel(){
		if(this.nivel >= this.nivelMaximo) return;
		this.nivel++;
	}
}
