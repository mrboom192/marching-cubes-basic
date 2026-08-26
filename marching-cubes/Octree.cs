using System;
using Godot;

namespace marchingcubesbasic.examples;

[Tool]
public partial class Octree(Vector3 position, int resolution) : Node3D
{
	// In an octree, each node has eight children.
	private Octree[] _children = new Octree[8];
	private Aabb _bounds = new(position, Vector3.One * (float)Math.Pow(2, 4 + resolution));
	private bool _ran = false;

	// For now, what determines if the octree subdivides is if the origin is contained within it
	// For simplicity, we use Godot's built-in AABB class
	
	// TODO Implement a chunking function that takes _bounds.size, halves it, and repeats for 8 corners of the AABB
	
	// In the future, get rid of _Process and transition to a signal based approach
	// For now, the center (0, 0, 0) will be the point of interest
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (resolution <= 0)
		{
			if (!_ran)
			{
				AddChild(new Chunk(_bounds, new ProceduralWorld()));
			
				_ran =  true;
			}
			else
			{
				return;
			}
		}
		
		var children = GetChildren();
		
		if (_bounds.HasPoint(Vector3.Zero) && !_ran)
		{
			// Remove all children
			foreach(var child in children)
			{
				child.QueueFree();
			}
			
			AddChild(new Octree(_bounds.Position, resolution - 1));
			AddChild(new Octree(_bounds.Position * new Vector3(2, 1, 1), resolution - 1));
			AddChild(new Octree(_bounds.Position * new Vector3(1, 2, 1), resolution - 1));
			AddChild(new Octree(_bounds.Position * new Vector3(2, 2, 1), resolution - 1));
			AddChild(new Octree(_bounds.Position * new Vector3(1, 1, 2), resolution - 1));
			AddChild(new Octree(_bounds.Position * new Vector3(2, 1, 2), resolution - 1));
			AddChild(new Octree(_bounds.Position * new Vector3(1, 2, 2), resolution - 1));
			AddChild(new Octree(_bounds.Position * new Vector3(2, 2, 2), resolution - 1));

			_ran = true;
		}
		else if (!_ran)
		{
			// Remove all children
			foreach(var child in children)
			{
				child.QueueFree();
			}
			
			GD.Print("RAN2");
			AddChild(new Chunk(_bounds, new ProceduralWorld()));
			
			_ran =  true;
		}
	}
}
