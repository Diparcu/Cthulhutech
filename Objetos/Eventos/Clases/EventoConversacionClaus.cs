using Godot;
using System;
using System.Collections.Generic;

public partial class EventoConversacionClaus : Evento
{
	public EventoConversacionClaus(Dia dia) : base(dia)
	{
		this.cambiarFondo("res://Sprites/Fondos/Pasillo.png"); // Placeholder

		this.dialogos.Add(new Dialogo("Claus", "Tsk. ¿Cuánto falta para que abra el campo de tiro?"));
		this.dialogos.Add(new Dialogo("El chico de cabello rapado golpea impacientemente la pared con el puño. Tiene esa mirada de alguien que busca pelea o una distracción fuerte."));
		this.dialogos.Add(new Dialogo("Claus", "Estar aquí sentado escuchando teorías sobre historia o matemáticas me enferma. Deberíamos estar aprendiendo a romper cosas. O gente."));
		this.dialogos.Add(new Dialogo("Claus", "¿Tú qué me miras? Espero que en el entrenamiento de combate no seas un estorbo."));

		this.dialogos.Add(new Dialogo("Claus se aleja refunfuñando hacia el aula.").setAction(() => {
			this.getJugador().updateFlag("Conoce a Claus", true);
		}));
	}
}
