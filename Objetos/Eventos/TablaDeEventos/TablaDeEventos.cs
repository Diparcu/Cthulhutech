using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TablaDeEventos
{
		private List<EntradaTablaDeEventos> eventosClase = new List<EntradaTablaDeEventos>{
			new EntradaTablaDeEventos(typeof(EventoDia0Isla),
					new List<String>(){ Flags.ISLENO },
					null,
					20),

			new EntradaTablaDeEventos(typeof(EventoDia0Chud),
					new List<String>(){ Flags.CHUD },
					null),

			new EntradaTablaDeEventos(typeof(EventoCasaJugadorIsla), 
					null, 
					new List<String>(){ Flags.CHUD }),

			new EntradaTablaDeEventos(typeof(EventoCasaJugadorIsla), 
					null, 
					null),
		};

		private List<EntradaTablaDeEventos> eventosAlmuerzo = new List<EntradaTablaDeEventos>{
		};

		private List<EntradaTablaDeEventos> eventosEntrenamiento = new List<EntradaTablaDeEventos>{
		};

		private List<EntradaTablaDeEventos> eventosDespuesDeClase = new List<EntradaTablaDeEventos>{
		};

		private List<EntradaTablaDeEventos> eventosAventuraTarde = new List<EntradaTablaDeEventos>{
		};

		private List<EntradaTablaDeEventos> eventosNoche = new List<EntradaTablaDeEventos>{
		};

		private List<EntradaTablaDeEventos> eventosSueno = new List<EntradaTablaDeEventos>{
		};

        public Evento getProximoEvento(Flags flags){
            return null;
        }

		private Evento getEventoTemprano(Flags flags)
		{
			List<EntradaTablaDeEventos> eventos = new List<EntradaTablaDeEventos>(this.eventosClase);

			eventos = this.getEventosDisponibles(eventos, flags);

			return null;
		}

		private List<EntradaTablaDeEventos> getEventosDisponibles(
				List<EntradaTablaDeEventos> eventos,
				Flags flags)
		{
			List<EntradaTablaDeEventos> eventosDisponibles = this.getEventosConFlagsRequeridas(eventos, flags);
			eventosDisponibles = this.getEventosSinFlagsProhibitivas(eventosDisponibles, flags);

			foreach(EntradaTablaDeEventos wa in eventosDisponibles){
				GD.Print(wa);
			}

			return eventosDisponibles;
		}

		private List<EntradaTablaDeEventos> getEventosConFlagsRequeridas(
				List<EntradaTablaDeEventos> eventos,
				Flags flags)
		{
			List<EntradaTablaDeEventos> eventosConFlags = eventos
				.Where(evento => evento.FlagsRequeridas == null ||
						evento.FlagsRequeridas
						.All(flag => flags.Variables.TryGetValue(flag, out bool valor) && valor))
				.ToList();

			return eventosConFlags;
		}

		private List<EntradaTablaDeEventos> getEventosSinFlagsProhibitivas(
				List<EntradaTablaDeEventos> eventos,
				Flags flags)
		{
			List<EntradaTablaDeEventos> eventosSinFlags = eventos
				.Where(evento =>
						(evento.FlagsProhibitivas == null ||
						 !evento.FlagsProhibitivas.Any(f =>
							 flags.Variables.TryGetValue(f, out var v) && v)))
				.ToList();


			return eventosSinFlags;
		}
}
