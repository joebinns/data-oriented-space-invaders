using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct PlayerMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var dt = SystemAPI.Time.DeltaTime;
        
        foreach (var (transform, entity) in
                 SystemAPI.Query<RefRW<LocalTransform>>()
                     .WithAll<Player>()
                     .WithEntityAccess())
        {
            ProcessPlayerMovement(transform, entity, dt);
        }
    }
    private void ProcessPlayerMovement(RefRW<LocalTransform> transform, Entity entity, float dt)
    { 
        var input = Input.GetAxis("Horizontal");
        if (input == 0f)
        {
            return;
        }

        var speed = input * 4f;
        var velocity = speed * new float3(1f, 0f, 0f);
        var deltaPosition = velocity * Time.deltaTime;
        var newPosition = transform.ValueRO.Position + deltaPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -GameSettings.Instance.Width, GameSettings.Instance.Width);
        transform.ValueRW.Position = newPosition;
    }
}