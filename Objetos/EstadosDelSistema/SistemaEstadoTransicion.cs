using Godot;
using System;

public class SistemaEstadoTransicion : SistemaEstado
{
	private TransicionDia transicion;
	private bool cargaRealizada = false;

	public SistemaEstadoTransicion(Sistema sistema, TransicionDia transicion) : base(sistema)
	{
		this.transicion = transicion;
	}

	override public void comportamiento(Sistema sistema, double delta){
		this.transicion.comportamiento(sistema, delta);

		// Logic to advance the day when screen is black
		if(this.transicion.ListoParaCargar && !cargaRealizada)
		{
			// 1. Advance Logic & UI
			// By calling sistema.avanzarDia(), we execute logic AND update UI.
			// This fixes the issue of UI being stale after fade in.
			sistema.avanzarDia();

			// 2. Update Transition Text to reflect the NEW state
			int numDia = sistema.getDiaCargado().NumeroDia;
			string nomFase = sistema.getDiaCargado().getPeriodoDelDia();
			transicion.ActualizarTexto($"Día {numDia} - {nomFase}");

			// 3. Save Game
			sistema.GuardarJuego();

			// 4. Start Fade Out
			cargaRealizada = true;
			this.transicion.IniciarFadeOut();
		}
	}

	override public void dibujar(Sistema sistema){
		// Keep drawing the scene behind?
		// transicion.comportamiento handles the Draw call if it's a child node.
		// However, Sistema._Draw calls this.estado.dibujar.
		// If we don't draw the underlying day, the screen might flash unless Transicion covers everything properly.
		// Transicion covers everything with black rect, so we might not need to draw the day.
		// But to be safe in fade phases:
		if(sistema.getDiaCargado() != null) sistema.getDiaCargado().dibujar(sistema);
	}

	override public void input(Sistema sistema, InputEvent @event){
		// Block input during transition
	}
}
