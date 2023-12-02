using Unity.Entities;

public readonly partial struct InvaderSpawnerAspect : IAspect
{
	public readonly Entity Entity;

	private readonly RefRW<InvaderSpawner> _invaderSpawner;

	public int Waves => _invaderSpawner.ValueRO.Waves;
	public int Count => _invaderSpawner.ValueRO.Count;
	public float Spacing => _invaderSpawner.ValueRO.Spacing;
	public float Height => _invaderSpawner.ValueRO.Height;
	public float SpawnPeriod => _invaderSpawner.ValueRO.SpawnPeriod;
	public Entity Prefab => _invaderSpawner.ValueRO.Prefab;


	public float NextSpawnTime
	{
		get => _invaderSpawner.ValueRO.NextSpawnTime;
		set => _invaderSpawner.ValueRW.NextSpawnTime = value;
	}

	public int CurrentWave
	{
		get => _invaderSpawner.ValueRO.CurrentWave;
		set => _invaderSpawner.ValueRW.CurrentWave = value;
	}
}
