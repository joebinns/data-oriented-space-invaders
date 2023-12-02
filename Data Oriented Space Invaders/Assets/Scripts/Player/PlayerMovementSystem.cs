using Unity.Entities;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public partial struct PlayerMovementSystem : ISystem
{
	[BurstCompile]
	public void OnCreate(ref SystemState state)
	{
		state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
	}

	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		state.CompleteDependency();

		// Schedule player movement job
		var input = Input.GetAxis("Horizontal");
		var deltaTime = SystemAPI.Time.DeltaTime;
		new PlayerMovementJob()
		{
			Input = input,
			DeltaTime = deltaTime
		}.ScheduleParallel();
	}
}
