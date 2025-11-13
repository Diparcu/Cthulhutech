using Godot;
using System;
using System.Collections.Generic;

public partial class EventoKC : Evento 
{
	public EventoKC(Dia dia): base(dia){

		Sprite2D fondo = new Sprite2D();
		Sprite2D kc = new Sprite2D();
		
		fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_ORDENADOR.png");
		kc.Texture = (Texture2D)GD.Load("res://Sprites/Personajes/KC.png");

		this.AddChild(fondo);
		this.AddChild(kc);

		kc.Visible = false;
		kc.ZIndex = -1;
		kc.Position = new Vector2(200, 500);
		kc.Scale = new Vector2(0.7f, 0.7f);

		fondo.ZIndex = -1;
		fondo.Position = new Vector2(1280/2, 640/2); 
		fondo.Scale = new Vector2(1, 1);
		 
		// Dialog lists
		List<Dialogo> exitoInformatica = new List<Dialogo>();
		List<Dialogo> falloInformatica = new List<Dialogo>();
		List<Dialogo> exitoPercepcion = new List<Dialogo>();
		List<Dialogo> falloPercepcion = new List<Dialogo>();
		List<Dialogo> facebook = new List<Dialogo>();
		List<Dialogo> encuentro = new List<Dialogo>();

		// Perception success
		exitoPercepcion.Add(new Dialogo("¡Es un compañero de curso! Nunca has hablado con él, pero lo reconoces."));
		exitoPercepcion.Add(new Dialogo("KC: '¿Trajiste el dinero?'"));
		exitoPercepcion.Add(new Dialogo("Jugador: '¿Trajiste los chocolates?'"));
		exitoPercepcion.Add(new Dialogo("KC te muestra una pistola."));
		exitoPercepcion.Add(new Dialogo("KC: 'Aquí tienes. Ahora, el dinero.'").setFinal());

		// Perception failure
		falloPercepcion.Add(new Dialogo("No estás seguro de por qué, pero su cara te suena."));
		falloPercepcion.Add(new Dialogo("KC: '¿Trajiste el dinero?'"));
		falloPercepcion.Add(new Dialogo("Jugador: '¿Trajiste los chocolates?'"));
		falloPercepcion.Add(new Dialogo("KC te muestra una pistola."));
		falloPercepcion.Add(new Dialogo("KC: 'Aquí tienes. Ahora, el dinero.'").setFinal());

		// Meeting part
		encuentro.Add(new Dialogo("Llegas al lugar acordado. El callejón es oscuro y sucio."));
		encuentro.Add(new Dialogo("Una figura te espera en la oscuridad. Te resulta extrañamente familiar."));
		Dialogo checkeoPercepcion = new Dialogo("Tiras los dados para ver si lo reconoces.");
		OpcionDialogo opcionPercepcion = new OpcionDialogo("Intentar reconocerlo", 10, "Percepcion");
		opcionPercepcion.setSiguienteDialogo(exitoPercepcion);
		opcionPercepcion.setSiguienteDialogoFallo(falloPercepcion);
		checkeoPercepcion.setDesicion(new List<OpcionDialogo>{opcionPercepcion});
		encuentro.Add(checkeoPercepcion);

		// Facebook part
		facebook.Add(new Dialogo("Vas a Facebook y buscas grupos de compra y venta."));
		facebook.Add(new Dialogo("Publicas: 'Busco chocolates'"));
		facebook.Add(new Dialogo("Al poco tiempo, un tal 'KC' te contacta."));
		facebook.Add(new Dialogo("KC: 'Tengo los chocolates que buscas. Nos vemos en el callejón de la calle Lincoyán a las 10 PM.'"));
		facebook.Add(new Dialogo("Jugador: 'Ahí estaré.'")
		    .addAction(() => {
		        fondo.Texture = (Texture2D)GD.Load("res://Sprites/Fondos/FONDO_CALLEJON.jpg");
		        kc.Visible = true;
		    }));
		facebook.AddRange(encuentro);

		// Informatica success
		exitoInformatica.Add(new Dialogo("Descubres que hay una palabra clave 'chocolates', para preguntar por armas en paginas más públicas."));
		exitoInformatica.AddRange(facebook);

		// Informatica failure
		falloInformatica.Add(new Dialogo("No encuentras nada en los foros profundos. Decides ir a lo fácil."));
		falloInformatica.Add(new Dialogo("Vas a Facebook y buscas grupos de compra y venta."));
		falloInformatica.Add(new Dialogo("Publicas: 'Busco armas'"));
		falloInformatica.Add(new Dialogo("A los pocos minutos, sientes sirenas de policía acercándose."));
		falloInformatica.Add(new Dialogo("La policía irrumpe en tu casa y te arresta."));
		falloInformatica.Add(new Dialogo("GAME OVER")
		    .addAction(() => {
		        this.dia.getSistema().GameOver();
		    }));

		// Informatica check
		Dialogo checkeoInformatica = new Dialogo("Tiras los dados para ver si encuentras algo.");
		OpcionDialogo opcionInformatica = new OpcionDialogo("Buscar informacion", 5, "Investigacion");
		opcionInformatica.setSiguienteDialogo(exitoInformatica);
		opcionInformatica.setSiguienteDialogoFallo(falloInformatica);
		checkeoInformatica.setDesicion(new List<OpcionDialogo>{opcionInformatica});

		// Initial dialogs
		this.dialogos.Add(new Dialogo("Debido a las circunstancias decidiste buscar en algunos foros profundos acerca de como conseguir un arma"));
		this.dialogos.Add(checkeoInformatica);

		this.cargarTexto();
	}
}
