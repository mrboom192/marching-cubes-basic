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
		Frequency = PlanetRadius,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
		Seed = seed
	};

	// Since the planet surface is implicitly defined by an SDF, negative values raise terrain while positive
	// values dig out terrain. TODO Add in biomes
	public float GetNoiseDisplacement(Vector3 position)
	{
		var direction = (position - PlanetCenter) / PlanetCenter.DistanceTo(position);
		
		// Mountains
		const float mountainWeight = 100;
		const float mountainFrequency = 0.0001f;
		var mountainNoise = mountainWeight * _baseNoise.GetNoise3Dv(direction * mountainFrequency);
		mountainNoise *= mountainNoise;
		
		// Hills
		const float hillWeight = 10;
		const float hillFrequency = 0.01f;
		var hillNoise = hillWeight * _baseNoise.GetNoise3Dv(direction * hillFrequency);
		hillNoise *= hillNoise;
		
		// Bumps
		const float bumpWeight = 1f;
		const float bumpFrequency = 0.1f;
		var bumpNoise = bumpWeight * _baseNoise.GetNoise3Dv(direction * bumpFrequency);

		var displacement = -mountainNoise + -hillNoise + bumpNoise;
		
		return displacement;
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
