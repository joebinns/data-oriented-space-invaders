using Unity.Entities;

public struct Projectile : IComponentData
{
    public float VerticalVelocity;
    public float DistanceTravelled;
    public float DestroyAtDistance;
}
