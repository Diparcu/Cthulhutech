using Godot;
using System;
using System.Collections.Generic;

public partial class Dia0Blanco : Dia
{
    public Dia0Blanco(Sistema sistema) : base(sistema)
    {
        this.eventoCargado = new EventoDia0Blanco(this);
        this.AddChild(this.eventoCargado);
    }
}

public partial class EventoDia0Blanco : Evento
{
    public EventoDia0Blanco(Dia dia) : base(dia)
    {
        // Configuración inicial de la escena
        TextureRect fondo = new TextureRect();
        fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/Departamentos_Interior_Zona_Salina.jpg");
        fondo.SetSize(new Vector2(1280, 640));
        fondo.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        this.AddChild(fondo);
        fondo.ZIndex = -1;

        // Comienza la narrativa
        this.dialogos.Add(new Dialogo("Narrador", "Sábado, Día 0. Temprano."));
        this.dialogos.Add(new Dialogo("Narrador", "Te despiertas en tu pequeño cuarto. Las paredes son delgadas, siempre lo han sido. Vives en el ghetto nazzadi, un laberinto de edificios apretados y gente que nunca duerme. Eres el único niño blanco que conocen."));
        this.dialogos.Add(new Dialogo("Narrador", "Hoy no viene el tutor. Tienes el día para ti, lo que significa que tienes el día para las 'voces'."));
        this.dialogos.Add(new Dialogo("Narrador", "No son voces de verdad. Son más como... susurros en tu cabeza. Murmullos que se cuelan por las paredes. A veces, crees que vienen del armario."));
        this.dialogos.Add(new Dialogo("Narrador", "[color=darkgreen]'...no me alcanza el dinero...'[/color] [color=darkgreen]'...ojalá se callara el perro del vecino...'[/color] [color=darkgreen]'...otra vez sopa de fideos...'[/color]"));
        this.dialogos.Add(new Dialogo("Narrador", "Fragmentos de pensamientos ajenos. No sabes de quién son, ni por qué los oyes. Tus padres te han dicho que tienes mucha imaginación. Has aprendido a no hablar de ello."));
        this.dialogos.Add(new Dialogo("Jugador", "(Mejor pongo la tele. El ruido ayuda a ahogar el otro ruido.)"));

        this.dialogos.Add(new Dialogo("Narrador", "Te sientas frente al viejo televisor. En la pantalla, unos dibujos animados de colores chillones se pelean con sartenes y yunques.").addAction(() => {
            // Aquí se podría añadir un sonido de TV si existiera.
        }));

        this.dialogos.Add(new Dialogo("Narrador", "El presentador de las noticias de la mañana habla sobre las tensiones en la frontera. [color=darkgreen]'...este traje me aprieta...'[/color] escuchas, mezclado con el reporte del clima. [color=darkgreen]'...tengo que acordarme de comprar leche...'[/color]"));
        this.dialogos.Add(new Dialogo("Narrador", "Es inútil. Las voces se cuelan por todas partes. Son como una radio mal sintonizada que no puedes apagar. No puedes controlarlas. Ni siquiera sabes qué son."));
        this.dialogos.Add(new Dialogo("Jugador", "(Tal vez si salgo... afuera hay más ruido. A veces el ruido de la calle ayuda de verdad.)"));

        this.dialogos.Add(new Dialogo("Narrador", "Te pones los zapatos y caminas hacia la puerta. El aire del departamento se siente pesado, cargado con los pensamientos y las vidas de todos los que te rodean.").addAction(() => {
            this.getSistema().cambiarFondo("res://Sprites/Fondos/Departamentos_Zona_Salina.jpg");
        }));

        this.dialogos.Add(new Dialogo("Narrador", "Abres la puerta y sales al pasillo. El olor a comida frita y a humedad te golpea. El murmullo de tus vecinos es ahora un coro constante. Un nuevo día en el ghetto."));
        // Aquí se podría conectar con la siguiente escena del día.
        // Por ahora, la escena termina aquí.

        this.cargarTexto();
    }
}
