using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
public partial struct CollisionSystem : ISystem
{
	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
		var playerSize = 1f;

		foreach (var playerAspect in SystemAPI.Query<PlayerAspect>())
		{
			foreach (var projectileAspect in SystemAPI.Query<ProjectileAspect>())
			{
				var squareDistance = math.distancesq(playerAspect.Position, projectileAspect.Position);
				if (squareDistance <= (playerSize * playerSize))
				{
					entityCommandBuffer.DestroyEntity(projectileAspect.Entity);
				}
			}
		}

		entityCommandBuffer.Playback(state.EntityManager);
		entityCommandBuffer.Dispose();
	}
}
