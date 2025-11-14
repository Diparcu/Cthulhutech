using Godot;
using System;

public partial class Personaje 
{
	public string origen;
	public string arquetipo;
	private string nombre = "Default";
	private string alias = "Default";
	private int orgon = 0;// 5 + media entre intelecto y tenacidad 
	private int reflejos = 0;// media entre agilidad intelecto y percepcion
	private int vitalidad = 0;// 5 + media entre fuerza y tenacidad

    public int Dinero { get; set; } = 0;

    public void resetearStats()
    {
        this.agilidad = 5;
        this.fuerza = 5;
        this.inteligencia = 5;
        this.percepcion = 5;
        this.presencia = 5;
        this.tenacidad = 5;
        this.puntosDeEstadistica = 6;
    }

	public int Orgon
	{
		get { return 5 + this.inteligencia/2 + this.tenacidad/2; }
		set { this.orgon = value; }
	}

	public int Reflejos
	{
		get { return this.agilidad/ 3 + this.inteligencia/3 + this.percepcion/3; }
		set { this.reflejos = value; }
	}

	public int Vitalidad
	{
		get { return 5 + (this.fuerza + this.tenacidad)/2; }
		set { this.vitalidad = value; }
	}

	//35 puntos a distribuir
	private int puntosDeEstadistica = 6;
	private int agilidad = 5;
	private int fuerza = 5;
	private int inteligencia = 5;
	private int percepcion = 5;
	private int presencia = 5;
	private int tenacidad = 5;

	public int PuntosDeEstadistica
	{
		get { return this.puntosDeEstadistica; }
	}

	private void subirEstadistica(ref int estadistica){
		if(this.puntosDeEstadistica == 0 || estadistica >= 10) return;
		estadistica++;
		this.puntosDeEstadistica--;
	}

	private void bajarEstadistica(ref int estadistica){
		if(estadistica <= 1) return;
		estadistica--;
		this.puntosDeEstadistica++;
	}

	public int Agilidad
	{
		get { return agilidad; }
		set { 
			agilidad = value;
		}
	}

	public void subirAgilidad(){ this.subirEstadistica(ref this.agilidad); }
	public void bajarAgilidad(){ this.bajarEstadistica(ref this.agilidad); }

	public int Fuerza
	{
		get { return fuerza; }
		set { fuerza = value; }
	}

	public void subirFuerza(){ this.subirEstadistica(ref this.fuerza); }
	public void bajarFuerza(){ this.bajarEstadistica(ref this.fuerza); }

	public int Inteligencia
	{
		get { return inteligencia; }
		set { inteligencia = value; }
	}

	public void subirInteligencia(){ this.subirEstadistica(ref this.inteligencia); }
	public void bajarInteligencia(){ this.bajarEstadistica(ref this.inteligencia); }

	public int Percepcion
	{
		get { return percepcion; }
		set { percepcion = value; }
	}

	public void subirPercepcion(){ this.subirEstadistica(ref this.percepcion); }
	public void bajarPercepcion(){ this.bajarEstadistica(ref this.percepcion); }

	public int Presencia
	{
		get { return presencia; }
		set { presencia = value; }
	}

	public void subirPresencia(){ this.subirEstadistica(ref this.presencia); }
	public void bajarPresencia(){ this.bajarEstadistica(ref this.presencia); }

	public int Tenacidad
	{
		get { return tenacidad; }
		set { tenacidad = value; }
	}

	public void subirTenacidad(){ this.subirEstadistica(ref this.tenacidad); }
	public void bajarTenacidad(){ this.bajarEstadistica(ref this.tenacidad); }


	//20 puntos a distribuir entre habilidades y perks
	private int puntosDeHabilidad = 20;
	private int atletismo = 0;
	private int cienciasOcultas = 0;
	private int conocimientoRegional = 0;
	private int educacion = 0;
	private int investigacion = 0;
	private int labia = 0;
	private int leptoescritura = 0;
	private int medicina = 0;
	private int saivorFeire = 0;
	private int sigilo = 0;
	private int supervivencia = 0;
	private int tasacion = 0;

	public int PuntosDeHabilidad
	{
		get { return this.puntosDeHabilidad; }
	}

	private void subirHabilidad(ref int habilidad){
		if(this.puntosDeHabilidad == 0 || habilidad >= 10) return;
		habilidad++;
		this.puntosDeHabilidad--;
	}

	private void bajarHabilidad(ref int habilidad){
		if(habilidad <= 1) return;
		habilidad--;
		this.puntosDeHabilidad++;
	}

	public int Atletismo
	{
		get { return atletismo + this.fuerza/2; }
		set { atletismo = value; }
	}

	public void subirAtletismo(){ this.subirHabilidad(ref this.atletismo); }
	public void bajarAtletismo(){ this.bajarHabilidad(ref this.atletismo); }

	public int CienciasOcultas
	{
		get { return cienciasOcultas + this.inteligencia/2; }
		set { cienciasOcultas = value; }
	}

	public void subirCienciasOcultas(){ this.subirHabilidad(ref this.cienciasOcultas); }
	public void bajarCienciasOcultas(){ this.bajarHabilidad(ref this.cienciasOcultas); }

	public int ConocimientoRegional
	{
		get { return conocimientoRegional + this.inteligencia/2; }
		set { conocimientoRegional = value; }
	}

	public void subirConocimientoRegional(){ this.subirHabilidad(ref this.conocimientoRegional); }
	public void bajarConocimientoRegional(){ this.bajarHabilidad(ref this.conocimientoRegional); }

	public int Educacion
	{
		get { return educacion + this.inteligencia/2; }
		set { Educacion = value; }
	}

	public void subirEducacion(){ this.subirHabilidad(ref this.educacion); }
	public void bajarEducacion(){ this.bajarHabilidad(ref this.educacion); }

	public int Investigacion
	{
		get { return this.investigacion + this.inteligencia/2; }
		set { this.investigacion = value; }
	}

	public void subirInvestigacion(){ this.subirHabilidad(ref this.investigacion); }
	public void bajarInvestigacion(){ this.bajarHabilidad(ref this.investigacion); }

	public int Labia
	{
		get { return this.labia + this.presencia/2; }
		set { this.labia = value; }
	}

	public void subirLabia(){ this.subirHabilidad(ref this.labia); }
	public void bajarLabia(){ this.bajarHabilidad(ref this.labia); }

	public int Leptoescritura
	{
		get { return this.leptoescritura + this.inteligencia/2; }
		set { this.leptoescritura = value; }
	}

	public void subirLeptoescritura(){ this.subirHabilidad(ref this.leptoescritura); }
	public void bajarLeptoescritura(){ this.bajarHabilidad(ref this.leptoescritura); }

	public int Medicina
	{
		get { return this.medicina + this.inteligencia/2; }
		set { this.medicina = value; }
	}

	public void subirMedicina(){ this.subirHabilidad(ref this.medicina); }
	public void bajarMedicina(){ this.bajarHabilidad(ref this.medicina); }

	public int SaivorFeire
	{
		get { return this.saivorFeire + this.presencia/2; }
		set { this.saivorFeire = value; }
	}

	public void subirSaivorFeire(){ this.subirHabilidad(ref this.saivorFeire); }
	public void bajarSaivorFeire(){ this.bajarHabilidad(ref this.saivorFeire); }

	public int Sigilo
	{
		get { return this.sigilo + this.agilidad/2; }
		set { this.sigilo = value; }
	}

	public void subirSigilo(){ this.subirHabilidad(ref this.sigilo); }
	public void bajarSigilo(){ this.bajarHabilidad(ref this.sigilo); }

	public int Supervivencia
	{
		get { return this.supervivencia + this.tenacidad/2; }
		set { this.supervivencia = value; }
	}

	public void subirSupervivencia(){ this.subirHabilidad(ref this.supervivencia); }
	public void bajarSupervivencia(){ this.bajarHabilidad(ref this.supervivencia); }

	public int Tasacion
	{
		get { return this.tasacion + this.inteligencia/2; }
		set { this.tasacion = value; }
	}

	public void subirTasacion(){ this.subirHabilidad(ref this.tasacion); }
	public void bajarTasacion(){ this.bajarHabilidad(ref this.tasacion); }

	private int pelea = 0;
	private int armasArrojadizas = 0;
	private int esquivar = 0;

	public int ModificadorTiradasDiarias { get; set; } = 0;
    public int XP { get; set; } = 0;
}
