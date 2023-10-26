using Unity.Entities;

public struct ProjectileSpawner : IComponentData
{
    public Entity Prefab;
    public Entity SpawnTransform;
    public float NextSpawnTime;
    public float SpawnRate;
}
