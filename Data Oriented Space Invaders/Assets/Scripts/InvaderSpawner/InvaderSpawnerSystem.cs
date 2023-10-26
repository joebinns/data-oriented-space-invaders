using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public partial struct InvaderSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Execute.InvaderSpawning>();
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
        var tanks = new NativeArray<Entity>(invaderSpawner.Count, Allocator.Temp);
        ecb.Instantiate(invaderSpawner.Prefab, tanks);

        ecb.Playback(state.EntityManager);
    }
}