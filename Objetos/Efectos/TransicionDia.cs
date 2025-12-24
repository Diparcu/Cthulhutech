using Godot;
using System;

public partial class TransicionDia : Control
{
	private Color colorFondo = new Color(0, 0, 0, 0f);
	private Color colorTexto = new Color(1, 1, 1, 0f);
	private string textoMostrar;

	// Estados de la transición:
	// 0: Fade In (Negro aparece)
	// 1: Wait (Negro total) -> Aquí ocurre la carga
	// 2: Fade Out (Negro desaparece)
	// 3: Terminado
	public int Estado { get; private set; } = 0;

	private float alpha = 0f;
	private float velocidad = 1.5f; // Velocidad del fade
	private double tiempoEspera = 2.0;
	private double temporizador = 0;
	public bool ListoParaCargar { get { return Estado == 1; } }

	public TransicionDia(string texto)
	{
		this.textoMostrar = texto;
	}

	public void IniciarFadeOut()
	{
		if (Estado == 1)
		{
			Estado = 2;
		}
	}

	public void ActualizarTexto(string texto)
	{
		this.textoMostrar = texto;
		this.QueueRedraw();
	}

	public void comportamiento(Sistema sistema, double delta)
	{
		switch (Estado)
		{
			case 0: // Fade In
				alpha += (float)delta * velocidad;
				if (alpha >= 1f)
				{
					alpha = 1f;
					Estado = 1;
				}
				break;

			case 1: // Wait / Hold (Pantalla Negra)
				temporizador += delta;
				if (temporizador >= tiempoEspera)
				{
					this.IniciarFadeOut();
				}
				break;

			case 2: // Fade Out
				alpha -= (float)delta * velocidad;
				if (alpha <= 0f)
				{
					alpha = 0f;
					Estado = 3;
					sistema.setEstado(new SistemaEstadoJugando(sistema));
				}
				break;
		}

		colorFondo = new Color(0, 0, 0, alpha);
		// El texto aparece junto con el fondo o un poco retardado si se quisiera,
		// pero simple: alpha del texto igual al fondo
		colorTexto = new Color(1, 1, 1, alpha);

		this.QueueRedraw();
	}

	public override void _Draw()
	{
		// Fondo negro
		DrawRect(new Rect2(Vector2.Zero, GetViewportRect().Size), colorFondo);

		if (alpha > 0)
		{
			// Texto centrado
			Vector2 size = Sistema.fuente.GetStringSize(textoMostrar, HorizontalAlignment.Center, -1, 64);
			Vector2 position = (GetViewportRect().Size - size) / 2;
			position.Y += size.Y / 3; // Ajuste visual vertical

			DrawString(
				Sistema.fuente,
				position,
				textoMostrar,
				HorizontalAlignment.Center,
				-1,
				64,
				colorTexto
			);
		}
	}
}
