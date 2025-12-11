
using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class ResultadoTirada
{
	public bool Exito { get; set; }
	public string CombinacionNombre { get; set; }
	public int SumaCombinacion { get; set; }
	public List<int> DadosLanzados { get; set; }
	public List<int> DadosEnCombinacion { get; set; }

	public ResultadoTirada()
	{
		DadosLanzados = new List<int>();
		DadosEnCombinacion = new List<int>();
		CombinacionNombre = "Ninguna";
		SumaCombinacion = 0;
		Exito = false;
	}

	public void checkeoHabilidad(
			Personaje personaje,
			string habilidad,
			int dificultad){
		if(habilidad == "") return;
		int valorHabilidad = this.getValorHabilidad(personaje, habilidad);
		this.tirarDados(valorHabilidad);
		this.generarResultadoAlto();
		if(this.SumaCombinacion >= dificultad) this.Exito = true;
	}

	private int getValorHabilidad(Personaje jugador,
			string habilidad){
		Type tipo = typeof(Personaje);
		PropertyInfo propiedad = tipo.GetProperty(habilidad);
		return (int)Math.Ceiling((double)propiedad.GetValue(jugador).ToString().ToInt());
	}

	private void tirarDados(int cantidadDados){
		var rand = new Random();
		List<int> resultados = new List<int>();

		for(int i = 0; i < cantidadDados; i++){
			resultados.Add(rand.Next(10) + 1);
		}

		resultados.Sort();
		this.DadosLanzados = resultados;
	}

	private void generarResultadoAlto(){
		int resultadoMasAlto = 0;

		resultadoMasAlto = Math.Max(
				this.generarResultadoNumeroRepetido(),
				this.generarResultadoEscala());

		this.SumaCombinacion = resultadoMasAlto;
	}

	private int generarResultadoEscala(){
		List<int> lista = this.DadosLanzados.Distinct().ToList();
		int total = 0, totalTemporal = 0, iteraciones = 0;

		for(int i = 0; i < lista.Count - 1; i++){
			totalTemporal = lista[i];
			iteraciones = 1;
			for(int j = i; j < lista.Count - 1; j++){
				if(lista[j] + 1 == lista[j + 1]){
					totalTemporal += lista[j + 1];
					iteraciones++;
				}else{
					break;
				}
			}
			if(total < totalTemporal && iteraciones >= 3) total = totalTemporal;
		}

		return total;
	}

	private int generarResultadoNumeroRepetido(){
		int totalTemporal, total = 0;
		for(int i = 1; i <= 10; i++){
			totalTemporal = this.generarResultadoNumeroRepetidoSumatoria(this.DadosLanzados, i);
			if(total < totalTemporal) total = totalTemporal;
		}
		return total;
	}

	private int generarResultadoNumeroRepetidoSumatoria(
			List<int> resultados,
			int numero){
		int total = 0;
		foreach(int i in resultados){
			if(numero == i) total += i;
		}
		return total;
	}

}
