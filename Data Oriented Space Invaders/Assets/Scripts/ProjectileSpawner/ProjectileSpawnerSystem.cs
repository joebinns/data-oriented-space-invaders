using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

[BurstCompile]
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct ProjectileSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<ProjectileSpawner> projectileSpawner in SystemAPI.Query<RefRW<ProjectileSpawner>>())
        {
            ProcessSpawner(ref state, projectileSpawner);
        }
    }

    [BurstCompile]
    private void ProcessSpawner(ref SystemState state, RefRW<ProjectileSpawner> spawner)
    {
        if (!(spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)) return;
        // Reset the next spawn time
        spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnPeriod;
        
        // Spawn projectile
        Entity projectile = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
        state.EntityManager.SetComponentData(projectile, LocalTransform.FromPosition
        (
            SystemAPI.GetComponent<LocalTransform>(spawner.ValueRO.SpawnTransform).Position
        ));
    }
}