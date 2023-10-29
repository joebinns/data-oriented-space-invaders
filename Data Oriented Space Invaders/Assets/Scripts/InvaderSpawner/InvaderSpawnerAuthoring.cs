using UnityEngine;
using Unity.Entities;

class InvaderSpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public int Count;
    public float Spacing;
    public float Height;
}

class InvaderSpawnerBaker : Baker<InvaderSpawnerAuthoring>
{
    public override void Bake(InvaderSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new InvaderSpawner
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            Count = authoring.Count,
            Spacing = authoring.Spacing,
            Height = authoring.Height
        });
    }
}