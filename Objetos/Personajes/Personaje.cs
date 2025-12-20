using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Personaje 
{
	public string origen;
	public string arquetipo;
	private string nombre = "Default";
	private string alias = "Default";
	private int orgon = 0;// 5 + media entre intelecto y tenacidad 
	private int reflejos = 0;// media entre agilidad intelecto y percepcion
	private int vitalidad = 0;// 5 + media entre fuerza y tenacidad

	private int locura = 0;
	public event Action OnLocuraMaxima;

	public int Locura
	{
		get { return locura; }
		set
		{
			locura = Math.Clamp(value, 0, 20);

			if(locura >= 5)
				EnsurePerk(new PerkLocuraLeve());
			if(locura >= 10)
				EnsurePerk(new PerkLocuraModerada());
			if(locura >= 15)
				EnsurePerk(new PerkLocuraSevera());

			if(locura >= 20)
				OnLocuraMaxima?.Invoke();
		}
	}

	private void EnsurePerk(Perk perk)
	{
		if(!this.perks.Any(p => p.Nombre == perk.Nombre))
		{
			this.addPerk(perk);
		}
	}

	private Flags flags;
	private List<Perk> perks = new List<Perk>();

	public static readonly string AGILIDAD = "Agilidad";
	public static readonly string FUERZA = "Fuerza";
	public static readonly string INTELIGENCIA = "Inteligencia";
	public static readonly string PERCEPCION = "Percepcion";
	public static readonly string PRESENCIA = "Presencia";
	public static readonly string TENACIDAD = "Tenacidad";

	public static readonly string LABIA = "Labia";

	public int Dinero { get; set; } = 0;
	public Flags Flags{ get{ return this.flags; } }

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
		get { return this.percepcion; }
		set { percepcion = value; }
	}

	public void subirPercepcion(){ this.subirEstadistica(ref this.percepcion); }
	public void bajarPercepcion(){ this.bajarEstadistica(ref this.percepcion); }

	public int Presencia
	{
		get { return presencia + this.getBonoPerk(PRESENCIA); }
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

	private int aficionesFutbol = 0;
	private int aficionesInfluencers = 0;
	private int arcanotecnico = 0;
	private int armero = 0;
	private int artillero = 0;
	private int artista = 0;
	private int atletismo = 0;
	private int bajosFondos = 0;
	private int banalidadesLavarRopa = 0;
	private int banalidadesJuegosDeCarta = 0;
	private int burocracia = 0;
	private int cienciasOcultas = 0;
	private int cienciasDeLaTierra = 0;
	private int cienciasDeLaVida = 0;
	private int cienciasFisicas = 0;
	private int comunicaciones = 0;
	private int conocimientoRegional = 0;
	private int cultura = 0;
	private int cumplimientoDeLaLey = 0;
	private int delincuencia = 0;
	private int demoliciones = 0;
	private int educacion = 0;
	private int historia = 0;
	private int idiomaIngles = 0;
	private int informatica = 0;
	private int ingenieria = 0;
	private int ingenieriaArcanotec = 0;
	private int interpretacion = 0;
	private int intimidar = 0;
	private int investigacion = 0;
	private int labia = 0;
	private int latrocinio = 0;
	private int lectoescritura = 0;
	private int medicina = 0;
	private int meditacion = 0;
	private int negocios = 0;
	private int observar = 0;
	private int persuacion = 0;
	private int pilotarAuto = 0;
	private int pilotarMecha = 0;
	private int pilotarSkate = 0;
	private int saviorFaire = 0;
	private int seduccion = 0;
	private int seguridad = 0;
	private int sigilo = 0;
	private int supervivencia = 0;
	private int tasacion = 0;
	private int tecnicoReparar = 0;
	private int vigilancia = 0;

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

	// --- HABILIDADES ---
	public int AficionesFutbol { get => aficionesFutbol; set => aficionesFutbol = value; }
	public void subirAficionesFutbol() { subirHabilidad(ref aficionesFutbol); }
	public void bajarAficionesFutbol() { bajarHabilidad(ref aficionesFutbol); }

	public int AficionesInfluencers { get => aficionesInfluencers; set => aficionesInfluencers = value; }
	public void subirAficionesInfluencers() { subirHabilidad(ref aficionesInfluencers); }
	public void bajarAficionesInfluencers() { bajarHabilidad(ref aficionesInfluencers); }

	public int Arcanotecnico { get => arcanotecnico; set => arcanotecnico = value; }
	public void subirArcanotecnico() { subirHabilidad(ref arcanotecnico); }
	public void bajarArcanotecnico() { bajarHabilidad(ref arcanotecnico); }

	public int Armero { get => armero; set => armero = value; }
	public void subirArmero() { subirHabilidad(ref armero); }
	public void bajarArmero() { bajarHabilidad(ref armero); }

	public int Artillero { get => artillero; set => artillero = value; }
	public void subirArtillero() { subirHabilidad(ref artillero); }
	public void bajarArtillero() { bajarHabilidad(ref artillero); }

	public int Artista { get => artista; set => artista = value; }
	public void subirArtista() { subirHabilidad(ref artista); }
	public void bajarArtista() { bajarHabilidad(ref artista); }

	public int Atletismo { get => atletismo; set => atletismo = value; } // Flexible: AGI/FUE
	public void subirAtletismo() { subirHabilidad(ref atletismo); }
	public void bajarAtletismo() { bajarHabilidad(ref atletismo); }

	public int BajosFondos { get => bajosFondos; set => bajosFondos = value; }
	public void subirBajosFondos() { subirHabilidad(ref bajosFondos); }
	public void bajarBajosFondos() { bajarHabilidad(ref bajosFondos); }

	public int BanalidadesLavarRopa { get => banalidadesLavarRopa; set => banalidadesLavarRopa = value; }
	public void subirBanalidadesLavarRopa() { subirHabilidad(ref banalidadesLavarRopa); }
	public void bajarBanalidadesLavarRopa() { bajarHabilidad(ref banalidadesLavarRopa); }

	public int BanalidadesJuegosDeCarta { get => banalidadesJuegosDeCarta; set => banalidadesJuegosDeCarta = value; }
	public void subirBanalidadesJuegosDeCarta() { subirHabilidad(ref banalidadesJuegosDeCarta); }
	public void bajarBanalidadesJuegosDeCarta() { bajarHabilidad(ref banalidadesJuegosDeCarta); }

	public int Burocracia { get => burocracia; set => burocracia = value; }
	public void subirBurocracia() { subirHabilidad(ref burocracia); }
	public void bajarBurocracia() { bajarHabilidad(ref burocracia); }

	public int CienciasOcultas { get => cienciasOcultas; set => cienciasOcultas = value; }
	public void subirCienciasOcultas() { subirHabilidad(ref cienciasOcultas); }
	public void bajarCienciasOcultas() { bajarHabilidad(ref cienciasOcultas); }

	public int CienciasDeLaTierra { get => cienciasDeLaTierra; set => cienciasDeLaTierra = value; }
	public void subirCienciasDeLaTierra() { subirHabilidad(ref cienciasDeLaTierra); }
	public void bajarCienciasDeLaTierra() { bajarHabilidad(ref cienciasDeLaTierra); }

	public int CienciasDeLaVida { get => cienciasDeLaVida; set => cienciasDeLaVida = value; }
	public void subirCienciasDeLaVida() { subirHabilidad(ref cienciasDeLaVida); }
	public void bajarCienciasDeLaVida() { bajarHabilidad(ref cienciasDeLaVida); }

	public int CienciasFisicas { get => cienciasFisicas; set => cienciasFisicas = value; }
	public void subirCienciasFisicas() { subirHabilidad(ref cienciasFisicas); }
	public void bajarCienciasFisicas() { bajarHabilidad(ref cienciasFisicas); }

	public int Comunicaciones { get => comunicaciones; set => comunicaciones = value; }
	public void subirComunicaciones() { subirHabilidad(ref comunicaciones); }
	public void bajarComunicaciones() { bajarHabilidad(ref comunicaciones); }

	public int ConocimientoRegional { get => conocimientoRegional; set => conocimientoRegional = value; }
	public void subirConocimientoRegional() { subirHabilidad(ref conocimientoRegional); }
	public void bajarConocimientoRegional() { bajarHabilidad(ref conocimientoRegional); }

	public int Cultura { get => cultura; set => cultura = value; }
	public void subirCultura() { subirHabilidad(ref cultura); }
	public void bajarCultura() { bajarHabilidad(ref cultura); }

	public int CumplimientoDeLaLey { get => cumplimientoDeLaLey; set => cumplimientoDeLaLey = value; }
	public void subirCumplimientoDeLaLey() { subirHabilidad(ref cumplimientoDeLaLey); }
	public void bajarCumplimientoDeLaLey() { bajarHabilidad(ref cumplimientoDeLaLey); }

	public int Delincuencia { get => delincuencia; set => delincuencia = value; }
	public void subirDelincuencia() { subirHabilidad(ref delincuencia); }
	public void bajarDelincuencia() { bajarHabilidad(ref delincuencia); }

	public int Demoliciones { get => demoliciones; set => demoliciones = value; }
	public void subirDemoliciones() { subirHabilidad(ref demoliciones); }
	public void bajarDemoliciones() { bajarHabilidad(ref demoliciones); }

	public int Educacion { get => educacion; set => educacion = value; }
	public void subirEducacion() { subirHabilidad(ref educacion); }
	public void bajarEducacion() { bajarHabilidad(ref educacion); }

	public int Historia { get => historia; set => historia = value; }
	public void subirHistoria() { subirHabilidad(ref historia); }
	public void bajarHistoria() { bajarHabilidad(ref historia); }

	public int IdiomaIngles { get => idiomaIngles; set => idiomaIngles = value; }
	public void subirIdiomaIngles() { subirHabilidad(ref idiomaIngles); }
	public void bajarIdiomaIngles() { bajarHabilidad(ref idiomaIngles); }

	public int Informatica { get => informatica; set => informatica = value; }
	public void subirInformatica() { subirHabilidad(ref informatica); }
	public void bajarInformatica() { bajarHabilidad(ref informatica); }

	public int Ingenieria { get => ingenieria; set => ingenieria = value; }
	public void subirIngenieria() { subirHabilidad(ref ingenieria); }
	public void bajarIngenieria() { bajarHabilidad(ref ingenieria); }

	public int IngenieriaArcanotec { get => ingenieriaArcanotec; set => ingenieriaArcanotec = value; }
	public void subirIngenieriaArcanotec() { subirHabilidad(ref ingenieriaArcanotec); }
	public void bajarIngenieriaArcanotec() { bajarHabilidad(ref ingenieriaArcanotec); }

	public int Interpretacion { get => interpretacion; set => interpretacion = value; }
	public void subirInterpretacion() { subirHabilidad(ref interpretacion); }
	public void bajarInterpretacion() { bajarHabilidad(ref interpretacion); }

	public int Intimidar { get => intimidar; set => intimidar = value; } // Flexible: PRE/FUE
	public void subirIntimidar() { subirHabilidad(ref intimidar); }
	public void bajarIntimidar() { bajarHabilidad(ref intimidar); }

	public int Investigacion { get => investigacion; set => investigacion = value; }
	public void subirInvestigacion() { subirHabilidad(ref investigacion); }
	public void bajarInvestigacion() { bajarHabilidad(ref investigacion); }

	public int Labia { get => this.labia + (int)Math.Ceiling(this.Presencia/2.0) + this.getBonoPerk(LABIA); set => labia = value; }
	public void subirLabia() { subirHabilidad(ref labia); }
	public void bajarLabia() { bajarHabilidad(ref labia); }

	public int Latrocinio { get => latrocinio; set => latrocinio = value; }
	public void subirLatrocinio() { subirHabilidad(ref latrocinio); }
	public void bajarLatrocinio() { bajarHabilidad(ref latrocinio); }

	public int Lectoescritura { get => lectoescritura; set => lectoescritura = value; }
	public void subirLectoescritura() { subirHabilidad(ref lectoescritura); }
	public void bajarLectoescritura() { bajarHabilidad(ref lectoescritura); }

	public int Medicina { get => medicina; set => medicina = value; }
	public void subirMedicina() { subirHabilidad(ref medicina); }
	public void bajarMedicina() { bajarHabilidad(ref medicina); }

	public int Meditacion { get => meditacion; set => meditacion = value; }
	public void subirMeditacion() { subirHabilidad(ref meditacion); }
	public void bajarMeditacion() { bajarHabilidad(ref meditacion); }

	public int Negocios { get => negocios; set => negocios = value; }
	public void subirNegocios() { subirHabilidad(ref negocios); }
	public void bajarNegocios() { bajarHabilidad(ref negocios); }

	public int Observar { get => observar; set => observar = value; }
	public void subirObservar() { subirHabilidad(ref observar); }
	public void bajarObservar() { bajarHabilidad(ref observar); }

	public int Persuasion { get => persuacion; set => persuacion = value; }
	public void subirPersuasion() { subirHabilidad(ref persuacion); }
	public void bajarPersuasion() { bajarHabilidad(ref persuacion); }

	public int PilotarAuto { get => pilotarAuto; set => pilotarAuto = value; }
	public void subirPilotarAuto() { subirHabilidad(ref pilotarAuto); }
	public void bajarPilotarAuto() { bajarHabilidad(ref pilotarAuto); }

	public int PilotarMecha { get => pilotarMecha; set => pilotarMecha = value; }
	public void subirPilotarMecha() { subirHabilidad(ref pilotarMecha); }
	public void bajarPilotarMecha() { bajarHabilidad(ref pilotarMecha); }

	public int PilotarSkate { get => pilotarSkate; set => pilotarSkate = value; }
	public void subirPilotarSkate() { subirHabilidad(ref pilotarSkate); }
	public void bajarPilotarSkate() { bajarHabilidad(ref pilotarSkate); }

	public int SaviorFaire { get => saviorFaire; set => saviorFaire = value; }
	public void subirSaviorFaire() { subirHabilidad(ref saviorFaire); }
	public void bajarSaviorFaire() { bajarHabilidad(ref saviorFaire); }

	public int Seduccion { get => seduccion; set => seduccion = value; }
	public void subirSeduccion() { subirHabilidad(ref seduccion); }
	public void bajarSeduccion() { bajarHabilidad(ref seduccion); }

	public int Seguridad { get => seguridad; set => seguridad = value; }
	public void subirSeguridad() { subirHabilidad(ref seguridad); }
	public void bajarSeguridad() { bajarHabilidad(ref seguridad); }

	public int Sigilo { get => sigilo; set => sigilo = value; }
	public void subirSigilo() { subirHabilidad(ref sigilo); }
	public void bajarSigilo() { bajarHabilidad(ref sigilo); }

	public int Supervivencia { get => supervivencia; set => supervivencia = value; }
	public void subirSupervivencia() { subirHabilidad(ref supervivencia); }
	public void bajarSupervivencia() { bajarHabilidad(ref supervivencia); }

	public int Tasacion { get => tasacion; set => tasacion = value; }
	public void subirTasacion() { subirHabilidad(ref tasacion); }
	public void bajarTasacion() { bajarHabilidad(ref tasacion); }

	public int TecnicoReparar { get => tecnicoReparar; set => tecnicoReparar = value; }
	public void subirTecnicoReparar() { subirHabilidad(ref tecnicoReparar); }
	public void bajarTecnicoReparar() { bajarHabilidad(ref tecnicoReparar); }

	public int Vigilancia { get => vigilancia; set => vigilancia = value; }
	public void subirVigilancia() { subirHabilidad(ref vigilancia); }
	public void bajarVigilancia() { bajarHabilidad(ref vigilancia); }

	// --- GetBase Methods ---
	public int GetBaseAficionesFutbol() { return this.aficionesFutbol; }
	public int GetBaseAficionesInfluencers() { return this.aficionesInfluencers; }
	public int GetBaseArcanotecnico() { return this.arcanotecnico; }
	public int GetBaseArmero() { return this.armero; }
	public int GetBaseArtillero() { return this.artillero; }
	public int GetBaseArtista() { return this.artista; }
	public int GetBaseAtletismo() { return this.atletismo; }
	public int GetBaseBajosFondos() { return this.bajosFondos; }
	public int GetBaseBanalidadesLavarRopa() { return this.banalidadesLavarRopa; }
	public int GetBaseBanalidadesJuegosDeCarta() { return this.banalidadesJuegosDeCarta; }
	public int GetBaseBurocracia() { return this.burocracia; }
	public int GetBaseCienciasOcultas() { return this.cienciasOcultas; }
	public int GetBaseCienciasDeLaTierra() { return this.cienciasDeLaTierra; }
	public int GetBaseCienciasDeLaVida() { return this.cienciasDeLaVida; }
	public int GetBaseCienciasFisicas() { return this.cienciasFisicas; }
	public int GetBaseComunicaciones() { return this.comunicaciones; }
	public int GetBaseConocimientoRegional() { return this.conocimientoRegional; }
	public int GetBaseCultura() { return this.cultura; }
	public int GetBaseCumplimientoDeLaLey() { return this.cumplimientoDeLaLey; }
	public int GetBaseDelincuencia() { return this.delincuencia; }
	public int GetBaseDemoliciones() { return this.demoliciones; }
	public int GetBaseEducacion() { return this.educacion; }
	public int GetBaseHistoria() { return this.historia; }
	public int GetBaseIdiomaIngles() { return this.idiomaIngles; }
	public int GetBaseInformatica() { return this.informatica; }
	public int GetBaseIngenieria() { return this.ingenieria; }
	public int GetBaseIngenieriaArcanotec() { return this.ingenieriaArcanotec; }
	public int GetBaseInterpretacion() { return this.interpretacion; }
	public int GetBaseIntimidar() { return this.intimidar; }
	public int GetBaseInvestigacion() { return this.investigacion; }
	public int GetBaseLabia() { return this.labia; }
	public int GetBaseLatrocinio() { return this.latrocinio; }
	public int GetBaseLectoescritura() { return this.lectoescritura; }
	public int GetBaseMedicina() { return this.medicina; }
	public int GetBaseMeditacion() { return this.meditacion; }
	public int GetBaseNegocios() { return this.negocios; }
	public int GetBaseObservar() { return this.observar; }
	public int GetBasePersuasion() { return this.persuacion; }
	public int GetBasePilotarAuto() { return this.pilotarAuto; }
	public int GetBasePilotarMecha() { return this.pilotarMecha; }
	public int GetBasePilotarSkate() { return this.pilotarSkate; }
	public int GetBaseSaviorFaire() { return this.saviorFaire; }
	public int GetBaseSeduccion() { return this.seduccion; }
	public int GetBaseSeguridad() { return this.seguridad; }
	public int GetBaseSigilo() { return this.sigilo; }
	public int GetBaseSupervivencia() { return this.supervivencia; }
	public int GetBaseTasacion() { return this.tasacion; }
	public int GetBaseTecnicoReparar() { return this.tecnicoReparar; }
	public int GetBaseVigilancia() { return this.vigilancia; }


	private int pelea = 0;
	private int armasArrojadizas = 0;
	private int esquivar = 0;

	public int ModificadorTiradasDiarias { get; set; } = 0;
	public int XP { get; set; } = 7000;

	// --- Aumentar Methods ---
	public void AumentarAficionesFutbol() { this.aficionesFutbol++; }
	public void AumentarAficionesInfluencers() { this.aficionesInfluencers++; }
	public void AumentarArcanotecnico() { this.arcanotecnico++; }
	public void AumentarArmero() { this.armero++; }
	public void AumentarArtillero() { this.artillero++; }
	public void AumentarArtista() { this.artista++; }
	public void AumentarAtletismo() { this.atletismo++; }
	public void AumentarBajosFondos() { this.bajosFondos++; }
	public void AumentarBanalidadesLavarRopa() { this.banalidadesLavarRopa++; }
	public void AumentarBanalidadesJuegosDeCarta() { this.banalidadesJuegosDeCarta++; }
	public void AumentarBurocracia() { this.burocracia++; }
	public void AumentarCienciasOcultas() { this.cienciasOcultas++; }
	public void AumentarCienciasDeLaTierra() { this.cienciasDeLaTierra++; }
	public void AumentarCienciasDeLaVida() { this.cienciasDeLaVida++; }
	public void AumentarCienciasFisicas() { this.cienciasFisicas++; }
	public void AumentarComunicaciones() { this.comunicaciones++; }
	public void AumentarConocimientoRegional() { this.conocimientoRegional++; }
	public void AumentarCultura() { this.cultura++; }
	public void AumentarCumplimientoDeLaLey() { this.cumplimientoDeLaLey++; }
	public void AumentarDelincuencia() { this.delincuencia++; }
	public void AumentarDemoliciones() { this.demoliciones++; }
	public void AumentarEducacion() { this.educacion++; }
	public void AumentarHistoria() { this.historia++; }
	public void AumentarIdiomaIngles() { this.idiomaIngles++; }
	public void AumentarInformatica() { this.informatica++; }
	public void AumentarIngenieria() { this.ingenieria++; }
	public void AumentarIngenieriaArcanotec() { this.ingenieriaArcanotec++; }
	public void AumentarInterpretacion() { this.interpretacion++; }
	public void AumentarIntimidar() { this.intimidar++; }
	public void AumentarInvestigacion() { this.investigacion++; }
	public void AumentarLabia() { this.labia++; }
	public void AumentarLatrocinio() { this.latrocinio++; }
	public void AumentarLectoescritura() { this.lectoescritura++; }
	public void AumentarMedicina() { this.medicina++; }
	public void AumentarMeditacion() { this.meditacion++; }
	public void AumentarNegocios() { this.negocios++; }
	public void AumentarObservar() { this.observar++; }
	public void AumentarPersuasion() { this.persuacion++; }
	public void AumentarPilotarAuto() { this.pilotarAuto++; }
	public void AumentarPilotarMecha() { this.pilotarMecha++; }
	public void AumentarPilotarSkate() { this.pilotarSkate++; }
	public void AumentarSaviorFaire() { this.saviorFaire++; }
	public void AumentarSeduccion() { this.seduccion++; }
	public void AumentarSeguridad() { this.seguridad++; }
	public void AumentarSigilo() { this.sigilo++; }
	public void AumentarSupervivencia() { this.supervivencia++; }
	public void AumentarTasacion() { this.tasacion++; }
	public void AumentarTecnicoReparar() { this.tecnicoReparar++; }
	public void AumentarVigilancia() { this.vigilancia++; }

	public void updateFlag(string nombre){
		this.flags.updateFlag(nombre);
	}

	public void updateFlag(string nombre, bool siono){
		this.flags.updateFlag(nombre, siono);
	}

	public bool getFlag(string nombre){
		return this.flags.getFlag(nombre);
	}

	public void inicializarFlags(){
		this.flags = new Flags();
	}

	public int getBonoPerk(string nombre){
		int bonoCompleto = 0;
		foreach(Perk perk in this.perks){
			bonoCompleto += perk.getBono(nombre);
		}
		return bonoCompleto;
	}

	public Personaje addPerk(Perk perk){
		Perk perkPreexistente = this.perks.FirstOrDefault(p => p.Nombre == perk.Nombre);
		if(perkPreexistente == null)
			this.perks.Add(perk);
		else
			perkPreexistente.subirNivel();

		return this;
	}
}
