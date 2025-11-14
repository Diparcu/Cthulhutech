using Godot;
using System;

public partial class Dia0Blanco : Node2D
{
    public override void _Ready()
    {
        var label = new Label();
        label.Text = "Nacido en la arcología, de padre humano y madre nazzadi, su raro albinismo es señal de poderes sobrehumnos aún por descubrir, sabido por el estado, un agente de la NGT (nuevo gobierno terrestre) está atento a ti y tus talentos";
        label.Position = new Vector2(100, 100);
        AddChild(label);
    }
}
