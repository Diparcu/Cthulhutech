using Godot;
using System;
using System.Collections.Generic;

public partial class EventoConversacionShinji : Evento
{
	public EventoConversacionShinji(Dia dia) : base(dia)
	{
		this.cambiarFondo("res://Sprites/Fondos/FONDO_MEDIA.jpg"); // Placeholder

		this.dialogos.Add(new Dialogo("Shinji", "E-eh... hola. Perdona."));
		this.dialogos.Add(new Dialogo("El chico delgado se ajusta las gafas, mirando a todos lados como si esperara que alguien lo regañe por existir."));
		this.dialogos.Add(new Dialogo("Shinji", "Estaba pensando... después de clases, algunos vamos al 'Calabozo del Androide'. Tienen... tienen mesas de cartas."));
		this.dialogos.Add(new Dialogo("Shinji", "N-no es nada ilegal, creo. Solo jugamos un poco."));

		// 1. Crear la rama de Aceptar
		List<Dialogo> ramaAceptar = new List<Dialogo>();
		ramaAceptar.Add(new Dialogo("Shinji sonríe, visiblemente aliviado."));
		ramaAceptar.Add(new Dialogo("Shinji", "¡Genial! Verás que es divertido. No apostamos mucho... usualmente."));
		ramaAceptar.Add(new Dialogo("Suena la campana. Shinji se sobresalta y corre hacia el salón.").addAction(() => {
			this.getJugador().updateFlag(Flags.CONOCE_A_SHINJI, true);
		}).setFinal(typeof(EventoRouterClase)));

		// 2. Crear la rama de Rechazar (o "No saber jugar")
		List<Dialogo> ramaRechazar = new List<Dialogo>();
		ramaRechazar.Add(new Dialogo("Shinji asiente rápidamente."));
		ramaRechazar.Add(new Dialogo("Shinji", "¡Oh, no te preocupes! Es fácil. Yo te puedo enseñar las reglas básicas antes de que pierd— digo, antes de que juguemos."));
		ramaRechazar.Add(new Dialogo("Suena la campana. Shinji se sobresalta y corre hacia el salón.").addAction(() => {
			this.getJugador().updateFlag(Flags.CONOCE_A_SHINJI, true);
		}).setFinal(typeof(EventoRouterClase)));

		// 3. Crear las Opciones
		OpcionDialogo op1 = new OpcionDialogo("Suena interesante, iré.");
		op1.setSiguienteDialogo(ramaAceptar);

		OpcionDialogo op2 = new OpcionDialogo("No sé jugar cartas.");
		op2.setSiguienteDialogo(ramaRechazar);

		// 4. Añadir al diálogo principal
		var dialogoDecision = new Dialogo("Shinji", "¿Te gustaría venir?");
		dialogoDecision.addOpcion(op1);
		dialogoDecision.addOpcion(op2);

		this.dialogos.Add(dialogoDecision);
	}
}
