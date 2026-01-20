using Godot;
using System;

public partial class ClasePorDefecto : Evento
{
    public ClasePorDefecto(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Profe Sergio",
                    "Miren niños, ando muy encañao, porfavor miren a la pared hasta que la clase termine.")
                .addCambioDeEvento(TablaDeEventos.INTERACCION_CLASE));
    }
}
