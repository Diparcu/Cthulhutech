using Godot;
using System;
using System.Collections.Generic;

public partial class EntradaTablaDeEventos
{
    private Type evento;
    private int peso = 10;
    private List<string> flagsRequeridas;
    private List<string> flagsProhibitivas;

    public List<string> FlagsRequeridas {get { return this.flagsRequeridas; } }
    public List<string> FlagsProhibitivas {get { return this.flagsProhibitivas; }}

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
}
