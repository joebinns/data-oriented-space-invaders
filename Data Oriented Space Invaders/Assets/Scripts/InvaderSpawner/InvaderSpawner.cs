using Unity.Entities;

public struct InvaderSpawner : IComponentData
{
    public Entity Prefab;
    public int Count;
    public float Spacing;
    public float Height;
}
