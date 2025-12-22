using Godot;
using System;
using System.Collections.Generic;

public partial class EventoSeleccionAsiento : Evento
{
	public EventoSeleccionAsiento(Dia dia) : base(dia)
	{
		this.cambiarFondo("res://Sprites/Fondos/FONDO_UBB.jpg"); // Placeholder

		Dialogo dialogoInicial = new Dialogo("Narrador", "Entras al salón. El aire está viciado y huele a tiza y sudor frío. ¿Dónde te sientas?");

		OpcionDialogo opClaus = new OpcionDialogo("Junto a Claus");
		opClaus.setSiguienteDialogo(new List<Dialogo>{ new Dialogo("Te acercas al pupitre donde Claus golpea rítmicamente la mesa.").setFinal(typeof(EventoConversacionClaus)) });

		OpcionDialogo opShinji = new OpcionDialogo("Junto a Shinji");
		opShinji.setSiguienteDialogo(new List<Dialogo>{ new Dialogo("Te sientas al lado de Shinji, que parece intentar hacerse invisible.").setFinal(typeof(EventoConversacionShinji)) });

		dialogoInicial.addOpcion(opClaus);
		dialogoInicial.addOpcion(opShinji);

		this.dialogos.Add(dialogoInicial);
	}
}
