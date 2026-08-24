using Godot;

namespace marchingcubesbasic.examples;

[Tool]
public partial class Octree(Vector3 position, Vector3 size, byte depth) : Node3D
{
	// In an octree, each node has eight children.
	private Octree[] _children = new Octree[8];
	private Aabb _bounds = new(position, size);
	private readonly byte _depth = depth;

	// For now, what determines if the octree subdivides is if the origin is contained within it
	// For simplicity, we use Godot's built-in AABB class
	
	// TODO Implement a chunking function that takes _bounds.size, halves it, and repeats for 8 corners of the AABB
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddChild(new Chunk(_bounds));

		// TODO Implement a camera which passes in its position here
		// if (_depth < 3 && _bounds.HasPoint(Vector3.Zero))
		// {
		// 	
		// }

		// AddChild(new Chunk(Vector3I.Zero, 1));
		// AddChild(new Chunk(new Vector3I(0, 16, 0), 1));
		// AddChild(new Chunk(new Vector3I(16, 0, 0), 1));
		// AddChild(new Chunk(new Vector3I(0, 0, 16), 1));
		// AddChild(new Chunk(new Vector3I(16, 16, 0), 1));
		// AddChild(new Chunk(new Vector3I(0, 16, 16), 1));
		// AddChild(new Chunk(new Vector3I(16, 0, 16), 1));
		// AddChild(new Chunk(new Vector3I(16, 16, 16), 1));
		// AddChild(new Chunk(new Vector3I(32, 0, 0), 1));
	}
}
