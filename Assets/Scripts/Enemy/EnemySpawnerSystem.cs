using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;
public partial struct EnemySpawnerSystem : ISystem
{

    private EntityManager entityManager;
    private Entity enemySpawnerEntity;
    private EnemySpawnerComponent enemySpawnerComponent;
    private Entity player;

    private Unity.Mathematics.Random random;

    private void OnCreate(ref SystemState state)
    {
        random = Unity.Mathematics.Random.CreateFromIndex((uint)enemySpawnerComponent.GetHashCode());

    }


    private void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        enemySpawnerEntity = SystemAPI.GetSingletonEntity<EnemySpawnerComponent>();
        enemySpawnerComponent = entityManager.GetComponentData<EnemySpawnerComponent>(enemySpawnerEntity);

        player = SystemAPI.GetSingletonEntity<PlayerComponent>();

        SpawnEnemies(ref state);
    }


    private void SpawnEnemies(ref SystemState state)
    {
        enemySpawnerComponent.currentTimeBeforeNextSpawn -= SystemAPI.Time.DeltaTime;
        if (enemySpawnerComponent.currentTimeBeforeNextSpawn <= 0.0f)
        {
            for (int i = 0; i < enemySpawnerComponent.enemySpawnRate; i++)
            {
                EntityCommandBuffer ECB = new EntityCommandBuffer(Allocator.TempJob);
                Entity enemyEntity = entityManager.Instantiate(enemySpawnerComponent.enemy);

                LocalTransform enemyTransform = entityManager.GetComponentData<LocalTransform>(enemyEntity);
                LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(player);

                float minDistanceSquared = enemySpawnerComponent.distanceFromPlayer* enemySpawnerComponent.distanceFromPlayer;
                float2 randomOffset = random.NextFloat2Direction() * random.NextFloat(enemySpawnerComponent.distanceFromPlayer, enemySpawnerComponent.spawnRadius);
                float2 playerPosition = new float2(playerTransform.Position.x, playerTransform.Position.y);
                float2 spawnPosition = playerPosition + randomOffset;
                float distanceSquared = math.lengthsq(spawnPosition - playerPosition);

                if (distanceSquared < minDistanceSquared) 
                {
                    spawnPosition = playerPosition + math.normalize(randomOffset) * math.sqrt(minDistanceSquared);
                }
                enemyTransform.Position = new float3(spawnPosition.x, spawnPosition.y, 0.0f);

                float3 direction = math.normalize(playerTransform.Position - enemyTransform.Position);//look direciton 
                float angle = math.atan2(direction.x, direction.y);
                angle-=math.radians(90.0f);
                quaternion lookRotation = quaternion.AxisAngle(new float3(0, 0, 1), angle);
                enemyTransform.Rotation = lookRotation;


                ECB.SetComponent(enemyEntity, enemyTransform);
                ECB.AddComponent(enemyEntity, new EnemyComponent
                {
                    currentHealth = 100.0f,
                    Speed = 1.25f,
                });



                ECB.Playback(entityManager);
                ECB.Dispose();
            }
            int desiredEnemiesPerWave = enemySpawnerComponent.enemyIncrementRate + enemySpawnerComponent.enemySpawnRate;
            int enemiesPerWave = math.min(desiredEnemiesPerWave, enemySpawnerComponent.maxEnemySpawn);
            enemySpawnerComponent.enemySpawnRate = enemiesPerWave;

            enemySpawnerComponent.currentTimeBeforeNextSpawn = enemySpawnerComponent.timeBetweenSpawn;
        }

        entityManager.SetComponentData(enemySpawnerEntity, enemySpawnerComponent);
    }
}
