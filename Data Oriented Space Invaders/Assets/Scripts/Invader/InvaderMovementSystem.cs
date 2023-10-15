using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct InvaderMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var dt = SystemAPI.Time.DeltaTime;
        
        foreach (var (transform, entity) in
                 SystemAPI.Query<RefRW<LocalTransform>>()
                     .WithAll<Invader>()
                     .WithEntityAccess())
        {
            ProcessInvaderMovement(transform, entity, dt);
        }
    }

    private void ProcessInvaderMovement(RefRW<LocalTransform> transform, Entity entity, float dt)
    {
        var pos = transform.ValueRO.Position;
        
        pos.y = (float)entity.Index;

        var dir = new float3(1f, 0f, 0f);

        transform.ValueRW.Position += dir * dt * 5.0f;
    }
}