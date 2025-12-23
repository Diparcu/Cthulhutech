using Godot;
using System;
using System.Collections.Generic;

public partial class EventoGenericoNoche : Evento
{
    public EventoGenericoNoche(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_UBB.jpg");

        this.dialogos.Add(new Dialogo("Jugador","'Vaya, que bueno es estar debuela en casa.'"));
        this.dialogos.Add(new Dialogo("'¿Que vas a hacer con el resto de la noche?'")
            .addDesicion(new List<OpcionDialogo>(){
                new OpcionDialogo("Dormir como un desgraciado.").setSiguienteDialogo(new List<Dialogo>(){
                        new Dialogo("Ahí te voy San Pedro.")
                        .setFinal(typeof(EventoGenericoClase)),
                        }),
                }));
    }
}
