using Unity.Entities;
using Unity.Burst;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
[BurstCompile]
public partial class PlayerShootingSystem : SystemBase
{
	private Entity _playerSingleton;

	[BurstCompile]
	protected override void OnCreate()
	{
		RequireForUpdate<PlayerAspect>();
	}

	[BurstCompile]
	protected override void OnStartRunning()
	{
		SystemAPI.TryGetSingletonEntity<PlayerAspect>(out _playerSingleton);
	}

	[BurstCompile]
	protected override void OnUpdate()
	{
		if (!SystemAPI.Exists(_playerSingleton)) return;

		var input = Input.GetAxis("Fire") != 0f;
		SystemAPI.SetComponentEnabled<PlayerShooting>(_playerSingleton, input);
	}
}
