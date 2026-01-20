using Godot;
using System;

public partial class EventoClaseDia1 : Evento
{
    public EventoClaseDia1(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo( "Wena los k, esta es la primera clase, mi nombre es profe Sergio."));
        this.dialogos.Add(new Dialogo( "Pero ustedes pueden decirme *papi*.")
                .addCambioDeEvento(TablaDeEventos.INTERACCION_CLASE));
    }
}
