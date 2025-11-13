using Godot;
using System;
using System.Collections.Generic;

public abstract class EstadoDia
{
    public static readonly String PERIODO_MANANA = "Mañana";
    public static readonly String PERIODO_TARDE = "Tarde";
    public static readonly String PERIODO_NOCHE = "Noche";
    public static readonly String PERIODO_NA = "N/A";

    protected String periodoDelDia;
    protected Dia dia;

    public EstadoDia(Dia dia){
        this.dia = dia;
    }

    public virtual void avanzarDia(){}

    public virtual void comportamiento(double delta){}

    public virtual void control(InputEvent @event){}

    public virtual void dibujar(Node2D sistema){}
}
