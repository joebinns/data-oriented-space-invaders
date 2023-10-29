using UnityEngine;
using Unity.Entities;

class ProjectileAuthoring : MonoBehaviour
{
    public float VerticalVelocity = 6f;
    public float DestroyAtDistance = 12f;
}

class ProjectileBaker : Baker<ProjectileAuthoring>
{
    public override void Bake(ProjectileAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Projectile
        {
            VerticalVelocity = authoring.VerticalVelocity,
            DestroyAtDistance = authoring.DestroyAtDistance,
            DistanceTravelled = 0f
        });
    }
}