using Godot;
using System;

public partial class TransicionDia : Control
{
    private Color color = new Color(0, 0, 0, 0f);
    private String palabra1;
    private String palabra2;
    private int estado = 0;
    private int contador = 0;
    private int contadorMaximo = 100;
    private float ratioDeCambio = 0.01f;
    private Vector2 posicionInicialString = new Vector2(1280/2, 640/2);
    private Vector2 separacionStrings = new Vector2(0, 640);

    public TransicionDia(String palabra1, String palabra2){
        this.palabra1 = palabra1;
        this.palabra2 = palabra2;
        this.posicionInicialString.X =
            this.posicionInicialString.X - this.palabra1.Length * 14;
    }

    public void comportamiento(Sistema sistema, double delta){
        switch (this.estado){
            case 0:
                this.setTransparencia(this.ratioDeCambio);
                break;
            case 2:
                this.contadorMaximo = 200;
                this.posicionInicialString.Y = this.posicionInicialString.Y + 6.4f;
                break;
            case 3:
                this.estado++;
                sistema.ActualizarUI();
                break;
            case 4:
                this.contadorMaximo = 100;
                break;
            case 5:
                this.setTransparencia(-this.ratioDeCambio);
                break;
            case 6:
                sistema.setEstado(new SistemaEstadoJugando(sistema));
                break;
        }

        if(this.contador >= 100){
            this.estado++;
            this.contador = 0;
        }
        this.contador++;
        this.QueueRedraw();
    } 

    public void setTransparencia(float transparencia){
        this.color = new Color(0, 0, 0, this.color.A + transparencia);
    }

    public override void _Draw(){
        this.DrawRect(
                new Rect2(Vector2.Zero, new Vector2(1280,640)),
                this.color,
                true
                );

        DrawString(
                Sistema.fuente, 
                this.posicionInicialString,
                this.palabra1, 
                0,
                -1,
                64,
                new Color(1, 1, 1, this.color.A));

        DrawString(
                Sistema.fuente, 
                this.posicionInicialString - this.separacionStrings,
                this.palabra2, 
                0,
                -1,
                64,
                new Color(1, 1, 1, this.color.A));
    }
}



