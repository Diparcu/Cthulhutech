using Godot;
using System;
using System.Collections.Generic;

public partial class UpdateFlag 
{
    string flag;
    bool value = true;

	public string Flag {get{ return this.flag;}}
	public bool Value {get{ return this.value;}}

    public UpdateFlag(string flag){
        this.flag = flag;
    }

    public UpdateFlag(string flag,
            bool value){
        this.flag = flag;
        this.value = value;
    }
}


