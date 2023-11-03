using System;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ProjectileMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
        
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, projectile, entity) in
                 SystemAPI.Query<RefRW<LocalTransform>,
                     RefRW<Projectile>>().WithEntityAccess())
        {
            var deltaVerticalDisplacement = ProcessProjectileMovement(transform, projectile, deltaTime);

            // Destroy entities that have left the arena
            projectile.ValueRW.DistanceTravelled += Math.Abs(deltaVerticalDisplacement);
            if (projectile.ValueRO.DistanceTravelled > projectile.ValueRO.DestroyAtDistance)
            {
                entityCommandBuffer.DestroyEntity(entity);
            }
        }
        
        entityCommandBuffer.Playback(state.EntityManager);
    }
    
    [BurstCompile]
    private float ProcessProjectileMovement(RefRW<LocalTransform> transform, RefRW<Projectile> projectile, float deltaTime)
    {
        var deltaVerticalDisplacement = projectile.ValueRO.VerticalVelocity * deltaTime;
        transform.ValueRW.Position += deltaVerticalDisplacement * new float3(0f, 1f, 0f);
        return deltaVerticalDisplacement;
    }
}