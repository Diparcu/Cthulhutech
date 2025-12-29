using Godot;
using System;
using System.Collections.Generic;

public class PerkVozCrispante : Perk 
{
    public PerkVozCrispante(){
        this.nombre = "Voz crispante";
        this.descripcion = "Tu voz resulta irritante para los demás, dificultando las interacciones sociales.";
        this.nivelMaximo = 3;
        this.bonoEstadisticas = new Dictionary<string, int>{
            { Personaje.PRESENCIA, -2}
        };
    }
}
