using Godot;
using System;

public partial class DiaEstandar : Dia
{
    public DiaEstandar(Sistema sistema) : base(sistema)
    {
        // No initial event loading here, Dia logic handles it via iniciarAvanzeFaseDelDia
        this.iniciarAvanzeFaseDelDia();
    }

    public TablaDeEventos GetTablaDeEventos()
    {
        return this.eventos;
    }

    public override Evento GetEventoClase()
    {
        Type tipoEvento = this.eventos.GetEventoTemprano(this.getFlags(), this.NumeroDia);
        if (tipoEvento != null)
        {
            return (Evento)Activator.CreateInstance(tipoEvento, this);
        }
        return null;
    }

    public override Evento GetEventoAlmuerzo()
    {
        Type tipoEvento = this.eventos.GetEventoAlmuerzo(this.getFlags(), this.NumeroDia);
        if (tipoEvento != null)
        {
            return (Evento)Activator.CreateInstance(tipoEvento, this);
        }
        return null;
    }

    public override Evento GetEventoEntrenamiento()
    {
        // Always return the base training event
        return new EventoEntrenamientoBase(this);
    }

    public override Evento GetEventoTarde()
    {
        Type tipoEvento = this.eventos.GetEventoTarde(this.getFlags(), this.NumeroDia);
        if (tipoEvento != null)
        {
            return (Evento)Activator.CreateInstance(tipoEvento, this);
        }
        return null;
    }

    public override Evento GetEventoNoche()
    {
        // Return the standard night choice event
        return new EventoNocheEstandar(this);
    }
}
