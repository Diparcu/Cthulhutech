using Godot;
using System;
using System.Collections.Generic;

public partial class EventoGenericoClase : Evento
{
    public EventoGenericoClase(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_UBB.jpg");
        Dialogo dialogo = new Dialogo("Jugador", "Hmmm, ¿donde deberia sentarme?");
        dialogo.addDesicion(new List<OpcionDialogo>(){
            new OpcionDialogo("Atras").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Atras será.")
                    .addCambioDeEvento(typeof(EventoShinjiClase1)),
                    }),
            new OpcionDialogo("Al medio").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Mira qlo, te dije como 7 veces que solo funciona la primera opcion, elige sentarte atras."),
                    dialogo
                    }),
            new OpcionDialogo("Al frente").setSiguienteDialogo(new List<Dialogo>(){
                    new Dialogo("Mira qlo, te dije como 7 veces que solo funciona la primera opcion, elige sentarte atras."),
                    dialogo
                    })
            });

            this.dialogos.Add(dialogo);
    }
}
