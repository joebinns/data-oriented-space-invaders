using System;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ProjectileMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var dt = SystemAPI.Time.DeltaTime;

        var ecb = new EntityCommandBuffer(Allocator.Temp);
        
        foreach (var (transform, projectile, entity) in
                 SystemAPI.Query<RefRW<LocalTransform>,
                     RefRW<Projectile>>().WithEntityAccess())
        {
            var deltaDisplacement = ProcessProjectileMovement(transform, projectile, dt);
            
            projectile.ValueRW.DistanceTravelled += Math.Abs(deltaDisplacement);
            if (projectile.ValueRO.DistanceTravelled > 12f)
            {
                ecb.DestroyEntity(entity);
            }
        }
        
        ecb.Playback(state.EntityManager);
    }
    private float ProcessProjectileMovement(RefRW<LocalTransform> transform, RefRW<Projectile> projectile, float dt)
    {
        var velocity = projectile.ValueRO.VerticalVelocity;
        var deltaDisplacement = velocity * dt;
        transform.ValueRW.Position += deltaDisplacement * new float3(0f, 1f, 0f);
        return deltaDisplacement;
    }
}