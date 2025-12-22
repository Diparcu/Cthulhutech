using Godot;
using System;
using System.Collections.Generic;

public partial class EventoRouterClase : Evento
{
	public EventoRouterClase(Dia dia) : base(dia)
	{
		// 1. Verificar flags
		bool vioMate = this.getJugador().getFlag("VioMatematicas");
		bool vioLecto = this.getJugador().getFlag("VioLectoescritura");

		Type proximoEvento = typeof(EventoClaseMatematicasSergio);
		string flagAActivar = "VioMatematicas";

		// 2. Lógica de selección
		if (vioMate && !vioLecto)
		{
			proximoEvento = typeof(EventoClaseLectoescrituraBryan);
			flagAActivar = "VioLectoescritura";
		}
		else if (vioLecto && !vioMate)
		{
			proximoEvento = typeof(EventoClaseMatematicasSergio);
			flagAActivar = "VioMatematicas";
		}
		else
		{
			// Si vio ambas o ninguna, aleatorio o default (Mate)
			// Por simplicidad, alternamos o default
			if(new Random().Next(0, 2) == 0)
			{
				proximoEvento = typeof(EventoClaseMatematicasSergio);
				flagAActivar = "VioMatematicas";
			}
			else
			{
				proximoEvento = typeof(EventoClaseLectoescrituraBryan);
				flagAActivar = "VioLectoescritura";
			}
		}

		// 3. Actualizar Flag
		this.getJugador().updateFlag(flagAActivar, true);

		// 4. Transición inmediata
		this.dialogos.Add(new Dialogo("...").setFinal(proximoEvento));
	}
}
