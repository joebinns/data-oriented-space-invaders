using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ProjectileSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var dt = SystemAPI.Time.DeltaTime;
        
        foreach (var (transform, entity) in
                 SystemAPI.Query<RefRW<LocalTransform>>()
                     .WithAll<Projectile>()
                     .WithEntityAccess())
        {
            ProcessProjectileMovement(transform, entity, dt);
        }
    }
    private void ProcessProjectileMovement(RefRW<LocalTransform> transform, Entity entity, float dt)
    { 
        var velocity = new float3(0f, -1f, 0f); // projectile.ValueRO.Velocity

        transform.ValueRW.Position += velocity * dt;
    }
}