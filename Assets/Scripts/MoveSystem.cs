using Unity.Entities;
using Unity.Transforms;

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