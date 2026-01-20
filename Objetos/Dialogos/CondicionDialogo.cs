using Godot;
using System;
using System.Collections.Generic;

public partial class CondicionDialogo 
{
    private string flag;

	public string Flag
	{
		get { return flag; }
	}
    public CondicionDialogo(string flag){
        this.flag = flag;
    }

}
