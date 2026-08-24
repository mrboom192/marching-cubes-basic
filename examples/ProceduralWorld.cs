using Godot;

namespace marchingcubesbasic.examples;

public partial class ProceduralWorld : Node
{
    private readonly int _planetRadius = 3;
    
    // Signed distance function of our planet centered at (0, 0, 0)
    public float PlanetSdf(Vector3 position)
    {
        return position.Length() - _planetRadius;
    }

    // Return a plane 0.2 meters above 0.
    public float PlaneSdf(Vector3 position)
    {
        return position.Y - 0.02f;
    }
}