using Godot;
using System;
using marchingcubesbasic.examples;

[Tool]
public partial class Terrain : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		const int resolution = 14;
		var a = Math.Pow(2, 4 + resolution);
		var location = (float)-a/2;
		
		ProceduralWorld sample = new(1);
		
		ChunkLoader loader = new ChunkLoader();
		AddChild(loader);

		Octree octree = new Octree(new Vector3(location, location, location), resolution, loader, sample);
		AddChild(octree);

		var cube = new MeshInstance3D();
		cube.Mesh = new BoxMesh();
		AddChild(cube);
		cube.Position = new Vector3(0, 0, 0);
		GD.Print("The cube is spawned at " +  cube.Position);
		cube.Position = sample.GetNearestSurfacePosition(cube.Position);
		GD.Print("The cube was moved to " +  cube.Position);
	}
}
