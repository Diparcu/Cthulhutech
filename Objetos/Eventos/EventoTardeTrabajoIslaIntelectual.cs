using Godot;

public partial class EventoTardeTrabajoIslaIntelectual : Evento
{
    public EventoTardeTrabajoIslaIntelectual(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Narrador", "Tarde - Trabajo Pobre Intelectual"));
        this.cargarTexto();
    }
}
