using System;
using Godot;

namespace marchingcubesbasic.examples;

[Tool]
public partial class Octree(Vector3 position, int resolution, byte depth) : Node3D
{
	// In an octree, each node has eight children.
	private Octree[] _children = new Octree[8];
	private Aabb _bounds = new(position, Vector3.One * (float)Math.Pow(2, 4 + resolution));
	private readonly byte _depth = depth;

	// For now, what determines if the octree subdivides is if the origin is contained within it
	// For simplicity, we use Godot's built-in AABB class
	
	// TODO Implement a chunking function that takes _bounds.size, halves it, and repeats for 8 corners of the AABB
	
	// For now, the center (0, 0, 0) will be the point of interest
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var children = GetChildren();
		if (_bounds.HasPoint(Vector3.Zero))
		{
			// Remove all children
			foreach(var child in children)
			{
				child.QueueFree();
			}
			
			AddChild(new Chunk(_bounds, new ProceduralWorld()));
		}
	}
	
}
