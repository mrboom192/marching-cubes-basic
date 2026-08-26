using Godot;
using System;
using marchingcubesbasic.examples;

[Tool]
public partial class Main : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddChild(new Octree(new Vector3(0f,0f,0f), 0, 1));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
