using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
public partial struct InvaderSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InvaderSpawner>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false; // Only run update once

        var invaderSpawner = SystemAPI.GetSingleton<InvaderSpawner>();
        var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
        var invaders = new NativeArray<Entity>(invaderSpawner.Count, Allocator.Temp);
        
        // Spawn invaders with a vertical height and horizontal spacing
        entityCommandBuffer.Instantiate(invaderSpawner.Prefab, invaders);
        var spawnHorizontal = (invaderSpawner.Spacing / 2f) * (1f - invaders.Length);
        foreach (var invader in invaders)
        {
            entityCommandBuffer.ReplaceComponentForLinkedEntityGroup(invader, LocalTransform.FromPosition(new float3(spawnHorizontal, invaderSpawner.Height, 0f)));
            spawnHorizontal += invaderSpawner.Spacing;
        }

        entityCommandBuffer.Playback(state.EntityManager);
    }
}