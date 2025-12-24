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

    public Evento getProximoEvento(Flags flags, Dia dia){
        return this.getEventoTemprano(flags, dia);
    }

    private Evento getEventoTemprano(Flags flags, Dia dia)
    {
        List<EntradaTablaDeEventos> eventos = new List<EntradaTablaDeEventos>(this.poolEventosAleatoriosClase);

        eventos = this.getEventosDisponibles(eventos, flags);

        if (eventos.Count == 0)
        {
            return new EventoNormal(dia); // Fallback
        }

        return this.seleccionarEventoPonderado(eventos, dia);
    }

    private Evento seleccionarEventoPonderado(List<EntradaTablaDeEventos> eventosCandidatos, Dia dia)
    {
        int pesoTotal = eventosCandidatos.Sum(e => e.Peso);
        int aleatorio = new Random().Next(0, pesoTotal);
        int pesoAcumulado = 0;

        foreach(EntradaTablaDeEventos entrada in eventosCandidatos)
        {
            pesoAcumulado += entrada.Peso;
            if(aleatorio < pesoAcumulado)
            {
                // Instanciar el evento
                return (Evento)Activator.CreateInstance(entrada.TipoEvento, dia);
            }
        }

        // Fallback por si acaso (no debería ocurrir)
        return (Evento)Activator.CreateInstance(eventosCandidatos.Last().TipoEvento, dia);
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
