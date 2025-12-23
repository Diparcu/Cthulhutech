using Godot;
using System;
using System.Collections.Generic;

public partial class EventoGenericoAlmuerzo : Evento
{
    public EventoGenericoAlmuerzo(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_UBB.jpg");
        Dialogo dialogo = new Dialogo("Jugador", "Hmmm, ¿donde deberia sentarme?");
        dialogo.addDesicion(new List<OpcionDialogo>(){
            new OpcionDialogo("Mesa autistas qlos.").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Con los autistas será.")
                    .addCambioDeEvento(typeof(EventoShinjiAlmuerzo1)),
                    }),
            new OpcionDialogo("Mesa con gente popular.").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Mira qlo, te dije como 7 veces que solo funciona la primera opcion, elige sentarte con los autistas."),
                    dialogo
                    }),
            new OpcionDialogo("Mesa con gente sospechosa.").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Mira qlo, te dije como 7 veces que solo funciona la primera opcion, elige sentarte con los autistas."),
                    dialogo
                    })
            });

            this.dialogos.Add(dialogo);
    }
}

