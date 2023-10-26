using UnityEngine;
using Unity.Entities;

class ProjectileAuthoring : MonoBehaviour
{
    public Vector3 Velocity;
}

class ProjectileBaker : Baker<ProjectileAuthoring>
{
    public override void Bake(ProjectileAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Projectile
        {
            Velocity = authoring.Velocity
        });
    }
}