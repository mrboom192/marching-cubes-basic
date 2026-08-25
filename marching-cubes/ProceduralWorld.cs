using Godot;

namespace marchingcubesbasic.examples;

[Tool]
// Handles world generation
public partial class ProceduralWorld : Node
{
	private readonly int _planetRadius = 3;
	private static FastNoiseLite _noise = new FastNoiseLite
	{
		Frequency = 0.05f
	};
	
	// Signed distance function of our planet centered at (0, 0, 0)
	public float PlanetSdf(Vector3 position)
	{
		return position.Length() - _planetRadius;
	}

	// Return a plane 0.2 meters above 0 with some added noise. Update function to be more accurately named
	public float PlaneSdf(Vector3 position)
	{
		return position.Y - 0.02f - (_noise.GetNoise3Dv(position) * 6);
	}
}
