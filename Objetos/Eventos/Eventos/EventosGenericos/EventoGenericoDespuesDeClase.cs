using Godot;
using System;
using System.Collections.Generic;

public partial class EventoGenericoDespuesDeClase : Evento
{
    public EventoGenericoDespuesDeClase(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_UBB.jpg");
        Dialogo dialogo = new Dialogo("Jugador", "Hmmm, ¿que deberia hacer en la tarde?");
        dialogo.addDesicion(new List<OpcionDialogo>(){
            new OpcionDialogo("Irme pa la csa noma, que tanta wea.").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Sep, irme pa la casa noma, que tanta wea.")
                    .setFinal(typeof(EventoGenericoNoche)),
                    }),
            });

            this.dialogos.Add(dialogo);
    }
}
