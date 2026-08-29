using Godot;

namespace marchingcubesbasic.examples;

[Tool]
// Handles world generation
public partial class ProceduralWorld : Node
{
	private const int PlanetRadius = 6_371_000;
	private static readonly Vector3 PlanetCenter = new(0, -PlanetRadius, 0);
	
	private static readonly FastNoiseLite HillNoise = new()
	{
		Frequency = 0.1f,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
	};
	
	private static readonly FastNoiseLite MountainNoise = new()
	{
		Frequency = 0.01f,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
		FractalOctaves = 1
	};

	private float GetNoiseDisplacement(Vector3 position)
	{
		return HillNoise.GetNoise3Dv(position) + MountainNoise.GetNoise3Dv(position) * 20;
	}
	
	// Signed distance function of our planet centered at (0, 0, 0)
	private float PlanetSdf(Vector3 position)
	{
		return PlanetCenter.DistanceTo(position) - PlanetRadius;
	}

	public float GetDisplacement(Vector3 position)
	{
		return PlanetSdf(position) - GetNoiseDisplacement(position);
	}
}
