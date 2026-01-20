using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TablaDeEventos
{

	public const string CLASE = "Clase";
	public const string PREVIO_CLASE = "Previo a clase";
	public const string INTERACCION_CLASE = "Interaccion en clase";
	public const string POSTERIOR_CLASE = "Posterior a clase";
	public const string ALMUERZO = "Almuerzo";
	public const string PREVIO_ALMUERZO = "Previo almuerzo";
	public const string POSTERIOR_ALMUERZO = "Posterior almuerzo";
	public const string ENTRENAMIENTO = "Entrenamiento";
	public const string PREVIO_ENTRENAMIENTO = "Previo entrenamiento";
	public const string POSTERIOR_ENTRENAMIENTO = "Posterior entrenamiento";
	public const string TARDE = "Tarde";
	public const string PREVIO_TARDE = "Previo tarde";
	public const string POSTERIOR_TARDE = "Posterior tarde";
	public const string NOCHE = "Noche";
	public const string PREVIO_NOCHE = "Previo noche";
	public const string POSTERIOR_NOCHE = "Posterior noche";


	private Dictionary<int, Type> poolEventosObligatoriosClase = new Dictionary<int, Type>{
		{1, typeof(EventoClaseDia1)}
	};

	private Dictionary<int, Type> poolEventosObligatoriosInteraccionClase = new Dictionary<int, Type>{
	};

	private Dictionary<int, Type> poolEventosObligatoriosAlmuerzo = new Dictionary<int, Type>{
	};

	private Dictionary<int, Type> poolEventosObligatoriosEntrenamiento = new Dictionary<int, Type>{
	};

	private Dictionary<int, Type> poolEventosObligatoriosDespuesDeClase = new Dictionary<int, Type>{
	};

	private Dictionary<int, Type> poolEventosObligatoriosAventuraTarde = new Dictionary<int, Type>{
	};

	private Dictionary<int, Type> poolEventosObligatoriosAventuraNoche = new Dictionary<int, Type>{
	};

	private Dictionary<int, Type> poolEventosObligatoriosSueno = new Dictionary<int, Type>{
	};


	private List<EntradaTablaDeEventos> poolEventosAleatoriosClase = new List<EntradaTablaDeEventos>{
	};

	private List<EntradaTablaDeEventos> poolEventosAleatoriosInteraccionClase = new List<EntradaTablaDeEventos>{
		new EntradaTablaDeEventos(typeof(EventoShinjiClase1), 
				new List<string>{
					Flags.CLASE_ASIENTO_TRASERO
				},
				new List<string>{
					Flags.CONOCE_A_SHINJI
				}),
	};

	private List<EntradaTablaDeEventos> poolEventosAleatoriosAlmuerzo = new List<EntradaTablaDeEventos>{
	};

	private List<EntradaTablaDeEventos> poolEventosAleatoriosEntrenamiento = new List<EntradaTablaDeEventos>{
	};

	private List<EntradaTablaDeEventos> poolEventosAleatoriosDespuesDeClase = new List<EntradaTablaDeEventos>{
	};

	private List<EntradaTablaDeEventos> poolEventosAleatoriosAventuraTarde = new List<EntradaTablaDeEventos>{
	};

	private List<EntradaTablaDeEventos> poolEventosAleatoriosNoche = new List<EntradaTablaDeEventos>{
	};

	private List<EntradaTablaDeEventos> poolEventosAleatoriosSueno = new List<EntradaTablaDeEventos>{
	};

	public void agregarEvento(EntradaTablaDeEventos evento,
			string pool){
		Dictionary<string, List<EntradaTablaDeEventos>> tablaEventosAleatoreos = this.getTablaEventosAleatoreos();
		List<EntradaTablaDeEventos> eventosAleatoreos;
		tablaEventosAleatoreos.TryGetValue(pool, out eventosAleatoreos);
		eventosAleatoreos.Add(evento);
	}

	public Type getProximoEvento(Flags flags, string fase, int dia){

		Type eventoObligatorio = this.getProximoEventoObligatorio(fase, dia);
		if(eventoObligatorio != null) return eventoObligatorio;

		return getProximoEventoAleatoreo(flags, fase, dia);
	}

	private Dictionary<string, List<EntradaTablaDeEventos>>  getTablaEventosAleatoreos(){
		return new Dictionary<string, List<EntradaTablaDeEventos>>{//TODO
			{ PREVIO_CLASE, this.poolEventosAleatoriosNoche },
			{ CLASE, this.poolEventosAleatoriosNoche },
			{ INTERACCION_CLASE, this.poolEventosAleatoriosInteraccionClase },
			{ POSTERIOR_CLASE, this.poolEventosAleatoriosDespuesDeClase },

			{ PREVIO_ALMUERZO, this.poolEventosAleatoriosNoche },
			{ ALMUERZO, this.poolEventosAleatoriosNoche },
			{ POSTERIOR_ALMUERZO, this.poolEventosAleatoriosNoche },

			{ PREVIO_ENTRENAMIENTO, this.poolEventosAleatoriosNoche },
			{ ENTRENAMIENTO, this.poolEventosAleatoriosNoche },
			{ POSTERIOR_ENTRENAMIENTO, this.poolEventosAleatoriosNoche },

			{ PREVIO_TARDE, this.poolEventosAleatoriosNoche },
			{ TARDE, this.poolEventosAleatoriosNoche },
			{ POSTERIOR_TARDE, this.poolEventosAleatoriosNoche },

			{ NOCHE, this.poolEventosAleatoriosNoche },
			{ PREVIO_NOCHE, this.poolEventosAleatoriosNoche },
			{ POSTERIOR_NOCHE, this.poolEventosAleatoriosNoche },
		};
	} 

	private Dictionary<string, Dictionary<int, Type>> getTablaEventosObligatorios(){
		return new Dictionary<string, Dictionary<int, Type>>{
			{ PREVIO_CLASE, new Dictionary<int, Type>() },
			{ CLASE, this.poolEventosObligatoriosClase },
			{ INTERACCION_CLASE, this.poolEventosObligatoriosInteraccionClase },
			{ POSTERIOR_CLASE, this.poolEventosObligatoriosClase },

			{ PREVIO_ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ POSTERIOR_ALMUERZO, this.poolEventosObligatoriosAlmuerzo },

			{ PREVIO_ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ POSTERIOR_ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },

			{ PREVIO_TARDE, this.poolEventosObligatoriosClase },
			{ TARDE, this.poolEventosObligatoriosClase },
			{ POSTERIOR_TARDE, this.poolEventosObligatoriosClase },

			{ PREVIO_NOCHE, new Dictionary<int, Type>() },
			{ NOCHE, this.poolEventosObligatoriosSueno },
			{ POSTERIOR_NOCHE, this.poolEventosObligatoriosClase },
		};
	} 

	private Type getProximoEventoObligatorio(string fase, int dia)
	{
		Dictionary<string, Dictionary<int, Type>> tablaEventosObligatorios = this.getTablaEventosObligatorios();

		Dictionary<int, Type> tablaEventosObligatiorsDeFase;
		tablaEventosObligatorios.TryGetValue(fase, out tablaEventosObligatiorsDeFase);

		Type eventoObligatorio;  
		tablaEventosObligatiorsDeFase.TryGetValue(dia, out eventoObligatorio);

		return eventoObligatorio;
	}

	private Type getProximoEventoAleatoreo(Flags flags, string fase, int dia)
	{
		Dictionary<string, List<EntradaTablaDeEventos>> tablaEventosAleatoreos = this.getTablaEventosAleatoreos();

		List<EntradaTablaDeEventos> tablaEventosAleatoreosDeFase;
		tablaEventosAleatoreos.TryGetValue(fase, out tablaEventosAleatoreosDeFase);

		List<EntradaTablaDeEventos> eventosDisponibles = this.getEventosDisponibles(tablaEventosAleatoreosDeFase, flags);

		int total = eventosDisponibles.Sum(e => e.Peso);
		int r = Random.Shared.Next(total);
		double acumulado = 0;
		foreach (var e in eventosDisponibles)
		{
			acumulado += e.Peso;
			if (r < acumulado)
				return e.Evento;
		}

		return this.getEventoPorDefecto(fase);
	}

	private Type getEventoPorDefecto(string fase){

		Dictionary<string, Type> tablaDeEventosPorDefecto =
			new Dictionary<string, Type>{
			{ PREVIO_CLASE, typeof(EventoGenericoClase) },
			{ CLASE, typeof(ClasePorDefecto) },
			{ INTERACCION_CLASE, typeof(InteraccionClasePorDefecto) },
			{ POSTERIOR_CLASE, typeof(EventoGenericoAlmuerzo) },

			{ PREVIO_ALMUERZO, typeof(EventoGenericoAlmuerzo) },
			{ ALMUERZO, typeof(AlmuerzoPorDefecto) },
			{ POSTERIOR_ALMUERZO, typeof(EventoGenericoEntrenamiento) },

			{ PREVIO_ENTRENAMIENTO, typeof(EventoGenericoEntrenamiento) },
			{ ENTRENAMIENTO, typeof(EntrenamientoPorDefecto) },
			{ POSTERIOR_ENTRENAMIENTO, typeof(EventoGenericoDespuesDeClase) },

			{ PREVIO_TARDE, typeof(EventoGenericoDespuesDeClase) },
			{ TARDE, typeof(EventoGenericoDespuesDeClase) },
			{ POSTERIOR_TARDE, typeof(EventoGenericoNoche) },

			{ PREVIO_NOCHE, typeof(EventoGenericoNoche) },
			{ NOCHE, typeof(NochePorDefecto) },
			{ POSTERIOR_NOCHE, typeof(EventoGenericoClase) },
		};

		Type eventoPorDefecto;
		tablaDeEventosPorDefecto.TryGetValue(fase, out eventoPorDefecto);

		return eventoPorDefecto;
	}

	private Evento getEventoClase(Flags flags)
	{
		List<EntradaTablaDeEventos> eventos = new List<EntradaTablaDeEventos>(this.poolEventosAleatoriosClase);

		eventos = this.getEventosDisponibles(eventos, flags);

		return null;
	}

	private List<EntradaTablaDeEventos> getEventosDisponibles(
			List<EntradaTablaDeEventos> eventos,
			Flags flags)
	{
		List<EntradaTablaDeEventos> eventosDisponibles = this.getEventosConFlagsRequeridas(eventos, flags);
		eventosDisponibles = this.getEventosSinFlagsProhibitivas(eventosDisponibles, flags);

		return eventosDisponibles;
	}

	private List<EntradaTablaDeEventos> getEventosConFlagsRequeridas(
			List<EntradaTablaDeEventos> eventos,
			Flags flags)
	{
		List<EntradaTablaDeEventos> eventosConFlags = eventos
			.Where(evento => evento.FlagsRequeridas == null ||
					evento.FlagsRequeridas
					.All(flag => flags.Flag.TryGetValue(flag, out bool valor) && valor))
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
						 flags.Flag.TryGetValue(f, out var v) && v)))
			.ToList();


		return eventosSinFlags;
	}
}
