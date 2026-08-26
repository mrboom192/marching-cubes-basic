using Godot;
using System;
using marchingcubesbasic.examples;

[Tool]
public partial class Main : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var resolution = 0;
		var a = Math.Pow(2, 4 + resolution);
		var location = (float)-a/2;
		AddChild(new Octree(new Vector3(location,location,location), 4));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
