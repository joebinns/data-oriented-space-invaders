using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct InvaderAspect : IAspect
{
	public readonly Entity Entity;

	private readonly RefRW<Invader> _invader;
	private readonly RefRW<LocalTransform> _transform;

	public float3 Position
	{
		get => _transform.ValueRO.Position;
		set => _transform.ValueRW.Position = value;
	}
}
