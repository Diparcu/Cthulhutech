using Godot;
using System.Collections.Generic;

public partial class EventoEntrenamientoBase : Evento
{
    public EventoEntrenamientoBase(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_ENTRENAMIENTO.jpg"); // Placeholder background
        this.dialogos.Add(new Dialogo("Narrador", "Es hora de entrenar. ¿En qué quieres enfocarte hoy?"));

        Dialogo decision = new Dialogo("Narrador", "Elige tu entrenamiento:");
        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        OpcionDialogo opArmas = new OpcionDialogo("Entrenamiento de Armas (+XP Combate)");
        opArmas.setSiguienteDialogo(new List<Dialogo> {
            new Dialogo("Narrador", "Dedicas la sesión a pulir tu técnica con las armas. Te sientes más letal.").addAction(() => {
                // Placeholder for stat gain logic
                this.getJugador().XP += 10;
            })
        });
        opciones.Add(opArmas);

        OpcionDialogo opFisico = new OpcionDialogo("Entrenamiento Físico (+XP Físico)");
        opFisico.setSiguienteDialogo(new List<Dialogo> {
            new Dialogo("Narrador", "Sudas la gota gorda levantando pesas y corriendo. Te sientes más fuerte.").addAction(() => {
                 // Placeholder for stat gain logic
                 this.getJugador().XP += 10;
            })
        });
        opciones.Add(opFisico);

        decision.setDesicion(opciones);
        this.dialogos.Add(decision);

        this.cargarTexto();
    }
}
