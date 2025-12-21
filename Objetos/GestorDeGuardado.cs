using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

public static class GestorDeGuardado
{
	private static string RUTA_GUARDADO = "user://savegame.json";

	public class SaveData
	{
		public int NumeroDia { get; set; }
		public string FaseDia { get; set; }
		public PersonajeData Personaje { get; set; }
	}

	public class PersonajeData
	{
		public string Nombre { get; set; }
		public string Origen { get; set; }
		public string Arquetipo { get; set; }
		public int Dinero { get; set; }
		public int Locura { get; set; }
		public int XP { get; set; }
		public Dictionary<string, bool> Flags { get; set; }
		public List<string> Perks { get; set; }

		// Stats
		public int Agilidad { get; set; }
		public int Fuerza { get; set; }
		public int Inteligencia { get; set; }
		public int Percepcion { get; set; }
		public int Presencia { get; set; }
		public int Tenacidad { get; set; }

		// Skills
		public int AficionesFutbol { get; set; }
		public int AficionesInfluencers { get; set; }
		public int Arcanotecnico { get; set; }
		public int Armero { get; set; }
		public int Artillero { get; set; }
		public int Artista { get; set; }
		public int Atletismo { get; set; }
		public int BajosFondos { get; set; }
		public int BanalidadesLavarRopa { get; set; }
		public int BanalidadesJuegosDeCarta { get; set; }
		public int Burocracia { get; set; }
		public int CienciasOcultas { get; set; }
		public int CienciasDeLaTierra { get; set; }
		public int CienciasDeLaVida { get; set; }
		public int CienciasFisicas { get; set; }
		public int Comunicaciones { get; set; }
		public int ConocimientoRegional { get; set; }
		public int Cultura { get; set; }
		public int CumplimientoDeLaLey { get; set; }
		public int Delincuencia { get; set; }
		public int Demoliciones { get; set; }
		public int Educacion { get; set; }
		public int Historia { get; set; }
		public int IdiomaIngles { get; set; }
		public int Informatica { get; set; }
		public int Ingenieria { get; set; }
		public int IngenieriaArcanotec { get; set; }
		public int Interpretacion { get; set; }
		public int Intimidar { get; set; }
		public int Investigacion { get; set; }
		public int Labia { get; set; }
		public int Latrocinio { get; set; }
		public int Lectoescritura { get; set; }
		public int Medicina { get; set; }
		public int Meditacion { get; set; }
		public int Negocios { get; set; }
		public int Observar { get; set; }
		public int Persuasion { get; set; }
		public int PilotarAuto { get; set; }
		public int PilotarMecha { get; set; }
		public int PilotarSkate { get; set; }
		public int SaviorFaire { get; set; }
		public int Seduccion { get; set; }
		public int Seguridad { get; set; }
		public int Sigilo { get; set; }
		public int Supervivencia { get; set; }
		public int Tasacion { get; set; }
		public int TecnicoReparar { get; set; }
		public int Vigilancia { get; set; }
	}

	public static void GuardarPartida(Personaje personaje, int numeroDia, string faseDia)
	{
		var saveData = new SaveData
		{
			NumeroDia = numeroDia,
			FaseDia = faseDia,
			Personaje = new PersonajeData
			{
				Origen = personaje.origen,
				Arquetipo = personaje.arquetipo,
				Dinero = personaje.Dinero,
				Locura = personaje.Locura,
				XP = personaje.XP,
				Flags = personaje.Flags.Variables,
				Perks = personaje.GetPerkNames(),

				Agilidad = personaje.Agilidad,
				Fuerza = personaje.Fuerza,
				Inteligencia = personaje.Inteligencia,
				Percepcion = personaje.Percepcion,
				Presencia = personaje.Presencia,
				Tenacidad = personaje.Tenacidad,

				// Using properties instead of GetBase methods
				AficionesFutbol = personaje.AficionesFutbol,
				AficionesInfluencers = personaje.AficionesInfluencers,
				Arcanotecnico = personaje.Arcanotecnico,
				Armero = personaje.Armero,
				Artillero = personaje.Artillero,
				Artista = personaje.Artista,
				Atletismo = personaje.Atletismo,
				BajosFondos = personaje.BajosFondos,
				BanalidadesLavarRopa = personaje.BanalidadesLavarRopa,
				BanalidadesJuegosDeCarta = personaje.BanalidadesJuegosDeCarta,
				Burocracia = personaje.Burocracia,
				CienciasOcultas = personaje.CienciasOcultas,
				CienciasDeLaTierra = personaje.CienciasDeLaTierra,
				CienciasDeLaVida = personaje.CienciasDeLaVida,
				CienciasFisicas = personaje.CienciasFisicas,
				Comunicaciones = personaje.Comunicaciones,
				ConocimientoRegional = personaje.ConocimientoRegional,
				Cultura = personaje.Cultura,
				CumplimientoDeLaLey = personaje.CumplimientoDeLaLey,
				Delincuencia = personaje.Delincuencia,
				Demoliciones = personaje.Demoliciones,
				Educacion = personaje.Educacion,
				Historia = personaje.Historia,
				IdiomaIngles = personaje.IdiomaIngles,
				Informatica = personaje.Informatica,
				Ingenieria = personaje.Ingenieria,
				IngenieriaArcanotec = personaje.IngenieriaArcanotec,
				Interpretacion = personaje.Interpretacion,
				Intimidar = personaje.Intimidar,
				Investigacion = personaje.Investigacion,
				// Labia includes derived value, usually we want base.
				// However, setter updates backing field 'labia'.
				// The Property 'Labia' getter includes bonuses.
				// We should ideally save the base value.
				// Looking at Personaje.cs, 'GetBaseLabia()' returns the base 'labia' field.
				// For 'Labia' specifically, let's use GetBaseLabia() to avoid double-counting bonuses on load.
				// But wait, the previous reviewer said GetBase methods don't exist? I saw them in read_file.
				// I will use GetBaseLabia() if I am sure it exists. I am sure.
				// For consistency with other properties that are just fields (like AficionesFutbol), using property is fine.
				// But Labia has logic.
				// If I save the Property 'Labia' (which is base + bonus), and then Load it into 'Labia' setter (which sets base 'labia'),
				// I will artificially inflate the stat permanently.
				// Therefore, for Labia and stats with modifiers, I MUST use the method that returns the raw value.
				// Since I verified GetBase... methods exist in Personaje.cs, I will revert to using them for SAFETY.
				Labia = personaje.GetBaseLabia(),

				Latrocinio = personaje.Latrocinio,
				Lectoescritura = personaje.Lectoescritura,
				Medicina = personaje.Medicina,
				Meditacion = personaje.Meditacion,
				Negocios = personaje.Negocios,
				Observar = personaje.Observar,
				Persuasion = personaje.Persuasion,
				PilotarAuto = personaje.PilotarAuto,
				PilotarMecha = personaje.PilotarMecha,
				PilotarSkate = personaje.PilotarSkate,
				SaviorFaire = personaje.SaviorFaire,
				Seduccion = personaje.Seduccion,
				Seguridad = personaje.Seguridad,
				Sigilo = personaje.Sigilo,
				Supervivencia = personaje.Supervivencia,
				Tasacion = personaje.Tasacion,
				TecnicoReparar = personaje.TecnicoReparar,
				Vigilancia = personaje.Vigilancia
			}
		};

		string jsonString = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });

		using var file = Godot.FileAccess.Open(RUTA_GUARDADO, Godot.FileAccess.ModeFlags.Write);
		file.StoreString(jsonString);
	}

	public static SaveData CargarPartida()
	{
		if (!Godot.FileAccess.FileExists(RUTA_GUARDADO))
		{
			return null;
		}

		using var file = Godot.FileAccess.Open(RUTA_GUARDADO, Godot.FileAccess.ModeFlags.Read);
		string jsonString = file.GetAsText();

		try
		{
			return JsonSerializer.Deserialize<SaveData>(jsonString);
		}
		catch
		{
			return null;
		}
	}
}
