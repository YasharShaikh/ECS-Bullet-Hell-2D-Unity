using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public partial struct EnemySystem : ISystem
{
    private EntityManager entityManager;

    private Entity playerEntity;


    public void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;

        playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
        LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);


        NativeArray<Entity> allEntitites = new NativeArray<Entity>();

        foreach (Entity entity in allEntitites)
        {
            if (entityManager.HasComponent<EnemyComponent>(entity))
            {
                LocalTransform enemyTransform = entityManager.GetComponentData<LocalTransform>(entity);
                EnemyComponent enemyComponent = entityManager.GetComponentData<EnemyComponent>(entity);

                float3 moveDirection = math.normalize(playerTransform.Position - enemyTransform.Position);
                enemyTransform.Position += enemyComponent.Speed * SystemAPI.Time.DeltaTime * moveDirection;

                float3 direction = math.normalize(playerTransform.Position - enemyTransform.Position);//look direciton 
                float angle = math.atan2(direction.x, direction.y);
                angle -= math.radians(90.0f);
                quaternion lookRotation = quaternion.AxisAngle(new float3(0, 0, 1), angle);
                enemyTransform.Rotation = lookRotation;

                entityManager.SetComponentData(entity, enemyTransform);
            }
        }
    }
}
