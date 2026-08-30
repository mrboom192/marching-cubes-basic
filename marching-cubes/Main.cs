using Godot;
using System;
using System.Diagnostics;
using marchingcubesbasic.examples;

[Tool]
public partial class Main : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		const int resolution = 10;
		var a = Math.Pow(2, 4 + resolution);
		var location = (float)-a/2;
		
		var stopwatch = Stopwatch.StartNew();
		AddChild(new Octree(new Vector3(location,location,location), resolution));
		stopwatch.Stop();
		
		GD.Print($"Octree took {stopwatch.ElapsedMilliseconds} ms");
	}
}
