using Godot;
using System;
using System.Collections.Generic;

public partial class EventoGenericoEntrenamiento : Evento
{
    public EventoGenericoEntrenamiento(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_UBB.jpg");
        Dialogo dialogo = new Dialogo("Jugador", "Hmmm, ¿que deberia entrenar?");
        dialogo.addDesicion(new List<OpcionDialogo>(){
            new OpcionDialogo("Entrenar armas.").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("A entrenar armas será.")
                    .addCambioDeEvento(typeof(EventoShinjiEntrenamientoArmas1)),
                    }),
            new OpcionDialogo("Entrenamiento fisico.").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Mira qlo, te dije como 7 veces que solo funciona la primera opcion, elige entrenar armas."),
                    dialogo
                    }),
            });

            this.dialogos.Add(dialogo);
    }
}
