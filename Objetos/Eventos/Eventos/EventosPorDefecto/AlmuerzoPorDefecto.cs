using Godot;
using System;

public partial class AlmuerzoPorDefecto : Evento
{
    public AlmuerzoPorDefecto(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Comes tu almuerzo tranquilamente, sin que nadie te moleste.")
                .setFinal());
    }
}
