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
		foreach (var invaderAspect in SystemAPI.Query<InvaderAspect>())
		{
			foreach (var projectileAspect in SystemAPI.Query<ProjectileAspect>())
			{
				if (projectileAspect.VerticalVelocity < 0f) continue; // Invader projectiles should not collider with other invaders

				var squareDistance = math.distancesq(invaderAspect.Position, projectileAspect.Position);
				if (squareDistance <= (0.75f))
				{
					entityCommandBuffer.DestroyEntity(projectileAspect.Entity);
					entityCommandBuffer.DestroyEntity(invaderAspect.Entity);
				}
			}
		}

		entityCommandBuffer.Playback(state.EntityManager);
		entityCommandBuffer.Dispose();
	}
}
