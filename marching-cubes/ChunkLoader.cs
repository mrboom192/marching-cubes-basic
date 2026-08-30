using System.Collections.Concurrent;
using Godot;

namespace marchingcubesbasic.examples;

public readonly record struct ChunkMeshData(
    Aabb Bounds,
    Vector3[] Vertices,
    int[] Indices
);

public class ChunkLoader
{
    private readonly ConcurrentQueue<Aabb> _requests = new();
    private readonly ConcurrentQueue<ChunkMeshData> _completed = new();
}