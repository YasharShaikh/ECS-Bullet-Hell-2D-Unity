using Unity.Entities;
using UnityEngine;

public class EnemySpawnerAuthoring : MonoBehaviour
{

    public GameObject enemy;
    public float spawnRadius;
    public float distanceFromPlayer;
    public float timeBetweenSpawn;
    public int enemySpawnRate;
    public int enemyIncrementRate;
    public int maxEnemySpawn;


    public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            Entity enemySpawnerEntity = GetEntity(TransformUsageFlags.None);
            AddComponent(enemySpawnerEntity, new EnemySpawnerComponent
            {
                enemy = GetEntity(authoring.enemy, TransformUsageFlags.None),
                spawnRadius = authoring.spawnRadius,
                distanceFromPlayer = authoring.distanceFromPlayer,
                timeBetweenSpawn = authoring.timeBetweenSpawn,
                enemySpawnRate = authoring.enemySpawnRate,
                enemyIncrementRate = authoring.enemyIncrementRate,
                maxEnemySpawn = authoring.maxEnemySpawn,

            });
        }
    }
}
