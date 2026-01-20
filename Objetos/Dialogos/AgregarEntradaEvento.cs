using Godot;
using System;

public partial class AgregarEntradaEvento 
{
    private string pool;
    private EntradaTablaDeEventos entrada;

    public string Pool{ get { return this.pool; } }
    public EntradaTablaDeEventos Entrada{ get { return this.entrada; } }

    public AgregarEntradaEvento(string pool,
            EntradaTablaDeEventos entrada) 
    {
        this.pool = pool;
        this.entrada = entrada;
    }
}
