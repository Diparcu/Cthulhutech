using Godot;
using System;
using System.Collections.Generic;

public class PerkVozCrispante : Perk 
{
    public PerkVozCrispante(){
        this.nombre = "Voz crispante";
        this.descripcion = "No me acuerdo si el perk te bajaba la labia o la precencia, asique le puse que baje la labia noma :v.";
        this.nivelMaximo = 3;
        this.bonoEstadisticas = new Dictionary<string, int>{
            { Personaje.PRESENCIA, -2}
        };
    }
}
