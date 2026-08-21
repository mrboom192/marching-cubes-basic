using Godot;

namespace marchingcubesbasic.examples;

public partial class Example : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddChild(new Chunk(Vector3I.Zero, 1));
		AddChild(new Chunk(new Vector3I(0, 16, 0), 1));
		AddChild(new Chunk(new Vector3I(16, 0, 0), 1));
		AddChild(new Chunk(new Vector3I(0, 0, 16), 1));
		AddChild(new Chunk(new Vector3I(16, 16, 0), 1));
		AddChild(new Chunk(new Vector3I(0, 16, 16), 1));
		AddChild(new Chunk(new Vector3I(16, 0, 16), 1));
		AddChild(new Chunk(new Vector3I(16, 16, 16), 1));
		AddChild(new Chunk(new Vector3I(32, 0, 0), 1));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print("RAN3");
	}
}