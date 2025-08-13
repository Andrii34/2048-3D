using System;
using UnityEngine;

public class CollisionDetector<T> : MonoBehaviour where T : Component
{
    public event Action<T, Collision> onCollisionStart;
    public event Action<T, Collision> onCollisionContinue;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.TryGetComponent(out T component))
        {
            onCollisionStart?.Invoke(component,col);
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.TryGetComponent(out T component))
        {
            onCollisionContinue?.Invoke(component, col);
        }
    }
}