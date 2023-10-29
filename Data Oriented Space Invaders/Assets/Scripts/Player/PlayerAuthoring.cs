using UnityEngine;
using Unity.Entities;

class PlayerAuthoring : MonoBehaviour
{
    public float Speed = 4f;
    public float Width = 20f;
}

class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Player
        {
            Speed = authoring.Speed,
            Width = authoring.Width
        });
    }
}