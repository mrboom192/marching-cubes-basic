using System;
using Godot;

namespace marchingcubesbasic.examples;

[Tool]
public partial class Octree(Vector3 position, int resolution, ChunkLoader loader) : Node3D
{
	private Aabb _bounds = new(position, Vector3.One * (1 << (4 + resolution)));
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
	
	// In the future, get rid of _Ready and transition to a signal based approach
	// For now, the center (0, 0, 0) will be the point of interest
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Ready()
	{
		if (resolution <= 0 || !_bounds.HasPoint(Vector3.Zero))
		{
			AddChild(new Chunk(_bounds, loader));
			return;
		}

		// Add in children
		foreach(var origin in _childOrigins)
		{
			AddChild(new Octree(origin, resolution - 1, loader));
		}
	}
}
