using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

[BurstCompile]
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct ProjectileSpawnerSystem : ISystem
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

		// Schedule invader spawner job
		var elapsedTime = (float)SystemAPI.Time.ElapsedTime;
		var entityCommandBufferSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
		new ProjectileSpawnerJob()
		{
			ElapsedTime = elapsedTime,
			EntityCommandBuffer = entityCommandBufferSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
		}.ScheduleParallel();
    }
}