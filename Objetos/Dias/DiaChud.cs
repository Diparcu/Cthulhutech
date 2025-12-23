using Godot;
using System;
using System.Collections.Generic;

public partial class DiaChud : Dia
{
    public DiaChud(Sistema sistema) : base(sistema)
    {
        this.eventoCargado = new EventoDia0Chud(this);
        this.faseDelDiaActual = 3;
        this.AddChild(this.eventoCargado);
    }
}

public partial class EventoDia0Chud : Evento
{
    public EventoDia0Chud(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_ORDENADOR.png");
        this.dialogos.Add(new Dialogo("Te despiertas, como ya es de costumbre, alrededor de las 4 de la tarde."));
        this.dialogos.Add(new Dialogo("Tus padres te llevan weiando las ultimas tres semanas para que regreses al colegio y dejí de ser una plasta qla."));
        this.dialogos.Add(new Dialogo("Jugador", "Tal vez tengan razon, por otro lado..."));
        this.dialogos.Add(new Dialogo("Bla bla bla, te quedai weiando en el pc o algo así.")
                .setFinal(typeof(EventoDia1Chud)));
    }
}

public partial class EventoDia1Chud : Evento
{
    public EventoDia1Chud(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_ORDENADOR.png");
        this.dialogos.Add(new Dialogo("Jugador", "Wiii, me gusta mucho weiar en el pc molestando a gente random en los foros de la interweb."));
        this.dialogos.Add(new Dialogo("Jugador", "Me llegó un mensaje de un wn random diciendome que me van a sacar la shusha."));
        this.dialogos.Add(new Dialogo("Jugador", "Ja, no sae na que yo ya se."));
        this.dialogos.Add(new Dialogo("Entonces escuchas un ruido afuera de tu casa."));
        this.dialogos.Add(new Dialogo("Vai a ver y es un hombre malvao en un traje malvado."));
        this.dialogos.Add(new Dialogo("Te asusta mucho y decides hacerle caso a tus padres y meterte en el colegio militar.")
                .setFinal(typeof(EventoClase1Chud)));
    }
}

public partial class EventoClase1Chud : Evento
{
    public EventoClase1Chud(Dia dia) : base(dia)
    {
        this.cambiarFondo("res://Sprites/Fondos/FONDO_UBB.jpg");
        this.dialogos.Add(new Dialogo("Jugador", "Vaya, finalmente en mi primer día de clases. Como buen Chud que soy, lo más dificil fue despertarme antes de las 2 de la tarde, pero ya estoy aquí.")
                .addCambioDeEvento(typeof(EventoGenericoClase)));
    }
}
