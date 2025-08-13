using UnityEngine;

namespace Cube2024.Effects 
{
    public interface ICollisionEffect<T>
    {

        void Execute(T other, Collision collisionData);
    }
}

