using Unity.Entities;

public struct ProjectileSpawner : IComponentData
{
    public Entity Prefab;
    public float NextSpawnTime;
    public float SpawnPeriod;
}
