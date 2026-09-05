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
	// values dig out terrain. Noise values are generated in the range of [-1, 1].
	// TODO Add in biomes, make frequency proportional to the radius of the planet
	private float GetNoiseDisplacement(Vector3 position)
	{
		var direction = (position - PlanetCenter) / PlanetCenter.DistanceTo(position);
		
		// Mountains
		const float mountainWeight = 0.00139f;
		const float mountainFrequency = 0.0001f;
		var mountainNoise = _baseNoise.GetNoise3Dv(direction * mountainFrequency);
		mountainNoise *= mountainNoise; // Values from 0-1
		mountainNoise *= PlanetRadius * mountainWeight;
		
		// Hills
		const float hillWeight = 0.0000314f;
		const float hillFrequency = 0.001f;
		var hillNoise = _baseNoise.GetNoise3Dv(direction * hillFrequency);
		hillNoise *= hillNoise;
		hillNoise *= PlanetRadius * hillWeight;
		
		// Bumps
		const float detailWeight = 0.0000005f;
		const float bumpFrequency = 0.1f;
		var bumpNoise = _baseNoise.GetNoise3Dv(direction * bumpFrequency);
		bumpNoise *= PlanetRadius * detailWeight;

		var displacement = -mountainNoise + -hillNoise + bumpNoise;
		
		return displacement;
	}
	
	// Signed distance function of our planet centered at (0, 0, 0)
	private static float PlanetSdf(Vector3 position)
	{
		return PlanetCenter.DistanceTo(position) - PlanetRadius;
	}
	
	private static float Crater(Vector3 position)
	{
		var craterPosition = new Vector3(5f, 5f, 5f);
		const float craterRadius = 100f;

		var distance = craterPosition.DistanceTo(position);

		if (distance > craterRadius)
			return 0f;

		return craterRadius - distance;
	}

	public float GetDisplacement(Vector3 position)
	{
		return PlanetSdf(position) + GetNoiseDisplacement(position) + Crater(position);
	}
}
