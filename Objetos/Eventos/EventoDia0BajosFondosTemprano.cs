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
        dialogoCaminar.Add(new Dialogo("Narrador", "Te han contado que cuando construyeron la arcología, la mayoría de la gente se mudó. Luego llegaron los vagabundos, quienes levantaron esas estructuras de metal que ves por todas partes, como parches sobre las heridas del paisaje."));
        dialogoCaminar.Add(new Dialogo("Narrador", "Pero eventualmente, ellos también se fueron a la arcología. O al menos eso dicen. Tú y tu padre son de los últimos habitantes de este yermo."));

        // Tirada Oculta: Observar / Percepción para notar detalles del entorno
        ResultadoTirada tiradaObservar = realizarTiradaCombinacion("Observar", "Percepcion", 11);
        string textoTiradaObservar = $"[color=darkgreen][Tirada Oculta - Observar diff 11: {string.Join("-", tiradaObservar.DadosLanzados)} | {(tiradaObservar.Exito ? "Éxito" : "Fallo")}][/color]";
        dialogoCaminar.Add(new Dialogo("Narrador", textoTiradaObservar));

        if (tiradaObservar.Exito)
        {
             dialogoCaminar.Add(new Dialogo("Narrador", "Te detienes frente a una casa sellada con tablones podridos. Notas algo extraño: el musgo en la puerta ha sido rasgado recientemente, y hay huellas frescas en el barro que no parecen de animal. Alguien —o algo— ha estado aquí hace poco."));
             dialogoCaminar.Add(new Dialogo("Narrador", "Un escalofrío te recorre la espalda. Quizás no están tan solos como creían."));
        }
        else
        {
             dialogoCaminar.Add(new Dialogo("Narrador", "Todo está cubierto de un musgo espeso. La madera vieja cruje bajo tus pies. Hay casas selladas que seguro esconden secretos de otras épocas."));
             dialogoCaminar.Add(new Dialogo("Narrador", "Es un lugar perfecto para un niño que busca aventuras, aunque entiendes por qué a un adulto le parecería un sitio lúgubre y deprimente."));
        }

        dialogoCaminar.Add(new Dialogo("Narrador", "El sol comienza a subir y el hambre te llama de vuelta a casa.").addAction(() => {
             this.getSistema().cargarEscena(new Dia0BajosFondosTarde(this.getSistema()));
        }));

        opcionCaminar.setSiguienteDialogo(dialogoCaminar);


        this.dialogos.Add(decisionInicial);
        this.cargarTexto();
    }
}
