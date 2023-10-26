using UnityEngine;
using Unity.Entities;

class ProjectileSpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public Transform SpawnTransform;
    public float SpawnRate;
}

class SpawnerBaker : Baker<ProjectileSpawnerAuthoring>
{
    public override void Bake(ProjectileSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new ProjectileSpawner
        {
            // By default, each authoring GameObject turns into an Entity.
            // Given a GameObject (or authoring component), GetEntity looks up the resulting Entity.
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            SpawnTransform = GetEntity(authoring.SpawnTransform, TransformUsageFlags.Dynamic),
            NextSpawnTime = 0.0f,
            SpawnRate = authoring.SpawnRate
        });
    }
}