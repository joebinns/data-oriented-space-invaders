using Unity.Entities;
using UnityEngine;

class InvaderAuthoring : MonoBehaviour
{
}

class InvaderBaker : Baker<InvaderAuthoring>
{
    public override void Bake(InvaderAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<Invader>(entity);
    }
}