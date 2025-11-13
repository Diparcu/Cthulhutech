using Godot;
using System.Collections.Generic;

public partial class EventoCreditos : Evento
{
    public EventoCreditos(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Programacion - Mercelo Murillo - Diego Paredes"));
        this.dialogos.Add(new Dialogo("Historia - Jean Germain - Mercelo Murillo - Diego Paredes"));
        this.dialogos.Add(new Dialogo("Diseño - Jean Germain - Mercelo Murillo - Diego Paredes"));
        this.dialogos.Add(new Dialogo("Gracias por jugar!")
            .addAction(() => {
                this.dia.getSistema().volverAlMenuPrincipal();
            }));

        this.cargarTexto();
    }
}
