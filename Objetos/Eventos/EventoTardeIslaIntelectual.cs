using Godot;

public partial class EventoTardeIslaIntelectual : Evento
{
    public EventoTardeIslaIntelectual(Dia dia) : base(dia)
    {
        this.dialogos.Add(new Dialogo("Narrador", "Tarde - Pobre Intelectual"));
        this.cargarTexto();
    }
}
