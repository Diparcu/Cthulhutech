using Godot;
using System;

public partial class EntrenamientoPorDefecto : Evento
{
    public EntrenamientoPorDefecto(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Haces tu entrenamiento normalmente.")
                .setFinal());
    }
}
