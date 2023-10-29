using Unity.Entities;
using UnityEngine;

class InvaderAuthoring : MonoBehaviour
{
    public float Speed = 3f;
    public float Width = 20f;
}

class InvaderBaker : Baker<InvaderAuthoring>
{
    public override void Bake(InvaderAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Invader
        {
            Speed = authoring.Speed,
            Width = authoring.Width
        });
    }
}