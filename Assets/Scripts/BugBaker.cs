using Unity.Entities;

public class BugBaker : Baker<BugAuthoring>
{
    public override void Bake(BugAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new Speed
        {
            Value = authoring.speed
        });
        AddComponent(entity, new ProjectileRef
        {
            Entity = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic)
        });
    }
}