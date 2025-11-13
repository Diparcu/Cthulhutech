using Godot;

public partial class TardeIslaIntelectual : Dia
{
    public TardeIslaIntelectual(Sistema sistema) : base(sistema)
    {
        this.setEventoCargado(new EventoTardeIslaIntelectual(this));
    }
}
