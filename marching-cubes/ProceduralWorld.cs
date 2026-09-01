using System;
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
	
	private readonly FastNoiseLite _mediumNoise = new()
	{
		Frequency = 0.01f,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
		Seed = seed
	};
	
	private readonly FastNoiseLite _sparseNoise = new()
	{
		Frequency = 0.001f,
		FractalOctaves = 1,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
		Seed = seed
	};

	// Using noise means that the volume near the surface of the SDF becomes more distorted
	// It's not so easy to use it to simply move vertices up and down
	private float GetNoiseDisplacement(Vector3 position)
	{
		var baseDisplacement = _baseNoise.GetNoise3Dv(position);
		
		var e = 1f * _baseNoise.GetNoise3Dv(position) 
		        + 0.5f * _baseNoise.GetNoise3Dv(position * 2f) 
		        + 0.25f * _baseNoise.GetNoise3Dv(position * 4);
		
		e = e / (1f + 0.5f + 0.25f);
		return -(float)Math.Pow(e, 2) * 50;
	}
	
	// Signed distance function of our planet centered at (0, 0, 0)
	private static float PlanetSdf(Vector3 position)
	{
		return PlanetCenter.DistanceTo(position) - PlanetRadius;
	}

	public float GetDisplacement(Vector3 position)
	{
		return PlanetSdf(position) + GetNoiseDisplacement(position);
	}
}
