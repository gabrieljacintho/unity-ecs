using Unity.Entities;

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
