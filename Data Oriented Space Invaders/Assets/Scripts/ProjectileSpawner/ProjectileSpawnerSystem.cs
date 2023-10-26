using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Rendering;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct ProjectileSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Queries for all Spawner components. Uses RefRW because this system wants
        // to read from and write to the component. If the system only needed read-only
        // access, it would use RefRO instead.
        foreach (RefRW<ProjectileSpawner> projectileSpawner in SystemAPI.Query<RefRW<ProjectileSpawner>>())
        {
            ProcessSpawner(ref state, projectileSpawner);
        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<ProjectileSpawner> spawner)
    {
        // If the next spawn time has passed.
        if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            Entity projectile = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);

            state.EntityManager.SetComponentData(projectile, LocalTransform.FromPosition
            (
                SystemAPI.GetComponent<LocalTransform>(spawner.ValueRO.SpawnTransform).Position
            ));

                // Resets the next spawn time.
            spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
        }
    }
}