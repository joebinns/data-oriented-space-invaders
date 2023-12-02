using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct PlayerAspect : IAspect
{
	public readonly Entity Entity;

	private readonly RefRW<Player> _player;
	private readonly RefRW<LocalTransform> _transform;

	public float Speed => _player.ValueRO.Speed;
	public float Width => _player.ValueRO.Width;

	public float3 Position
	{
		get => _transform.ValueRO.Position;
		set => _transform.ValueRW.Position = value;
	}
}
