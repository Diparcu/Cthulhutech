using Godot;
using System;

public partial class InteraccionClasePorDefecto : Evento
{
    public InteraccionClasePorDefecto(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo( "El resto de la clase pasó sin que siquiera me diera cuenta.")
                .setFinal(typeof(EventoGenericoAlmuerzo)));
    }
}
