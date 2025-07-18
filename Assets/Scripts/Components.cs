using Unity.Entities;

public struct Speed : IComponentData
{
    public float Value;
}

public struct ConflictState : IComponentData { }

public struct ProjectileRef : IComponentData
{
    public Entity Entity;
}