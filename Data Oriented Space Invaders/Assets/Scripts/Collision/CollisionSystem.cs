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
		var squareDistanceThreshold = 0.75f;

		foreach (var projectileAspect in SystemAPI.Query<ProjectileAspect>())
		{
			foreach (var invaderAspect in SystemAPI.Query<InvaderAspect>())
			{
				if (projectileAspect.VerticalVelocity < 0f) continue; // Invader projectiles should not collider with other invaders

				var squareDistance = math.distancesq(invaderAspect.Position, projectileAspect.Position);
				if (squareDistance <= squareDistanceThreshold)
				{
					entityCommandBuffer.DestroyEntity(projectileAspect.Entity);
					entityCommandBuffer.DestroyEntity(invaderAspect.Entity);
				}
			}

			foreach (var playerAspect in SystemAPI.Query<PlayerAspect>())
			{
				if (projectileAspect.VerticalVelocity > 0f) continue; // Player projectiles should not collider with the player

				var squareDistance = math.distancesq(playerAspect.Position, projectileAspect.Position);
				if (squareDistance <= squareDistanceThreshold)
				{
					entityCommandBuffer.DestroyEntity(projectileAspect.Entity);
				}
			}
		}

		entityCommandBuffer.Playback(state.EntityManager);
		entityCommandBuffer.Dispose();
	}
}
