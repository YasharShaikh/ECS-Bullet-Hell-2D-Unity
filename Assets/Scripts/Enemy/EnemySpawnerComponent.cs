using Unity.Entities;
using Unity.Physics.GraphicsIntegration;

public struct EnemySpawnerComponent : IComponentData
{
    public Entity enemy;
    public float spawnRadius;
    public float distanceFromPlayer;
    public float timeBetweenSpawn;
    public float currentTimeBeforeNextSpawn;    
    public int enemySpawnRate;
    public int enemyIncrementRate;
    public int maxEnemySpawn;
}
