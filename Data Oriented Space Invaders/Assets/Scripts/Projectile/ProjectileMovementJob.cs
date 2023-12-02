using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct ProjectileMovementJob : IJobEntity
{
	public float DeltaTime;
	public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

	[BurstCompile]
	private void Execute(ProjectileAspect projectileAspect, [EntityIndexInQuery] int sortKey)
	{
		// Process projectile movement
		var deltaVerticalDisplacement = projectileAspect.VerticalVelocity * DeltaTime;
		projectileAspect.Position += deltaVerticalDisplacement * new float3(0f, 1f, 0f);

		// Destroy entities that have left the arena
		projectileAspect.DistanceTravelled += Mathf.Abs(deltaVerticalDisplacement);
		if (projectileAspect.DistanceTravelled > projectileAspect.DestroyAtDistance)
		{
			EntityCommandBuffer.DestroyEntity(sortKey, projectileAspect.Entity);
		}
	}
}