using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct InvaderSpawnerSystem : ISystem
{
	private EntityQuery _invadersQuery;

	[BurstCompile]
	public void OnCreate(ref SystemState state)
	{
		state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();

		_invadersQuery = state.GetEntityQuery(typeof(Invader));
	}

	[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
		state.CompleteDependency();

		// Schedule invader spawner job
		var numInvaders = _invadersQuery.CalculateEntityCount();
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