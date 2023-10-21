using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms; 

public partial struct InvaderMovementSystem : ISystem
{
    private int _dir;
    
    public void OnCreate(ref SystemState state)
    {
        _dir = 1;
    }

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

        // When the horizontal position exceeds a certain value, head in the opposite direction
        if (pos.x <= -10f)
        {
            _dir = 1;
        }
        else if (pos.x >= 10f)
        {
            _dir = -1;
        }
        
        var dir = new float3(1f * _dir, 0f, 0f);
        var velocity = 5.0f * dir;

        transform.ValueRW.Position += velocity * dt;
    }
}