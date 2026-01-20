using Godot;
using System;

public partial class NochePorDefecto : Evento
{
    public NochePorDefecto(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("La noche pasa sin mayor altercado.")
                .setFinal());
    }
}
