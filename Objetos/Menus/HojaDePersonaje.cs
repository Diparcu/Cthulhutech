using Godot;
using System;
using System.Collections.Generic;

public partial class HojaDePersonaje : CanvasLayer
{
	private Sistema sistema;
	private Personaje personaje;
	private Label xpLabel;

	private Dictionary<string, Label> statLabels = new Dictionary<string, Label>();

	private readonly Dictionary<int, int> costoAtributos = new Dictionary<int, int>
	{
		{ 2, 10 }, { 3, 20 }, { 4, 30 }, { 5, 20 }, { 6, 25 }, { 7, 30 },
		{ 8, 35 }, { 9, 40 }, { 10, 45 }, { 11, 50 }
	};

	private readonly Dictionary<int, int> costoHabilidades = new Dictionary<int, int>
	{
		{ 1, 5 }, { 2, 10 }, { 3, 20 }, { 4, 20 }, { 5, 30 }
	};

	public HojaDePersonaje(Sistema sistema)
	{
		this.sistema = sistema;
		this.personaje = sistema.getJugador();
	}

	public override void _Ready()
	{
		// Fondo oscuro semitransparente para pausar el juego
		var background = new ColorRect
		{
			Color = new Color(0, 0, 0, 0.5f),
			Size = GetViewport().GetVisibleRect().Size
		};
		AddChild(background);

		// Panel principal
		PanelContainer panel = new PanelContainer
		{
			Position = new Vector2(100, 50),
			Size = new Vector2(1080, 540)
		};
		var styleBox = new StyleBoxFlat
		{
			BgColor = new Color(0.1f, 0.1f, 0.1f, 0.9f),
			CornerRadiusTopLeft = 10, CornerRadiusTopRight = 10,
			CornerRadiusBottomLeft = 10, CornerRadiusBottomRight = 10,
			BorderWidthLeft = 2, BorderWidthRight = 2, BorderWidthTop = 2, BorderWidthBottom = 2,
			BorderColor = new Color(0.5f, 0.5f, 0.5f)
		};
		panel.AddThemeStyleboxOverride("panel", styleBox);
		AddChild(panel);

		// Contenedor principal
		var mainContainer = new VBoxContainer { SizeFlagsHorizontal = Control.SizeFlags.ExpandFill };
		panel.AddChild(mainContainer);

		// Título
		var titulo = new Label
		{
			Text = "Hoja de Personaje",
			HorizontalAlignment = HorizontalAlignment.Center,
			AutowrapMode = TextServer.AutowrapMode.Word,
			CustomMinimumSize = new Vector2(1080, 0)
		};
		titulo.AddThemeFontSizeOverride("font_size", 36);
		titulo.AddThemeColorOverride("font_color", new Color(0.9f, 0.9f, 0.9f));
		mainContainer.AddChild(titulo);

		mainContainer.AddChild(new HSeparator());

		// XP
		var xpContainer = new HBoxContainer();
		xpContainer.AddChild(new Label { Text = "Puntos de Experiencia: " });
		xpLabel = new Label { Text = personaje.XP.ToString() };
		xpContainer.AddChild(xpLabel);
		mainContainer.AddChild(xpContainer);

		mainContainer.AddChild(new HSeparator());

		// Atributos y Habilidades
		var columns = new HBoxContainer { Alignment = HBoxContainer.AlignmentMode.Begin };
		mainContainer.AddChild(columns);

		var atributosContainer = new VBoxContainer { CustomMinimumSize = new Vector2(540, 0) };
		columns.AddChild(atributosContainer);

		var habilidadesContainer = new VBoxContainer { CustomMinimumSize = new Vector2(540, 0) };
		columns.AddChild(habilidadesContainer);

		// Títulos de columnas
		atributosContainer.AddChild(new Label { Text = "Atributos", HorizontalAlignment = HorizontalAlignment.Center });
		habilidadesContainer.AddChild(new Label { Text = "Habilidades", HorizontalAlignment = HorizontalAlignment.Center });
		atributosContainer.AddChild(new VSeparator());
		habilidadesContainer.AddChild(new VSeparator());

		// Añadir Atributos
		AddStatEntry(atributosContainer, "Agilidad", personaje.Agilidad, true);
		AddStatEntry(atributosContainer, "Fuerza", personaje.Fuerza, true);
		AddStatEntry(atributosContainer, "Inteligencia", personaje.Inteligencia, true);
		AddStatEntry(atributosContainer, "Percepción", personaje.Percepcion, true);
		AddStatEntry(atributosContainer, "Presencia", personaje.Presencia, true);
		AddStatEntry(atributosContainer, "Tenacidad", personaje.Tenacidad, true);

		// Añadir Habilidades
		AddStatEntry(habilidadesContainer, "Atletismo", personaje.GetBaseAtletismo(), false);
		AddStatEntry(habilidadesContainer, "Ciencias Ocultas", personaje.GetBaseCienciasOcultas(), false);
		AddStatEntry(habilidadesContainer, "Conocimiento Regional", personaje.GetBaseConocimientoRegional(), false);
		AddStatEntry(habilidadesContainer, "Educación", personaje.GetBaseEducacion(), false);
		AddStatEntry(habilidadesContainer, "Investigación", personaje.GetBaseInvestigacion(), false);
		AddStatEntry(habilidadesContainer, "Labia", personaje.GetBaseLabia(), false);
		AddStatEntry(habilidadesContainer, "Lectoescritura", personaje.GetBaseLectoescritura(), false);
		AddStatEntry(habilidadesContainer, "Medicina", personaje.GetBaseMedicina(), false);
		AddStatEntry(habilidadesContainer, "Savoir Faire", personaje.GetBaseSaviorFaire(), false);
		AddStatEntry(habilidadesContainer, "Sigilo", personaje.GetBaseSigilo(), false);
		AddStatEntry(habilidadesContainer, "Supervivencia", personaje.GetBaseSupervivencia(), false);
		AddStatEntry(habilidadesContainer, "Tasación", personaje.GetBaseTasacion(), false);

		// Botón de cerrar
		var cerrarBtn = new Button { Text = "Cerrar", Position = new Vector2(980, 500) };
		cerrarBtn.Pressed += () => this.QueueFree();
		cerrarBtn.Pressed += this.cambiarEstadoCerrarHoja;
		mainContainer.AddChild(cerrarBtn);
	}

	private void cambiarEstadoCerrarHoja(){
		this.sistema.setEstado(new SistemaEstadoJugando(this.sistema));
	}


	private void AddStatEntry(VBoxContainer container, string name, int value, bool isAttribute)
	{
		var hBox = new HBoxContainer();
		var nameLabel = new Label { Text = name, CustomMinimumSize = new Vector2(200, 0) };
		var valueLabel = new Label { Text = value.ToString(), CustomMinimumSize = new Vector2(50, 0) };
		var addButton = new Button { Text = "+" };

		statLabels[name] = valueLabel;

		addButton.Pressed += () => OnAddButtonPressed(name, isAttribute);

		hBox.AddChild(nameLabel);
		hBox.AddChild(valueLabel);
		hBox.AddChild(addButton);
		container.AddChild(hBox);
	}

	private void OnAddButtonPressed(string name, bool isAttribute)
	{
		int currentLevel, cost;
		Dictionary<int, int> costTable;

		if (isAttribute)
		{
			costTable = costoAtributos;
			switch (name)
			{
				case "Agilidad": currentLevel = personaje.Agilidad; break;
				case "Fuerza": currentLevel = personaje.Fuerza; break;
				case "Inteligencia": currentLevel = personaje.Inteligencia; break;
				case "Percepción": currentLevel = personaje.Percepcion; break;
				case "Presencia": currentLevel = personaje.Presencia; break;
				case "Tenacidad": currentLevel = personaje.Tenacidad; break;
				default: return;
			}
		}
		else
		{
			costTable = costoHabilidades;
			switch (name)
			{
				case "Atletismo": currentLevel = personaje.GetBaseAtletismo(); break;
				case "Ciencias Ocultas": currentLevel = personaje.GetBaseCienciasOcultas(); break;
				case "Conocimiento Regional": currentLevel = personaje.GetBaseConocimientoRegional(); break;
				case "Educación": currentLevel = personaje.GetBaseEducacion(); break;
				case "Investigación": currentLevel = personaje.GetBaseInvestigacion(); break;
				case "Labia": currentLevel = personaje.GetBaseLabia(); break;
				case "Lectoescritura": currentLevel = personaje.GetBaseLectoescritura(); break;
				case "Medicina": currentLevel = personaje.GetBaseMedicina(); break;
				case "Savoir Faire": currentLevel = personaje.GetBaseSaviorFaire(); break;
				case "Sigilo": currentLevel = personaje.GetBaseSigilo(); break;
				case "Supervivencia": currentLevel = personaje.GetBaseSupervivencia(); break;
				case "Tasación": currentLevel = personaje.GetBaseTasacion(); break;
				default: return;
			}
		}

		if (costTable.TryGetValue(currentLevel + 1, out cost) && personaje.XP >= cost)
		{
			personaje.XP -= cost;

			// Actualizar el stat en el personaje
			if (isAttribute)
			{
				switch (name)
				{
					case "Agilidad": personaje.Agilidad++; break;
					case "Fuerza": personaje.Fuerza++; break;
					case "Inteligencia": personaje.Inteligencia++; break;
					case "Percepción": personaje.Percepcion++; break;
					case "Presencia": personaje.Presencia++; break;
					case "Tenacidad": personaje.Tenacidad++; break;
				}
			}
			else
			{
				switch (name)
				{
					case "Atletismo": personaje.AumentarAtletismo(); break;
					case "Ciencias Ocultas": personaje.AumentarCienciasOcultas(); break;
					case "Conocimiento Regional": personaje.AumentarConocimientoRegional(); break;
					case "Educación": personaje.AumentarEducacion(); break;
					case "Investigación": personaje.AumentarInvestigacion(); break;
					case "Labia": personaje.AumentarLabia(); break;
					case "Lectoescritura": personaje.AumentarLectoescritura(); break;
					case "Medicina": personaje.AumentarMedicina(); break;
					case "Savoir Faire": personaje.AumentarSaviorFaire(); break;
					case "Sigilo": personaje.AumentarSigilo(); break;
					case "Supervivencia": personaje.AumentarSupervivencia(); break;
					case "Tasación": personaje.AumentarTasacion(); break;
				}
			}

			UpdateUI();
		}
	}

	private void UpdateUI()
	{
		xpLabel.Text = personaje.XP.ToString();
		statLabels["Agilidad"].Text = personaje.Agilidad.ToString();
		statLabels["Fuerza"].Text = personaje.Fuerza.ToString();
		statLabels["Inteligencia"].Text = personaje.Inteligencia.ToString();
		statLabels["Percepción"].Text = personaje.Percepcion.ToString();
		statLabels["Presencia"].Text = personaje.Presencia.ToString();
		statLabels["Tenacidad"].Text = personaje.Tenacidad.ToString();
		statLabels["Atletismo"].Text = personaje.GetBaseAtletismo().ToString();
		statLabels["Ciencias Ocultas"].Text = personaje.GetBaseCienciasOcultas().ToString();
		statLabels["Conocimiento Regional"].Text = personaje.GetBaseConocimientoRegional().ToString();
		statLabels["Educación"].Text = personaje.GetBaseEducacion().ToString();
		statLabels["Investigación"].Text = personaje.GetBaseInvestigacion().ToString();
		statLabels["Labia"].Text = personaje.GetBaseLabia().ToString();
		statLabels["Lectoescritura"].Text = personaje.GetBaseLectoescritura().ToString();
		statLabels["Medicina"].Text = personaje.GetBaseMedicina().ToString();
		statLabels["Savoir Faire"].Text = personaje.GetBaseSaviorFaire().ToString();
		statLabels["Sigilo"].Text = personaje.GetBaseSigilo().ToString();
		statLabels["Supervivencia"].Text = personaje.GetBaseSupervivencia().ToString();
		statLabels["Tasación"].Text = personaje.GetBaseTasacion().ToString();
	}
}
