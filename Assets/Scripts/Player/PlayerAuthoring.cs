using Unity.Entities;
using UnityEngine;



public class PlayerAuthoring : MonoBehaviour
{


    public float moveSpeed = 5.0f;
    public GameObject bullet;
    public int bulletCount = 50;
    [Range(0.0f, 10.0f)] public float bulletSpread = 5.0f;



    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
           Entity playerEntity = GetEntity(TransformUsageFlags.None);


            AddComponent(playerEntity, new PlayerComponent
            {
                moveSpeed = authoring.moveSpeed,
                bullet = GetEntity(authoring.bullet,TransformUsageFlags.None),
                bulletSpread = authoring.bulletSpread,
                bulletCount = authoring.bulletCount
            });
        }
    }

}
