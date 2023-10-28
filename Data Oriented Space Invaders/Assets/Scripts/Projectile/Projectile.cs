using Unity.Entities;
using Unity.Mathematics;

public struct Projectile : IComponentData
{
    public float VerticalVelocity;
    public float DistanceTravelled;
}
