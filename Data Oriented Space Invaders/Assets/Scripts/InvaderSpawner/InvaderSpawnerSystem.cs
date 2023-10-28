using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public partial struct InvaderSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        //state.RequireForUpdate<Execute.InvaderSpawner>(); // Not sure what this is supposed to do
        state.RequireForUpdate<InvaderSpawner>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false; // Only run update once

        var invaderSpawner = SystemAPI.GetSingleton<InvaderSpawner>();
        // This system will only run once, so the random seed can be hard-coded.
        // Using an arbitrary constant seed makes the behavior deterministic.
        var random = new Random(123);

        var query = SystemAPI.QueryBuilder().WithAll<URPMaterialPropertyBaseColor>().Build();
        // An EntityQueryMask provides an efficient test of whether a specific entity would
        // be selected by an EntityQuery.
        var queryMask = query.GetEntityQueryMask();

        var ecb = new EntityCommandBuffer(Allocator.Temp);
        var invaders = new NativeArray<Entity>(invaderSpawner.Count, Allocator.Temp);
        ecb.Instantiate(invaderSpawner.Prefab, invaders);
        
        // Set positions
        var spawnDistance = 2f;
        var spawnVertical = 10f;
        
        var spawnHorizontal = (spawnDistance / 2f) * (1f - invaders.Length);
        foreach (var invader in invaders)
        {
            // Every root entity instantiated from a prefab has a LinkedEntityGroup component, which
            // is a list of all the entities that make up the prefab hierarchy.
            ecb.ReplaceComponentForLinkedEntityGroup(invader, LocalTransform.FromPosition(new float3(spawnHorizontal, spawnVertical, 0f)));
            spawnHorizontal += spawnDistance;
        }

        ecb.Playback(state.EntityManager);
    }
}