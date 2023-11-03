using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct InvaderSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<InvaderSpawner> invaderSpawner in SystemAPI.Query<RefRW<InvaderSpawner>>())
        {
            ProcessSpawner(ref state, invaderSpawner);
        }
    }

    [BurstCompile]
    private void ProcessSpawner(ref SystemState state, RefRW<InvaderSpawner> spawner)
    {
        if (spawner.ValueRO.NextSpawnTime > SystemAPI.Time.ElapsedTime) return;
        if (spawner.ValueRO.CurrentWave >= spawner.ValueRO.Waves) return;

        // Spawn invaders with a vertical height and horizontal spacing
        var spawnHorizontal = (spawner.ValueRO.Spacing / 2f) * (1f - spawner.ValueRO.Count);
        var spawnVertical = (spawner.ValueRO.Height - spawner.ValueRO.CurrentWave * spawner.ValueRO.Spacing);
        for (var i = 0; i < spawner.ValueRO.Count; i++)
        {
            spawnHorizontal += spawner.ValueRO.Spacing;
            Entity invader = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
            state.EntityManager.SetComponentData(invader,
                LocalTransform.FromPosition(new float3(spawnHorizontal, spawnVertical, 0f))
            );
        }
        
        // Reset the next spawn time and update the number of remaining waves
        spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnPeriod;
        spawner.ValueRW.CurrentWave++;
    }
}