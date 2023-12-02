using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct ProjectileSpawnerJob : IJobEntity
{
	public float ElapsedTime;
	public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

	[BurstCompile]
	private void Execute(ProjectileSpawnerAspect spawnerAspect, [EntityIndexInQuery] int sortKey)
	{
		if (spawnerAspect.NextSpawnTime > ElapsedTime) return;

		// Spawn projectile
		var projectile = EntityCommandBuffer.Instantiate(sortKey, spawnerAspect.Prefab);
		var transform = LocalTransform.FromPosition(spawnerAspect.Position);
		EntityCommandBuffer.SetComponent(sortKey, projectile, transform);

		// Reset the next spawn time
		spawnerAspect.NextSpawnTime = ElapsedTime + spawnerAspect.SpawnPeriod;
	}
}