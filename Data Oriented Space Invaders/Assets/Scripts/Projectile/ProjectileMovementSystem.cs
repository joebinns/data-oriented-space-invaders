using Unity.Entities;
using Unity.Burst;
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

        foreach (var (transform, projectile) in
                 SystemAPI.Query<RefRW<LocalTransform>,
                     RefRO<Projectile>>())
        {
            ProcessProjectileMovement(transform, projectile, dt);
        }
    }
    private void ProcessProjectileMovement(RefRW<LocalTransform> transform, RefRO<Projectile> projectile, float dt)
    {
        var velocity = projectile.ValueRO.Velocity;

        transform.ValueRW.Position += velocity * dt;
    }
}