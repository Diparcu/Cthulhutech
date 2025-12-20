using Godot;
using System.Collections.Generic;
using System;

public class Flags 
{
	public static readonly string CAPITULO_1 = "Capitulo 1";
	public static readonly string CAPITULO_2 = "Capitulo 2";
	public static readonly string CAPITULO_3 = "Capitulo 3";
	public static readonly string CAPITULO_4 = "Capitulo 4";
	public static readonly string ISLENO = "Isleño";
	public static readonly string BLANCO = "Blanco";
	public static readonly string BAJOS_FONDOS = "Bajos fondos";
	public static readonly string CHUD = "chud";
	public static readonly string CONOCE_A_SHINJI = "Conoce a shinji";

	private Dictionary<string, bool> variables = new Dictionary<string, bool>
	{
		{ CAPITULO_1, false },
		{ CAPITULO_2, false },
		{ CAPITULO_3, false },
		{ CAPITULO_4, false },
		{ ISLENO, false },
		{ BLANCO, false },
		{ BAJOS_FONDOS, false },
		{ CHUD, false },
		{ CONOCE_A_SHINJI, false },
	};

	public Dictionary<string, bool> Variables {get{ return this.variables;}}

	public void updateFlag(String nombre){
		if (variables.ContainsKey(nombre)) 
			variables[nombre] = true;
	}

	public void updateFlag(String nombre, bool siono){
		if (variables.ContainsKey(nombre)) 
			variables[nombre] = siono;
	}

	public bool getFlag(String nombre){
		if (variables.ContainsKey(nombre)) 
			return variables[nombre];
		return false;
	}
}
