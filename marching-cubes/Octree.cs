using System;
using Godot;

namespace marchingcubesbasic.examples;

[Tool]
public partial class Octree(Vector3 position, int resolution) : Node3D
{
	// In an octree, each node has eight children.
	private Octree[] _children = new Octree[8];
	private Aabb _bounds = new(position, Vector3.One * (1 << (4 + resolution)));

	// Relevant corners
	private readonly Vector3[] _childOrigins =
	[
		position,
		position + new Vector3(1, 0, 0) * (1 << (3 + resolution)),
		position + new Vector3(0, 1, 0) * (1 << (3 + resolution)),
		position + new Vector3(1, 1, 0) * (1 << (3 + resolution)),
		position + new Vector3(0, 0, 1) * (1 << (3 + resolution)),
		position + new Vector3(1, 0, 1) * (1 << (3 + resolution)),
		position + new Vector3(0, 1, 1) * (1 << (3 + resolution)),
		position + new Vector3(1, 1, 1) * (1 << (3 + resolution)),
	];
	
	// In the future, get rid of _Process and transition to a signal based approach
	// For now, the center (0, 0, 0) will be the point of interest
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Ready()
	{
		if (resolution <= 0 || !_bounds.HasPoint(Vector3.Zero))
		{
			AddChild(new Chunk(_bounds, new ProceduralWorld()));
			return;
		}
		
		// Remove all children
		//foreach(var child in children)
		//{
		//	child.QueueFree();
		//}
			
		// Add in children
		foreach(var origin in _childOrigins)
		{
			AddChild(new Octree(origin, resolution - 1));
		}
	}
}
