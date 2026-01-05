using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TablaDeEventos
{

	public const string CLASE = "Clase";
	public const string PREVIO_CLASE = "Previo a clase";
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

	public Type getProximoEvento(Flags flags, string fase, int dia){

		Type eventoObligatorio = this.getProximoEventoObligatorio(fase, dia);
		if(eventoObligatorio != null) return eventoObligatorio;

		return getProximoEventoAleatoreo(flags, fase, dia);
	}

	private Type getProximoEventoObligatorio(string fase, int dia)
	{
		Dictionary<string, Dictionary<int, Type>> tablaEventosObligatorios = new Dictionary<string, Dictionary<int, Type>>{
			{ CLASE, this.poolEventosObligatoriosClase },
			{ PREVIO_CLASE, this.poolEventosObligatoriosClase },
			{ POSTERIOR_CLASE, this.poolEventosObligatoriosClase },
			{ ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ PREVIO_ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ POSTERIOR_ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ PREVIO_ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ POSTERIOR_ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ TARDE, this.poolEventosObligatoriosClase },
			{ PREVIO_TARDE, this.poolEventosObligatoriosClase },
			{ POSTERIOR_TARDE, this.poolEventosObligatoriosClase },
			{ NOCHE, this.poolEventosObligatoriosClase },
			{ PREVIO_NOCHE, this.poolEventosObligatoriosClase },
			{ POSTERIOR_NOCHE, this.poolEventosObligatoriosClase },
		};

		Dictionary<int, Type> tablaEventosObligatiorsDeFase;
		tablaEventosObligatorios.TryGetValue(fase, out tablaEventosObligatiorsDeFase);

		Type eventoObligatorio;  
		tablaEventosObligatiorsDeFase.TryGetValue(dia, out eventoObligatorio);

		return eventoObligatorio;
	}

	private Type getProximoEventoAleatoreo(Flags flags, string fase, int dia)
	{
		Dictionary<string, Dictionary<int, Type>> tablaEventosAleatoreos = new Dictionary<string, Dictionary<int, Type>>{
			{ CLASE, this.poolEventosObligatoriosClase },
			{ PREVIO_CLASE, this.poolEventosObligatoriosClase },
			{ POSTERIOR_CLASE, this.poolEventosObligatoriosClase },
			{ ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ PREVIO_ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ POSTERIOR_ALMUERZO, this.poolEventosObligatoriosAlmuerzo },
			{ ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ PREVIO_ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ POSTERIOR_ENTRENAMIENTO, this.poolEventosObligatoriosEntrenamiento },
			{ TARDE, this.poolEventosObligatoriosClase },
			{ PREVIO_TARDE, this.poolEventosObligatoriosClase },
			{ POSTERIOR_TARDE, this.poolEventosObligatoriosClase },
			{ NOCHE, this.poolEventosObligatoriosClase },
			{ PREVIO_NOCHE, this.poolEventosObligatoriosClase },
			{ POSTERIOR_NOCHE, this.poolEventosObligatoriosClase },
		};

		Dictionary<int, Type> tablaEventosAleatoreosDeFase;
		tablaEventosAleatoreos.TryGetValue(fase, out tablaEventosAleatoreosDeFase);

		Type eventoObligatorio;
		tablaEventosAleatoreosDeFase.TryGetValue(dia, out eventoObligatorio);

		return eventoObligatorio;
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
