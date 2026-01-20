using Godot;
using System.Collections.Generic;
using System;
using System.Linq;

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

	//Flags temporales
	public static readonly string CLASE_ASIENTO_FRONTAL = "Clase asiento frontal";
	public static readonly string CLASE_ASIENTO_CENTRAL = "Clase asiento central";
	public static readonly string CLASE_ASIENTO_TRASERO = "Clase asiento trasero";
	public static readonly string ALMUERZO_RAROS = "Almorzar con gente rara";
	public static readonly string ALMUERZO_POPULARES = "Almorzar con gente popular";
	public static readonly string ALMUERZO_SOSPECHOSOS = "Almorzar con gente sospechoza";

	private List<string> flagsDiarias = new List<string>{
		CLASE_ASIENTO_TRASERO,
		CLASE_ASIENTO_FRONTAL,
		CLASE_ASIENTO_CENTRAL
	};

	private Dictionary<string, bool> flags = new Dictionary<string, bool>
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

		//Flags temporales
		{ CLASE_ASIENTO_FRONTAL, false },
		{ CLASE_ASIENTO_CENTRAL, false },
		{ CLASE_ASIENTO_TRASERO, false },
	};


	public Dictionary<string, bool> Flag {get{ return this.flags;}}

	public void resetFlagsDiarias(){
		this.flagsDiarias.Where(this.flags.ContainsKey)
			.ToList()
			.ForEach(k => this.flags[k] = false);
	}

	public void updateFlag(String nombre){
		if (this.flags.ContainsKey(nombre)) 
			this.flags[nombre] = true;
	}

	public void updateFlag(String nombre, bool siono){
		if (this.flags.ContainsKey(nombre)) 
			this.flags[nombre] = siono;
	}

	public bool getFlag(String nombre){
		if (this.flags.ContainsKey(nombre)) 
			return this.flags[nombre];
		return false;
	}
}
