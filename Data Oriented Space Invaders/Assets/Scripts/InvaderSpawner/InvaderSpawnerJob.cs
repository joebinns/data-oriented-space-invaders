using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct InvaderSpawnerJob : IJobEntity
{
	public float ElapsedTime;
	public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

	[BurstCompile]
	private void Execute(InvaderSpawner spawner, [EntityIndexInQuery] int sortKey)
	{
		if (spawner.NextSpawnTime > ElapsedTime) return;
		if (spawner.CurrentWave >= spawner.Waves) return;

		// Spawn invaders with a vertical height and horizontal spacing
		var spawnHorizontal = (spawner.Spacing / 2f) * (1f - spawner.Count);
		var spawnVertical = (spawner.Height - spawner.CurrentWave * spawner.Spacing);
		for (var i = 0; i < spawner.Count; i++)
		{
			spawnHorizontal += spawner.Spacing;
			var invader = EntityCommandBuffer.Instantiate(sortKey, spawner.Prefab);
			var invaderTransform = LocalTransform.FromPosition(new float3(spawnHorizontal, spawnVertical, 0f));
			EntityCommandBuffer.SetComponent(sortKey, invader, invaderTransform);
		}

		// Reset the next spawn time and update the number of remaining waves
		spawner.NextSpawnTime = ElapsedTime + spawner.SpawnPeriod;
		spawner.CurrentWave++;
	}
}