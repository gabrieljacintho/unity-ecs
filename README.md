# Unity ECS

## Links
[Documentation](https://docs.unity3d.com/Packages/com.unity.entities@1.3/manual/index.html)

[Webinar](https://www.youtube.com/watch?v=gqJlQJn0N2g)

## Limitations
- No audio support
- No animation support
- No UI support
- No AI navigation support

## Entity
It's similar to a GameObject with components, but ECS compatible.
```
EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
Entity entity = entityManager.CreateEntity();
```

## Component
Responsible for storing data.
```
public struct Speed : IComponentData
{
    public float Value;
}
```
```
Entity entity = entityManager.CreateEntity(typeof(Speed));
entityManager.SetComponentData(entity, new Speed { Value = 10 });
```

## System
Responsible for the behavior of components.
```
/* 
 * SimulationSystemGroup == Update (default)
 * FixedStepSimulationSystemGroup == FixedUpdate
 * PresentationSystemGroup == LateUpdate
*/
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct MoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        // RefRO = Read-Only Reference, RefRW = Read-Write Reference
        foreach (var (speed, transform) in SystemAPI.Query<RefRO<Speed>, RefRW<LocalTransform>>().WithNone<ConflictState>())
        {
            transform.ValueRW.Position.x += speed.ValueRO.Value * SystemAPI.Time.DeltaTime;
        }
    }
}
```

## Baker
Allows us to convert GameObjects into Entities.
```
public class BugBaker : Baker<BugAuthoring>
{
    public override void Bake(BugAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Speed
        {
            Value = authoring.speed
        });
    }
}
```
```
public class BugAuthoring : MonoBehaviour
{
    public int speed;
    public GameObject projectilePrefab;
}
```

## Archetype
It is a Entity prototype with all its components.
```
EntityArchetype entityArchetype = entityManager.CreateArchetype(
    typeof(Speed),
    typeof(LocalTransform)
    );
Entity entity = entityManager.CreateEntity(entityArchetype);
```
