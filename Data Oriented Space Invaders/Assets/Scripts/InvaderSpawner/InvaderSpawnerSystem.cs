using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct InvaderSpawnerSystem : ISystem
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
		new InvaderSpawnerJob()
		{
			ElapsedTime = elapsedTime,
			EntityCommandBuffer = entityCommandBufferSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
		}.ScheduleParallel();
    }
}