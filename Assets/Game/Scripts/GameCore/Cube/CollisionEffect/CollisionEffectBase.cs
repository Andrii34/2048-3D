using UnityEngine;

namespace Cube2024.Effects
{
    public abstract class CollisionEffectBase<T> : MonoBehaviour, ICollisionEffect<T>
    {
        public abstract void Execute(T other, Collision collisionData);


    }
}


