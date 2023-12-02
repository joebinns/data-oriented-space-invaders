using UnityEngine;
using Unity.Entities;

class ProjectileSpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnPeriod = 0.5f;
}

class SpawnerBaker : Baker<ProjectileSpawnerAuthoring>
{
    public override void Bake(ProjectileSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new ProjectileSpawner
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            NextSpawnTime = 0.0f,
            SpawnPeriod = authoring.SpawnPeriod
        });
    }
}