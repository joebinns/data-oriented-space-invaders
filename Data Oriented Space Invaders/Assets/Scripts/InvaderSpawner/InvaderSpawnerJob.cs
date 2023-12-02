using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct InvaderSpawnerJob : IJobEntity
{
	public bool ShouldResetWaves;
	public float ElapsedTime;
	public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

	[BurstCompile]
	private void Execute(InvaderSpawnerAspect spawnerAspect, [EntityIndexInQuery] int sortKey)
	{
		if (spawnerAspect.NextSpawnTime > ElapsedTime) return;
		if (ShouldResetWaves)
		{
			spawnerAspect.CurrentWave = 0;
		}
		if (spawnerAspect.CurrentWave >= spawnerAspect.Waves) return;

		// Spawn invaders with a vertical height and horizontal spacing
		var spawnHorizontal = (spawnerAspect.Spacing / 2f) * (1f - spawnerAspect.Count);
		var spawnVertical = (spawnerAspect.Height - spawnerAspect.CurrentWave * spawnerAspect.Spacing);
		for (var i = 0; i < spawnerAspect.Count; i++)
		{
			spawnHorizontal += spawnerAspect.Spacing;
			var invader = EntityCommandBuffer.Instantiate(sortKey, spawnerAspect.Prefab);
			var invaderTransform = LocalTransform.FromPosition(new float3(spawnHorizontal, spawnVertical, 0f));
			EntityCommandBuffer.SetComponent(sortKey, invader, invaderTransform);
		}

		// Reset the next spawn time and update the number of remaining waves
		spawnerAspect.NextSpawnTime = ElapsedTime + spawnerAspect.SpawnPeriod;
		spawnerAspect.CurrentWave++;
	}
}