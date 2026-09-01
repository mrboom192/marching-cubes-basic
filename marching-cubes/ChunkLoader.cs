using System.Collections.Concurrent;
using System.Collections.Generic;
using Godot;

namespace marchingcubesbasic.examples;

public readonly record struct ChunkMeshData(
    MeshInstance3D Mesh,
    NodePath NodePath
);

[Tool]
public partial class ChunkLoader : Node
{
    private readonly ConcurrentQueue<ChunkMeshData> _completed = new();

    public void Enqueue(ChunkMeshData data)
    {
        _completed.Enqueue(data);
    }

    public override void _Process(double delta)
    {
        if (_completed.TryDequeue(out var data))
        {
            var chunk = GetNode<Node>(data.NodePath);
            chunk.AddChild(data.Mesh);
        }
    }
}