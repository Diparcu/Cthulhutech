using Godot;
using System.Collections.Generic;
using System;

public class Flags 
{
	public static readonly string ISLENO = "Isleño";
	public static readonly string BLANCO = "Blanco";
	public static readonly string BAJOS_FONDOS = "Bajos fondos";
	public static readonly string CHUD = "chud";

	Dictionary<string, bool> variables = new Dictionary<string, bool>
	{
		{ ISLENO, false },
		{ BLANCO, false },
		{ BAJOS_FONDOS, false },
		{ CHUD, false },
	};

	public void setFlag(String nombre){
		if (variables.ContainsKey(nombre)) 
			variables[nombre] = true;
	}

	public bool getFlag(String nombre){
		if (variables.ContainsKey(nombre)) 
			return variables[nombre];
		return false;
	}
}
