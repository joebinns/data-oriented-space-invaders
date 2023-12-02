using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct ProjectileAspect : IAspect
{
	public readonly Entity Entity;

	private readonly RefRW<Projectile> _projectile;
	private readonly RefRW<LocalTransform> _transform;

	public float VerticalVelocity => _projectile.ValueRO.VerticalVelocity;
	public float DestroyAtDistance => _projectile.ValueRO.DestroyAtDistance;

	public float DistanceTravelled
	{
		get => _projectile.ValueRO.DistanceTravelled;
		set => _projectile.ValueRW.DistanceTravelled = value;
	}

	public float3 Position
	{
		get => _transform.ValueRO.Position;
		set => _transform.ValueRW.Position = value;
	}
}
