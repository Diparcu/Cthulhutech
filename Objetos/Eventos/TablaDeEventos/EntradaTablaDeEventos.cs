using Godot;
using System;
using System.Collections.Generic;

public partial class EntradaTablaDeEventos
{
    private Type evento;
	public Type Evento
	{
		get { return evento; }
	}
    private int peso = 10;
	public int Peso
	{
		get { return this.peso; }
	}
    private List<string> flagsRequeridas;
    private List<string> flagsProhibitivas;

    public List<string> FlagsRequeridas {get { return this.flagsRequeridas; } }
    public List<string> FlagsProhibitivas {get { return this.flagsProhibitivas; }}

    public EntradaTablaDeEventos(Type evento){
        this.evento = evento;
    }

    public EntradaTablaDeEventos(Type evento,
            List<string> flagsRequeridas,
            List<string> flagsProhibitivas){

        this.evento = evento;
        this.flagsRequeridas = flagsRequeridas;
        this.flagsProhibitivas = flagsProhibitivas;
    }

    public EntradaTablaDeEventos(Type evento,
            List<string> flagsRequeridas,
            List<string> flagsProhibitivas,
            int peso){

        this.evento = evento;
        this.flagsRequeridas = flagsRequeridas;
        this.flagsProhibitivas = flagsProhibitivas;
        this.peso = peso;
    }

    public EntradaTablaDeEventos setPeso(int peso){
        this.peso = peso;
        return this;
    }
}
