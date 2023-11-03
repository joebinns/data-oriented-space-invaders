using UnityEngine;
using Unity.Entities;

class InvaderSpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public int Count = 6;
    public float Spacing = 2f;
    public float Height = 10f;
    public float SpawnPeriod = 4f;
    public int Waves = 3;
}

class InvaderSpawnerBaker : Baker<InvaderSpawnerAuthoring>
{
    public override void Bake(InvaderSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new InvaderSpawner
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            Count = authoring.Count,
            Spacing = authoring.Spacing,
            Height = authoring.Height,
            NextSpawnTime = 0.0f,
            SpawnPeriod = authoring.SpawnPeriod,
            Waves = authoring.Waves,
            CurrentWave = 0
        });
    }
}