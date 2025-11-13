using Godot;

public partial class TardeTrabajoIslaIntelectual : Dia
{
    public TardeTrabajoIslaIntelectual(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoTardeTrabajoIslaIntelectual(this));
    }
}
