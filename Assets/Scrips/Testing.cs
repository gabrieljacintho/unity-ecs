using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;


    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LocalTransform), // Transform component
            typeof(RenderMeshArray), // Rendering component
            typeof(LocalToWorld), // Required for rendering
            typeof(LevelComponent) // Custom component
            );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(200, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        RenderMeshArray renderMeshArray = new RenderMeshArray(
            new[] { _material },
            new[] { _mesh }
        );

        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetSharedComponentManaged(entity, renderMeshArray);
            entityManager.SetComponentData(entity, new LevelComponent { Level = UnityEngine.Random.Range(10f, 20f) });
        }

        entityArray.Dispose();
    }
}
