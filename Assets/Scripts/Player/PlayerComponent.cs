using Unity.Entities;

public struct PlayerComponent : IComponentData
{

    public float moveSpeed;
    public Entity bullet;           //ECS doesnt use prefabs
    public int bulletCount;
    public float bulletSpread;

}
