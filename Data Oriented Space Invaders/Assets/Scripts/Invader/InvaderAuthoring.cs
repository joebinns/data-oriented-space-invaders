using Unity.Entities;
using UnityEngine;

class InvaderAuthoring : MonoBehaviour
{
    [HideInInspector] public float Width;
    
    private void Awake()
    {
        Width = GameSettings.Instance.Width;
    }
}

class InvaderBaker : Baker<InvaderAuthoring>
{
    public override void Bake(InvaderAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Invader
        {
            Width = authoring.Width
        });
    }
}