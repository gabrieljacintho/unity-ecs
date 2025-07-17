# Unity ECS

## Links
[Documentation](https://docs.unity3d.com/Packages/com.unity.entities@1.3/manual/index.html)

[Tutorial](https://www.youtube.com/watch?v=ILfUuBLfzGI&list=PLzDRvYVwl53s40yP5RQXitbT--IRcHqba)

## Entity
It's similar to a GameObject with components, but ECS compatible.
```
EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
Entity entity = entityManager.CreateEntity();
```

## Component
Responsible for storing data.
```
public struct LevelComponent : IComponentData
{
    public float Level;
}
```
```
Entity entity = entityManager.CreateEntity(typeof(LevelComponent));
entityManager.SetComponentData(entity, new LevelComponent { Level = 10 });
```

## System
Responsible for the behavior of components.
```
public partial class LevelUpSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComponent levelComponent) =>
        {
            levelComponent.Level += 1f * SystemAPI.Time.DeltaTime;
            UnityEngine.Debug.Log($"Current Level: {levelComponent.Level}");
        }).Schedule();
    }
}
```

## Archetype
It is a prototype Entity with all its components.
```
EntityArchetype entityArchetype = entityManager.CreateArchetype(
    typeof(LevelComponent),
    typeof(LocalTransform)
    );
Entity entity = entityManager.CreateEntity(entityArchetype);
```
