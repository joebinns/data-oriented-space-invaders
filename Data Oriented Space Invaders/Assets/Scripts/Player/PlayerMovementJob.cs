using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct PlayerMovementJob : IJobEntity
{
	public float Input;
	public float DeltaTime;

	[BurstCompile]
	private void Execute(PlayerAspect playerAspect, [EntityIndexInQuery] int sortKey)
	{
		if (Input == 0f) return;

		var velocity = Input * playerAspect.Speed * new float3(1f, 0f, 0f);
		var deltaPosition = velocity * DeltaTime;
		var newPosition = playerAspect.Position + deltaPosition;
		newPosition.x = Mathf.Clamp(newPosition.x, - playerAspect.Width / 2f, playerAspect.Width / 2f);
		playerAspect.Position = newPosition;
	}
}