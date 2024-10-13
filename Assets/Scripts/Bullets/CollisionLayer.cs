using UnityEngine;

public enum CollisionLayer 
{
    Default = 1 <<0,
    Wall = 1 <<6,
    Enemy = 2 <<7,
}

public class LayerMaskHelper
{
    public static uint GetLayerMaskFromTwoLayer(CollisionLayer layer1, CollisionLayer layer2)
    {
        return (uint)layer1 | (uint)layer2;
    }
}
