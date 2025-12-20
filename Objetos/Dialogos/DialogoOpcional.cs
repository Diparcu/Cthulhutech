using Godot;
using System;
using System.Collections.Generic;

public class DialogoOpcional 
{
	private List<Dialogo> proximoDialogoExito = new List<Dialogo>(); 
	private List<Dialogo> proximoDialogoFallo = new List<Dialogo>(); 

	String habilidad = "";
	String flag = "";
	int dificultad = 0;
	bool mostrarTirada = true;


    public DialogoOpcional setFlag(String flag){
        this.flag = flag;
        return this;
    }

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

    public DialogoOpcional agregarDialogoFallo(String dialogo){
        this.proximoDialogoFallo.Add(new Dialogo(dialogo));
        return this;
    }

    public DialogoOpcional agregarDialogoFallo(String nombre, String dialogo){
        this.proximoDialogoFallo.Add(new Dialogo(nombre, dialogo));
        return this;
    }

    public DialogoOpcional agregarDialogoExito(String dialogo){
        this.proximoDialogoExito.Add(new Dialogo(dialogo));
        return this;
    }

    public DialogoOpcional agregarDialogoExito(String nombre, String dialogo){
        this.proximoDialogoExito.Add(new Dialogo(nombre, dialogo));
        return this;
    }

    public List<Dialogo> checkeoDeHabilidad(Personaje personaje){
        ResultadoTirada tirada = new ResultadoTirada();
        tirada.checkeoHabilidad(personaje, this.habilidad, this.dificultad);
        if(tirada.Exito || personaje.getFlag(this.flag)){
            return this.generarMensajeDeCheckeo("\nExito",
                    this.proximoDialogoExito,
                    tirada);
        }else{
            return this.generarMensajeDeCheckeo("\nFallo",
                    this.proximoDialogoFallo,
                    tirada);
        }
    }

    private List<Dialogo> generarMensajeDeCheckeo(
            string mensajeAdicional,
            List<Dialogo> dialogos,
            ResultadoTirada tirada){

        if(!this.mostrarTirada || this.flag != "") return dialogos;

		List<Dialogo> dialogo; 
		String mensaje = "[ " + this.habilidad + " pasiva: " + this.dificultad + " ]";
        mensaje += "\nDados: -";

		foreach(int i in tirada.DadosLanzados){
			mensaje = mensaje + i + " - ";
		}

		mensaje += "\n";
		mensaje += "Total: " + tirada.SumaCombinacion;

        dialogo = new List<Dialogo>(dialogos); 
        mensaje += mensajeAdicional;

		dialogo.Insert(0, new Dialogo(mensaje));
		return dialogo;
    }
}
