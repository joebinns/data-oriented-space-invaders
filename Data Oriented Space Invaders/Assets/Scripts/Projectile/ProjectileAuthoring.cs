using UnityEngine;
using Unity.Entities;

class ProjectileAuthoring : MonoBehaviour
{
    public float VerticalVelocity;
}

class ProjectileBaker : Baker<ProjectileAuthoring>
{
    public override void Bake(ProjectileAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Projectile
        {
            VerticalVelocity = authoring.VerticalVelocity,
            DistanceTravelled = 0f
        });
    }
}