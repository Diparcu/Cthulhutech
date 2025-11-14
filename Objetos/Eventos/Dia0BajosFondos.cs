using Godot;
using System;

public partial class Dia0BajosFondos : Node2D
{
    public override void _Ready()
    {
        var label = new Label();
        label.Text = "tu padre arquitecto y madre ausente, has tenido un padre ausente que se esfuerza por mantener una calidad de vida decente viviendo en la chatarrería de la zona salina";
        label.Position = new Vector2(100, 100);
        AddChild(label);
    }
}
