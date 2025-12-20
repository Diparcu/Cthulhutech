using Godot;
using System;
using System.Collections.Generic;

public partial class EventoNocheEstandar : Evento
{
    public EventoNocheEstandar(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_NOCHE.jpg"); // Placeholder
        this.dialogos.Add(new Dialogo("Narrador", "La noche ha caído. El cansancio pesa, pero la ciudad sigue despierta."));

        Dialogo decision = new Dialogo("Narrador", "¿Qué harás?");
        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        // Option 1: Sleep (Restore Mana & End Day)
        OpcionDialogo opDormir = new OpcionDialogo("Ir a dormir (Restaurar Maná)");
        opDormir.setSiguienteDialogo(new List<Dialogo> {
            new Dialogo("Narrador", "Te metes en la cama y cierras los ojos. El sueño llega rápido.").addAction(() => {
                // Mana restoration is handled by Dia.avanzarDia() which is triggered by TerminarEvento logic loop
                // But explicitly, we just end the event here.
            })
        });
        opciones.Add(opDormir);

        // Option 2: Night Adventure
        OpcionDialogo opAventura = new OpcionDialogo("Salir a buscar aventuras");
        opAventura.setSiguienteDialogo(new List<Dialogo> {
            new Dialogo("Narrador", "Decides que la noche es joven. Sales a las calles.").addAction(() => {
                this.IniciarAventuraNoche();
            })
        });
        opciones.Add(opAventura);

        decision.setDesicion(opciones);
        this.dialogos.Add(decision);

        this.cargarTexto();
    }

    private void IniciarAventuraNoche()
    {
        if (this.dia is DiaEstandar diaEstandar)
        {
            Type tipoEvento = diaEstandar.GetTablaDeEventos().GetEventoNoche(this.dia.getFlags(), this.dia.NumeroDia);

            if (tipoEvento != null)
            {
                Evento eventoAleatorio = (Evento)Activator.CreateInstance(tipoEvento, this.dia);
                this.reemplazarDialogo(eventoAleatorio.GetDialogos());
            }
            else
            {
                 this.reemplazarDialogo(new List<Dialogo>{ new Dialogo("Narrador", "No encuentras nada interesante esta noche.")});
            }
        }
    }
}
