using Unity.Entities;
using Unity.Burst;

[BurstCompile]
public partial struct ProjectileMovementSystem : ISystem
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

		// Schedule projectile movement job
        var deltaTime = SystemAPI.Time.DeltaTime;
		var entityCommandBufferSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
		new ProjectileMovementJob()
		{
			DeltaTime = deltaTime,
			EntityCommandBuffer = entityCommandBufferSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
		}.ScheduleParallel();
    }
}