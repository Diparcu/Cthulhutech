using Godot;
using System;
using System.Collections.Generic;

public partial class EventoClaseLectoescrituraBryan : Evento
{
	public EventoClaseLectoescrituraBryan(Dia dia) : base(dia)
	{
		this.cambiarFondo("res://Sprites/Fondos/Clase.png");

		this.dialogos.Add(new Dialogo("Bryan", "Buenos días. Hoy no analizaremos a Shakespeare. Hoy analizaremos esto."));
		this.dialogos.Add(new Dialogo("El profesor proyecta un texto lleno de símbolos extraños, partes borrosas y sintaxis rota. Parece un archivo recuperado de un disco duro dañado... o algo peor."));
		this.dialogos.Add(new Dialogo("Bryan", "El lenguaje evoluciona, se corrompe. A veces, lo que está oculto entre el ruido es más importante que el mensaje claro. Descifrenlo."));
		this.dialogos.Add(new Dialogo("Te concentras en el patrón de los glifos corruptos...").setAction(RealizarTirada));
	}

	private void RealizarTirada()
	{
		// Tirada: Lectoescritura (INT) + Inteligencia
		// Dificultad 10
		var resultado = this.realizarTiradaCombinacion("Lectoescritura", "Inteligencia", 10);

		this.dialogos.Add(new Dialogo($"[color=darkgreen][Tirada Oculta - Lectoescritura diff 10: {string.Join(", ", resultado.DadosLanzados)} | {resultado.CombinacionNombre} ({resultado.SumaCombinacion})][/color]"));

		if (resultado.Exito)
		{
			this.dialogos.Add(new Dialogo("Tus ojos filtran el ruido estático. Las letras se reordenan en tu mente. Es un informe de suministros... pero las fechas son del futuro."));
			this.dialogos.Add(new Dialogo("Bryan", "Veo en tu cara que lo has visto. El subtexto. Muy bien. Mantén esa agudeza."));
			this.dialogos.Add(new Dialogo("Ganaste 10 XP").setAction(() => {
				this.getJugador().XP += 10;
			}));
		}
		else
		{
			this.dialogos.Add(new Dialogo("Los símbolos parecen bailar y retorcerse. Intentas seguirlos, pero te empieza a palpitar la sien."));
			this.dialogos.Add(new Dialogo("Bryan", "No fuerces la vista si no tienes la mente preparada. Solo verás caos donde hay orden."));
			this.dialogos.Add(new Dialogo("Te duele la cabeza y no entiendes nada."));
		}

		this.cargarTexto();
	}
}
