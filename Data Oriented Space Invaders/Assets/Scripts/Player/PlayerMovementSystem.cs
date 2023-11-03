using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        
        foreach (var (transform, player) in
                 SystemAPI.Query<RefRW<LocalTransform>,
                     RefRW<Player>>())
        {
            ProcessPlayerMovement(transform, player.ValueRO.Speed, player.ValueRO.Width, deltaTime);
        }
    }
    
    private void ProcessPlayerMovement(RefRW<LocalTransform> transform, float speed, float width, float deltaTime)
    { 
        var input = Input.GetAxis("Horizontal");
        if (input == 0f)
        {
            return;
        }
        
        var velocity = input * speed * new float3(1f, 0f, 0f);
        var deltaPosition = velocity * Time.deltaTime;
        var newPosition = transform.ValueRO.Position + deltaPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -width/2f, width/2f);
        transform.ValueRW.Position = newPosition;
    }
}