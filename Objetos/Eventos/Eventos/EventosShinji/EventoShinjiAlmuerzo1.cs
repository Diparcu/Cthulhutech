using Godot;
using System;

public partial class EventoShinjiAlmuerzo1 : Evento
{
    public EventoShinjiAlmuerzo1(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Jugador", "Wena wn, Shinji, tanto tiempo."));
        this.dialogos.Add(new Dialogo("Shinji", "Me gusta que me peguen en las bolas.")
                .setFinal(typeof(EventoGenericoEntrenamiento)));
    }
}
