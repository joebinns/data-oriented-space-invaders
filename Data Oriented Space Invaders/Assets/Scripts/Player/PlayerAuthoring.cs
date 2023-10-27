using UnityEngine;
using Unity.Entities;

class PlayerAuthoring : MonoBehaviour
{
}

class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Player
        {
            
        });
    }
}