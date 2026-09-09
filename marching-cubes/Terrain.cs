using Godot;
using System;
using System.Diagnostics;
using System.Threading;
using marchingcubesbasic.examples;

[Tool]
public partial class Terrain : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
		const int resolution = 14;
		var a = Math.Pow(2, 4 + resolution);
		var location = (float)-a/2;
		
		ChunkLoader loader = new ChunkLoader();
		AddChild(loader);

		Octree octree = new Octree(new Vector3(location, location, location), resolution, loader);
		AddChild(octree);
	}
}
