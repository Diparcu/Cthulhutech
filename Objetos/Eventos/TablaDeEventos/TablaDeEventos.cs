using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TablaDeEventos
{

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

    public Type GetEventoTemprano(Flags flags, int numeroDia)
    {
        return GetEvento(poolEventosObligatoriosClase, poolEventosAleatoriosClase, flags, numeroDia);
    }

    public Type GetEventoAlmuerzo(Flags flags, int numeroDia)
    {
        return GetEvento(poolEventosObligatoriosAlmuerzo, poolEventosAleatoriosAlmuerzo, flags, numeroDia);
    }

    public Type GetEventoEntrenamiento(Flags flags, int numeroDia)
    {
        return GetEvento(poolEventosObligatoriosEntrenamiento, poolEventosAleatoriosEntrenamiento, flags, numeroDia);
    }

    public Type GetEventoTarde(Flags flags, int numeroDia)
    {
        return GetEvento(poolEventosObligatoriosAventuraTarde, poolEventosAleatoriosAventuraTarde, flags, numeroDia);
    }

    public Type GetEventoNoche(Flags flags, int numeroDia)
    {
        return GetEvento(poolEventosObligatoriosAventuraNoche, poolEventosAleatoriosNoche, flags, numeroDia);
    }

    private Type GetEvento(Dictionary<int, Type> obligatorios, List<EntradaTablaDeEventos> aleatorios, Flags flags, int numeroDia)
    {
        // 1. Check Mandatory
        if (obligatorios.ContainsKey(numeroDia))
        {
            return obligatorios[numeroDia];
        }

        // 2. Filter Random Pool
        List<EntradaTablaDeEventos> disponibles = getEventosDisponibles(aleatorios, flags);

        if (disponibles.Count == 0) return null;

        // 3. Select Randomly (Weighted)
        int totalPeso = disponibles.Sum(e => e.Peso);
        int randomValue = new Random().Next(0, totalPeso);
        int currentSum = 0;

        foreach (var entrada in disponibles)
        {
            currentSum += entrada.Peso;
            if (randomValue < currentSum)
            {
                return entrada.Evento;
            }
        }

        return disponibles.Last().Evento; // Fallback
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
