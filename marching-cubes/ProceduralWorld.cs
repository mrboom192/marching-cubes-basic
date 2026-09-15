using System;
using Godot;

namespace marchingcubesbasic.examples;

[Tool]
// Handles world generation
public partial class ProceduralWorld(int seed) : Node
{
	// private const int PlanetRadius = 6_371_000;
	private const int PlanetRadius = 100;
	private static readonly Vector3 PlanetCenter = new(0, -PlanetRadius, 0);

	// Position, radius
	private static readonly (Vector3, float)[] Craters =
	[
		(new Vector3(5f, 5f, 5f), 50f)
	];
	
	private readonly FastNoiseLite _baseNoise = new()
	{
		Frequency = PlanetRadius,
		NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
		Seed = seed
	};

	public Vector3 GetPlanetOrigin()
	{
		return PlanetCenter;
	}

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
	
	private static float GetCraterDisplacement(Vector3 position)
	{
		(Vector3, float) crater = Craters[0];
		
		var distance = crater.Item1.DistanceTo(position);

		if (distance > crater.Item2)
			return 0f;

		return crater.Item2 - distance;
	}

	/// <summary>
	/// Calculates the signed distance from the implicit surface.
	/// Values inside the surface are negative, while values outside are positive.
	/// </summary>
	/// <param name="position">The position to evaluate.</param>
	/// <returns>The signed distance from the implicit surface.</returns>
	public float GetSignedDistance(Vector3 position)
	{
		return PlanetSdf(position) + GetNoiseDisplacement(position) + GetCraterDisplacement(position);
	}

	/// <summary>
	/// Calculates the surface point closest to the given position.
	/// Currently relies on the SDF to be correct
	/// </summary>
	/// <param name="position">The position vector.</param>
	/// <returns>A point on the surface</returns>
	public Vector3 GetNearestSurfacePosition(Vector3 position)
	{
		var gradient = GetGradient(position, 10.0f);
		var distance = GetSignedDistance(position);
		
		return position - gradient * distance;
	}

	/// <summary>
	/// Returns a Vector3 representing the normalized gradient at that point,
	/// calculated using central finite difference.
	/// </summary>
	/// <param name="position">The position vector.</param>
	/// <param name="h">The spacing amount.</param>
	/// <returns>The gradient of the scalar field at position.</returns>
	public Vector3 GetGradient(Vector3 position, float h)
	{
		var hx = new Vector3(h, 0, 0);
		var dx = GetSignedDistance(position + hx) - GetSignedDistance(position - hx);

		var hy = new Vector3(0, h, 0);
		var dy = GetSignedDistance(position + hy) - GetSignedDistance(position - hy);

		var hz = new Vector3(0, 0, h);
		var dz = GetSignedDistance(position + hz) - GetSignedDistance(position - hz);

		return new Vector3(dx, dy, dz).Normalized();
	}
}
