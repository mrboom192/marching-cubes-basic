using Godot;

namespace marchingcubesbasic.examples;

[Tool]
// Handles world generation
public partial class ProceduralWorld(int seed) : Node
{
	private const int PlanetRadius = 6_371_000;
	private static readonly Vector3 PlanetCenter = new(0, -PlanetRadius, 0);
	
	private readonly FastNoiseLite _baseNoise = new()
	{
		Frequency = 0.1f,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
		Seed = seed
	};
	
	private readonly FastNoiseLite _sparseNoise = new()
	{
		Frequency = 0.001f,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
		Seed = seed
	};

	private float GetNoiseDisplacement(Vector3 position)
	{
		var baseDisplacement = _baseNoise.GetNoise3Dv(position);
		var hillDisplacement = Mathf.Clamp(baseDisplacement - 0.5f, 0, 1) * 10;
		var mountainDisplacement = Mathf.Clamp(_sparseNoise.GetNoise3Dv(position) - 0.5f, 0, 1) * 10000;
		
		return baseDisplacement + hillDisplacement + mountainDisplacement;
	}
	
	// Signed distance function of our planet centered at (0, 0, 0)
	private static float PlanetSdf(Vector3 position)
	{
		return PlanetCenter.DistanceTo(position) - PlanetRadius;
	}

	public float GetDisplacement(Vector3 position)
	{
		return PlanetSdf(position) - GetNoiseDisplacement(position);
	}
}
