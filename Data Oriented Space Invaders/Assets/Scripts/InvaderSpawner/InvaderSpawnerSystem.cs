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
		var invadersQuery = state.GetEntityQuery(typeof(Invader));
		var numInvaders = invadersQuery.CalculateEntityCount();

		state.CompleteDependency();

		// Schedule invader spawner job
		var shouldResetWaves = numInvaders == 0;
		var deltaTime = SystemAPI.Time.DeltaTime;
		var elapsedTime = (float)SystemAPI.Time.ElapsedTime;
		var entityCommandBufferSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
		new InvaderSpawnerJob()
		{
			DeltaTime = deltaTime,
			ShouldResetWaves = shouldResetWaves,
			ElapsedTime = elapsedTime,
			EntityCommandBuffer = entityCommandBufferSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
		}.ScheduleParallel();
    }
}