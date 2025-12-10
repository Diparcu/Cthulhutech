using Godot;
using System;
using System.Collections.Generic;

public class DialogoOpcional 
{
	private List<Dialogo> proximoDialogoExito = new List<Dialogo>(); 
	private List<Dialogo> proximoDialogoFallo = new List<Dialogo>(); 

	String habilidad;
	int dificultad = 0;

    public DialogoOpcional setHabilidad(String habilidad){
        this.habilidad = habilidad;
        return this;
    }

    public DialogoOpcional setDificultad(int dificultad){
        this.dificultad = dificultad;
        return this;
    }

    public DialogoOpcional setDialogoFallo(List<Dialogo> dialogo){
        this.proximoDialogoFallo = dialogo;
        return this;
    }

    public DialogoOpcional setDialogoExito(List<Dialogo> dialogo){
        this.proximoDialogoExito = dialogo;
        return this;
    }

    public List<Dialogo> checkeoDeHabilidad(Personaje personaje){
        ResultadoTirada tirada = new ResultadoTirada();
        tirada.checkeoHabilidad(personaje, this.habilidad, this.dificultad);
        GD.Print(tirada.Exito);
        if(tirada.Exito){
            return this.proximoDialogoExito;
        }else{
            return this.proximoDialogoFallo;
        }
    }
}
