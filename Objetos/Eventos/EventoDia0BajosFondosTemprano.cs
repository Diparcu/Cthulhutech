using Godot;
using System;
using System.Collections.Generic;

public partial class EventoDia0BajosFondosTemprano : Evento
{
    public EventoDia0BajosFondosTemprano(Dia dia) : base(dia)
    {
        this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_CASA_SALINA.jpg");

        this.dialogos.Add(new Dialogo("Narrador", "Día 0. Zona Salina. Temprano."));
        this.dialogos.Add(new Dialogo("Narrador", "Te despiertas en tu casa, una estructura improvisada en medio de la 'zona salina', una maraña de campos de cultivo mezclados con metal oxidado y restos de la antigua civilización."));
        this.dialogos.Add(new Dialogo("Narrador", "Aquí, la gente usó lo que encontró para hacerse viviendas. Hubo mucha gente alguna vez, pero ahora solo quedan pocas casas habitadas entre montones de chatarra y edificios abandonados."));
        this.dialogos.Add(new Dialogo("Narrador", "Tu padre es un arquitecto, pero nunca está en casa. Ni con su profesión alcanza para vivir bien en estos días. De tu madre... ni noticias. Solo son tú y él contra el mundo."));

        Dialogo decisionInicial = new Dialogo("Narrador", "Tienes la mañana libre. ¿Qué quieres hacer?");

        List<OpcionDialogo> opciones = new List<OpcionDialogo>();

        OpcionDialogo opcionTV = new OpcionDialogo("Ver TV y pensar en cosas de tu tiempo");
        OpcionDialogo opcionCaminar = new OpcionDialogo("Salir a caminar y explorar el sitio baldío");

        opciones.Add(opcionTV);
        opciones.Add(opcionCaminar);

        decisionInicial.setDesicion(opciones);

        // --- OPCIÓN A: VER TV ---
        List<Dialogo> dialogoTV = new List<Dialogo>();
        dialogoTV.Add(new Dialogo("Narrador", "Enciendes la vieja televisión. La señal va y viene, pero logras sintonizar un programa de mecánica."));
        dialogoTV.Add(new Dialogo("TV", "¡Bienvenidos de nuevo! Hoy vamos a ver cómo modificar el tren delantero de este clásico Toyota Corolla para que aguante los terrenos más difíciles..."));
        dialogoTV.Add(new Dialogo("Narrador", "Te quedas hipnotizado viendo cómo las manos expertas trabajan el metal, soldando y ajustando piezas. Es un recordatorio de que, con ingenio, cualquier cosa puede repararse o mejorarse."));

        dialogoTV.Add(new Dialogo("Narrador", "Miras a tu alrededor. Tu casa no es muy distinta a ese auto en reparación. Paredes de lámina reforzadas con madera vieja, muebles rescatados de la basura y reparados por tu padre. Es un collage de supervivencia."));

        dialogoTV.Add(new Dialogo("Narrador", "Vuelves la vista a la pantalla. El programa ha terminado y ahora pasan comerciales."));
        dialogoTV.Add(new Dialogo("TV", "[Música inspiradora y patriótica de fondo]"));
        dialogoTV.Add(new Dialogo("TV", "¡La Arcología es el futuro! Un lugar donde la integración nazzadi es una realidad, trabajando hombro con hombro por un mañana mejor."));
        dialogoTV.Add(new Dialogo("TV", "El Gobierno terrestre asegura un estatus de vida digno para todos sus ciudadanos. Un techo seguro y 'ter' garantizado para una dieta saludable. ¡Únete a nosotros!"));

        // Tirada Oculta: Historia / Inteligencia para analizar la propaganda
        ResultadoTirada tiradaHistoria = realizarTiradaCombinacion("Historia", "Inteligencia", 12);
        string textoTiradaHistoria = $"[color=darkgreen][Tirada Oculta - Historia diff 12: {string.Join("-", tiradaHistoria.DadosLanzados)} | {(tiradaHistoria.Exito ? "Éxito" : "Fallo")}][/color]";
        dialogoTV.Add(new Dialogo("Narrador", textoTiradaHistoria));

        if (tiradaHistoria.Exito)
        {
            dialogoTV.Add(new Dialogo("Narrador", "Frunces el ceño. Sabes que no todo es tan bonito como lo pintan. Has escuchado historias de gente que se fue a la arcología y nunca más se supo de ellos, o de nazzadis que viven en guetos segregados. La propaganda brilla, pero la realidad suele ser más opaca."));
        }
        else
        {
            dialogoTV.Add(new Dialogo("Narrador", "Suena maravilloso. Un lugar limpio, seguro y con comida garantizada. Definitivamente suena mejor que vivir entre chatarra y humedad."));
        }

        dialogoTV.Add(new Dialogo("Narrador", "Apagas la TV. El silencio vuelve a la casa, solo roto por el viento que silba entre las rendijas de metal.").addAction(() => {
             this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
        }));

        opcionTV.setSiguienteDialogo(dialogoTV);


        // --- OPCIÓN B: SALIR A CAMINAR ---
        List<Dialogo> dialogoCaminar = new List<Dialogo>();
        dialogoCaminar.Add(new Dialogo("Narrador", "Decides salir. El aire afuera es húmedo y salobre.").addAction(() => {
            this.getSistema().cambiarFondo("res://Sprites/Fondos/FONDO_POBRE.jpg");
        }));

        dialogoCaminar.Add(new Dialogo("Narrador", "Caminas por los alrededores. La zona está llena de humedales, interrumpidos por esqueletos de casas abandonadas que alguna vez formaron el centro de un pueblo vibrante."));

        Dialogo decisionExploracion = new Dialogo("Narrador", "Ves varias opciones frente a ti. ¿Hacia dónde te diriges?");
        List<OpcionDialogo> opcionesExploracion = new List<OpcionDialogo>();

        OpcionDialogo opCasa1 = new OpcionDialogo("Explorar casa abandonada 1 (Cultistas)");
        OpcionDialogo opCasa2 = new OpcionDialogo("Explorar casa abandonada 2 (Vagabundo)");
        OpcionDialogo opCasa3 = new OpcionDialogo("Explorar casa abandonada 3 (Saqueada)");
        OpcionDialogo opParque = new OpcionDialogo("Sentarse en el parque");

        opcionesExploracion.Add(opCasa1);
        opcionesExploracion.Add(opCasa2);
        opcionesExploracion.Add(opCasa3);
        opcionesExploracion.Add(opParque);

        decisionExploracion.setDesicion(opcionesExploracion);
        dialogoCaminar.Add(decisionExploracion);

        // --- SUB-OPCIÓN: CASA 1 (CULTISTAS) ---
        List<Dialogo> dialogoCasa1 = new List<Dialogo>();
        dialogoCasa1.Add(new Dialogo("Narrador", "Te acercas a una casa que parece extrañamente conservada en comparación con las demás. La puerta está entreabierta."));

        // Tirada Oculta: Observar / Percepción (Sensación de peligro)
        ResultadoTirada tiradaPeligro = realizarTiradaCombinacion("Observar", "Percepcion", 11);
        string textoTiradaPeligro = $"[color=darkgreen][Tirada Oculta - Observar diff 11: {string.Join("-", tiradaPeligro.DadosLanzados)} | {(tiradaPeligro.Exito ? "Éxito" : "Fallo")}][/color]";
        dialogoCasa1.Add(new Dialogo("Narrador", textoTiradaPeligro));

        if (tiradaPeligro.Exito)
        {
            dialogoCasa1.Add(new Dialogo("Narrador", "Sientes una opresión en el pecho al cruzar el umbral. El aire aquí dentro se siente pesado, cargado de una energía estática que te eriza los vellos de la nuca. Algo no está bien."));

            // Segunda Tirada: Observar / Percepción (Encontrar velas)
            ResultadoTirada tiradaVelas = realizarTiradaCombinacion("Observar", "Percepcion", 12);
            string textoTiradaVelas = $"[color=darkgreen][Tirada Oculta - Observar diff 12: {string.Join("-", tiradaVelas.DadosLanzados)} | {(tiradaVelas.Exito ? "Éxito" : "Fallo")}][/color]";
            dialogoCasa1.Add(new Dialogo("Narrador", textoTiradaVelas));

            if (tiradaVelas.Exito)
            {
                 dialogoCasa1.Add(new Dialogo("Narrador", "En un rincón oscuro, encuentras un altar improvisado. Hay unas velas negras de aspecto ceroso y repulsivo, rodeadas de símbolos trazados con tiza roja."));

                 Dialogo decisionVelas = new Dialogo("Narrador", "¿Qué haces con las velas?");
                 List<OpcionDialogo> opcionesVelas = new List<OpcionDialogo>();

                 OpcionDialogo opTomarVelas = new OpcionDialogo("Tomar las velas");
                 OpcionDialogo opDejarVelas = new OpcionDialogo("Dejarlas y salir");

                 opcionesVelas.Add(opTomarVelas);
                 opcionesVelas.Add(opDejarVelas);
                 decisionVelas.setDesicion(opcionesVelas);

                 // Tomar velas
                 List<Dialogo> dialogoTomarVelas = new List<Dialogo>();
                 dialogoTomarVelas.Add(new Dialogo("Narrador", "Sientes un hormigueo desagradable al tocarlas, pero las guardas en tu bolsillo.").addAction(() => {
                     this.getJugador().AgregarItem("Velas malignas");
                 }));
                 dialogoTomarVelas.Add(new Dialogo("Narrador", "Sales rápidamente de la casa, con la sensación de que ojos invisibles te observan.").addAction(() => {
                     this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
                 }));
                 opTomarVelas.setSiguienteDialogo(dialogoTomarVelas);

                 // Dejar velas
                 List<Dialogo> dialogoDejarVelas = new List<Dialogo>();
                 dialogoDejarVelas.Add(new Dialogo("Narrador", "Decides que es mejor no meterse con lo que no entiendes. Das media vuelta y sales.").addAction(() => {
                     this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
                 }));
                 opDejarVelas.setSiguienteDialogo(dialogoDejarVelas);

                 dialogoCasa1.Add(decisionVelas);
            }
            else
            {
                dialogoCasa1.Add(new Dialogo("Narrador", "Miras alrededor pero la oscuridad te impide distinguir detalles. La sensación de peligro es demasiada, así que decides marcharte.").addAction(() => {
                    this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
                }));
            }
        }
        else
        {
            dialogoCasa1.Add(new Dialogo("Narrador", "Entras y das una vuelta rápida. Parece solo otra casa abandonada más, aunque un poco más limpia. No encuentras nada de valor.").addAction(() => {
                this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
            }));
        }
        opCasa1.setSiguienteDialogo(dialogoCasa1);

        // --- SUB-OPCIÓN: CASA 2 (VAGABUNDO) ---
        List<Dialogo> dialogoCasa2 = new List<Dialogo>();
        dialogoCasa2.Add(new Dialogo("Narrador", "Entras en una casa que ha perdido parte del techo. La luz del sol ilumina un rincón donde parece haber vivido alguien recientemente."));
        dialogoCasa2.Add(new Dialogo("Narrador", "Entre unos trapos viejos encuentras una pequeña fortuna para alguien de tu edad: una lata de dulces aún sellada y un cuchillo oxidado pero funcional."));
        dialogoCasa2.Add(new Dialogo("Narrador", "Decides quedártelos. Nunca se sabe cuándo podrían ser útiles.").addAction(() => {
            this.getJugador().AgregarItem("Lata con dulces");
            this.getJugador().AgregarItem("Cuchillo");
        }));
        dialogoCasa2.Add(new Dialogo("Narrador", "Satisfecho con tu hallazgo, regresas a casa.").addAction(() => {
            this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
        }));
        opCasa2.setSiguienteDialogo(dialogoCasa2);

        // --- SUB-OPCIÓN: CASA 3 (SAQUEADA) ---
        List<Dialogo> dialogoCasa3 = new List<Dialogo>();
        dialogoCasa3.Add(new Dialogo("Narrador", "Te diriges a una estructura que apenas se mantiene en pie. Al entrar, te das cuenta de que fue una pérdida de tiempo."));
        dialogoCasa3.Add(new Dialogo("Narrador", "Está completamente vacía. Se han llevado todo, incluso las tuberías y hasta la puerta trasera. Solo hay polvo y escombros."));
        dialogoCasa3.Add(new Dialogo("Narrador", "Decepcionado, decides volver antes de que se haga más tarde.").addAction(() => {
            this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
        }));
        opCasa3.setSiguienteDialogo(dialogoCasa3);


        // --- SUB-OPCIÓN: PARQUE (PANDILLEROS) ---
        List<Dialogo> dialogoParque = new List<Dialogo>();
        dialogoParque.Add(new Dialogo("Narrador", "Caminas hacia lo que alguna vez fue un parque. Ahora es un terreno baldío con columpios oxidados. Te sientas en uno, disfrutando del chirrido metálico."));

        bool esAtacado = false;

        // Tirada Oculta: Observar / Percepción (Notar pandilleros)
        ResultadoTirada tiradaPandilleros = realizarTiradaCombinacion("Observar", "Percepcion", 11);
        string textoTiradaPandilleros = $"[color=darkgreen][Tirada Oculta - Observar diff 11: {string.Join("-", tiradaPandilleros.DadosLanzados)} | {(tiradaPandilleros.Exito ? "Éxito" : "Fallo")}][/color]";
        dialogoParque.Add(new Dialogo("Narrador", textoTiradaPandilleros));

        if (tiradaPandilleros.Exito)
        {
            dialogoParque.Add(new Dialogo("Narrador", "Por el rabillo del ojo, notas movimiento. Un grupo de figuras se aproxima desde el otro lado del parque. No parecen amistosos."));

            // Tirada Oculta: SaviorFaire (Notar peligro) - Usando Inteligencia o Percepción como stat base si no tiene uno específico, pero SaviorFaire suele ir con Presencia o Inteligencia.
            // Asumiendo Presencia como base lógica para SaviorFaire si no se especifica otra.
            ResultadoTirada tiradaPeligroSocial = realizarTiradaCombinacion("SaviorFaire", "Presencia", 12);
            string textoTiradaPeligroSocial = $"[color=darkgreen][Tirada Oculta - SaviorFaire diff 12: {string.Join("-", tiradaPeligroSocial.DadosLanzados)} | {(tiradaPeligroSocial.Exito ? "Éxito" : "Fallo")}][/color]";
            dialogoParque.Add(new Dialogo("Narrador", textoTiradaPeligroSocial));

            if (tiradaPeligroSocial.Exito)
            {
                dialogoParque.Add(new Dialogo("Narrador", "Tu instinto callejero se dispara. Su lenguaje corporal, la forma en que se dispersan... te están rodeando. Son peligrosos."));
                dialogoParque.Add(new Dialogo("Narrador", "Sin hacer movimientos bruscos, te levantas y te alejas rápidamente antes de que cierren el cerco. Logras perderlos entre los callejones.").addAction(() => {
                    this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
                }));
            }
            else
            {
                // Fallo SaviorFaire: Atacado
                dialogoParque.Add(new Dialogo("Narrador", "Piensas que solo son otros chicos pasando el rato. Te quedas sentado un momento demasiado largo."));
                dialogoParque.Add(new Dialogo("Narrador", "Cuando te das cuenta, ya están encima de ti. Te golpean y te registran los bolsillos."));
                esAtacado = true;
            }
        }
        else
        {
            // Fallo Observar: Atacado sorpresa
             dialogoParque.Add(new Dialogo("Narrador", "Estás distraído mirando el óxido en las cadenas. No ves venir el golpe hasta que es demasiado tarde."));
             dialogoParque.Add(new Dialogo("Narrador", "Todo se vuelve negro por un segundo. Sientes manos hurgando en tu ropa."));
             esAtacado = true;
        }

        // Lógica común de ataque para fallos (Observar O SaviorFaire)
        if (esAtacado)
        {
             dialogoParque.Add(new Dialogo("Narrador", "Te dejan tirado en el suelo, dolorido y humillado.").addAction(() => {
                 if (this.getJugador().Inventario.Count > 0)
                 {
                     this.getJugador().VaciarInventario();
                     // Sobrevive pero pierde items
                     this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
                 }
                 else
                 {
                     // Sin items: Game Over
                     this.getSistema().GameOver();
                 }
             }));
        }

        opParque.setSiguienteDialogo(dialogoParque);

        opcionCaminar.setSiguienteDialogo(dialogoCaminar);


        this.dialogos.Add(decisionInicial);
        this.cargarTexto();
    }
}
