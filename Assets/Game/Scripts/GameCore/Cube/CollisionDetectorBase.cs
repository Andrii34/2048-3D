using System;
using UnityEngine;

using UnityEngine;
using System;

public class CollisionDetectorBase<T> : MonoBehaviour where T : Component
{
    public event Action<T, Collision> OnCollisionStart;
    public event Action<T, Collision> OnCollisionContinue;

    [SerializeField, Tooltip("Minimum collision impulse required to trigger the event")]
    private float minImpulse = 1f; // настраивается в инспекторе

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.TryGetComponent(out T component))
        {
            float collisionImpulse = col.impulse.magnitude;

            if (collisionImpulse >= minImpulse)
                OnCollisionStart?.Invoke(component, col);
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.TryGetComponent(out T component))
        {
           
                OnCollisionContinue?.Invoke(component, col);
        }
    }
}
