using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct ProjectileSpawnerAspect : IAspect
{
	public readonly Entity Entity;

	private readonly RefRW<ProjectileSpawner> _projectileSpawner;
	private readonly RefRW<LocalTransform> _transform;

	public float SpawnPeriod => _projectileSpawner.ValueRO.SpawnPeriod;
	public Entity Prefab => _projectileSpawner.ValueRO.Prefab;

	public float NextSpawnTime
	{
		get => _projectileSpawner.ValueRO.NextSpawnTime;
		set => _projectileSpawner.ValueRW.NextSpawnTime = value;
	}

	public float3 Position
	{
		get => _transform.ValueRO.Position;
		set => _transform.ValueRW.Position = value;
	}
}
