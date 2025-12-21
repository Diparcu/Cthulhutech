using Godot;
using System;
using System.Collections.Generic;

public partial class EventoConversacionShinji : Evento
{
	public EventoConversacionShinji(Dia dia) : base(dia)
	{
		this.cambiarFondo("res://Sprites/Fondos/Pasillo.png"); // Placeholder

		this.dialogos.Add(new Dialogo("Shinji", "E-eh... hola. Perdona."));
		this.dialogos.Add(new Dialogo("El chico delgado se ajusta las gafas, mirando a todos lados como si esperara que alguien lo regañe por existir."));
		this.dialogos.Add(new Dialogo("Shinji", "Estaba pensando... después de clases, algunos vamos al 'Calabozo del Androide'. Tienen... tienen mesas de cartas."));
		this.dialogos.Add(new Dialogo("Shinji", "N-no es nada ilegal, creo. Solo jugamos un poco. ¿Te gustaría venir?"));

		Dialogo opcion = new Dialogo("Decide tu respuesta:", Dialogo.DESICION);
		opcion.addOpcion(new OpcionDialogo("Suena interesante, iré.", "Aceptar"));
		opcion.addOpcion(new OpcionDialogo("No sé jugar cartas.", "Rechazar")); // Aunque la narrativa dice que enseña, es la opción B
		this.dialogos.Add(opcion);

		// Lógica post-decisión
		this.dialogos.Add(new Dialogo("Shinji sonríe, visiblemente aliviado.").setTag("Aceptar"));
		this.dialogos.Add(new Dialogo("Shinji", "¡Genial! Verás que es divertido. No apostamos mucho... usualmente.").setTag("Aceptar"));

		this.dialogos.Add(new Dialogo("Shinji asiente rápidamente.").setTag("Rechazar"));
		this.dialogos.Add(new Dialogo("Shinji", "¡Oh, no te preocupes! Es fácil. Yo te puedo enseñar las reglas básicas antes de que pierd— digo, antes de que juguemos.").setTag("Rechazar"));

		// Final común
		this.dialogos.Add(new Dialogo("Suena la campana. Shinji se sobresalta y corre hacia el salón.").setAction(() => {
			this.getJugador().updateFlag(Flags.CONOCE_A_SHINJI, true);
		}));
	}
}
