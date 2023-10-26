using Unity.Entities;
using Unity.Mathematics;

public struct InvaderSpawner : IComponentData
{
    public Entity Prefab;
    public int Count;
    public float SafeZoneRadius;
}
