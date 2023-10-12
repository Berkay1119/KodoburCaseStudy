
public interface ISpawnable
{
    SpawnLocation SpawnLocation { get; set; }
    float SpawnCooldown { get; set; }
    void ReturnToPool();
    void Spawn(SpawnLocation spawnLocation);

    void SetCooldown();
    bool IsActive();
    int MaxAmount();
}