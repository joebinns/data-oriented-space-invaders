using Unity.Entities;

public struct InvaderSpawner : IComponentData
{
    public Entity Prefab;
    public int Count;
    public float Spacing;
    public float Height;
    public float NextSpawnTime;
    public float SpawnPeriod;
    public int Waves;
    public int CurrentWave;
}
