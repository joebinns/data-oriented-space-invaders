using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct InvaderMovementSystem : ISystem
{
    private int _direction;
    
    public void OnCreate(ref SystemState state)
    {
        _direction = 1;
    }
    
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, invader) in
                 SystemAPI.Query<RefRW<LocalTransform>,
                     RefRW<Invader>>())
        {
            if (invader.ValueRO.StartTime > SystemAPI.Time.ElapsedTime) return;
            UpdateInvadersDirection(transform, invader.ValueRO.Width);
            ApplyInvaderMovement(transform, invader.ValueRO.Speed, deltaTime);
        }
    }
    
    /// <summary>
    /// When the horizontal position exceeds a certain value,
    /// reverse direction of all invaders.
    /// </summary>
    private void UpdateInvadersDirection(RefRW<LocalTransform> transform, float width)
    {
        var directionChanged = false;
        if (transform.ValueRO.Position.x <= -width/2f)
        {
            _direction = 1;
        }
        else if (transform.ValueRO.Position.x >= width/2f)
        {
            _direction = -1;
        }
    }
    
    private void ApplyInvaderMovement(RefRW<LocalTransform> transform, float speed, float deltaTime)
    {
        var direction = new float3(1f * _direction, 0f, 0f);
        var velocity = speed * direction;
        var deltaPosition = velocity * deltaTime;
        transform.ValueRW.Position += deltaPosition;
    }
}