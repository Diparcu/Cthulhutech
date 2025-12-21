using Godot;
using System;
using System.Collections.Generic;

public partial class EventoClaseMatematicasSergio : Evento
{
	public EventoClaseMatematicasSergio(Dia dia) : base(dia)
	{
		// Fondo de clase
		this.cambiarFondo("res://Sprites/Fondos/Clase.png"); // Placeholder path

		this.dialogos.Add(new Dialogo("Sergio", "¡Atención! Dejen de babear sobre los pupitres. Esto no es un recreo, es balística aplicada."));
		this.dialogos.Add(new Dialogo("Sergio", "Si un proyectil sale con un ángulo de 30 grados y una velocidad de 800 m/s... ¿dónde impactará si el viento sopla en contra a 5 m/s?"));

		this.dialogos.Add(new Dialogo("Sergio", "Tú. Sí, tú, el que tiene cara de no saber ni sumar dos más dos. ¡Responde!").setAction(RealizarTirada));
	}

	private void RealizarTirada()
	{
		// Tirada: Ciencias Físicas (INT) + Inteligencia
		// Dificultad 12
		var resultado = this.realizarTiradaCombinacion("CienciasFisicas", "Inteligencia", 12);
		string color = resultado.Exito ? "green" : "red";
		string textoResultado = resultado.Exito ? "Éxito" : "Fallo";

		this.dialogos.Add(new Dialogo($"[color=darkgreen][Tirada Oculta - Ciencias Físicas diff 12: {string.Join(", ", resultado.DadosLanzados)} | {resultado.CombinacionNombre} ({resultado.SumaCombinacion})][/color]"));

		if (resultado.Exito)
		{
			this.dialogos.Add(new Dialogo("Calculas rápidamente la parábola y el factor de resistencia del aire. Das la coordenada exacta."));
			this.dialogos.Add(new Dialogo("Sergio", "Hmm. Correcto. Al menos uno de ustedes no es tan inútil como parece. No te confíes."));
			this.dialogos.Add(new Dialogo("Ganaste 10 XP").setAction(() => {
				this.getJugador().XP += 10;
			}));
		}
		else
		{
			this.dialogos.Add(new Dialogo("Balbuceas unos números al azar. Sergio te mira fijamente, y lentamente saca una libreta negra."));
			this.dialogos.Add(new Dialogo("Sergio", "Patético. Si eso fuera una granada, acabas de matar a todo tu pelotón."));
			this.dialogos.Add(new Dialogo("[color=red]Anotación Negativa al Expediente[/color]").setAction(() => {
				this.getJugador().updateFlag("AnotacionNegativa", true);
			}));
		}

		// Cargar el texto del primer dialogo generado dinamicamente
		this.cargarTexto();
	}
}
