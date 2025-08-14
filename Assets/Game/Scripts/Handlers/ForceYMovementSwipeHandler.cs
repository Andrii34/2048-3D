using UnityEngine;

using System;
using Zenject;
using Cube2024.Inputs;

namespace Cube2024.Handlers 
{ 
public class ForceYMovementSwipeHandler : MonoBehaviour
{
    [SerializeField] private float _force = 1.0f;

    private Rigidbody _movableRigidBody;
    private ISwipeDetector _swipeDetector;

   public void SetMovable(GameObject movableObject)
        {
            if (movableObject == null)
                throw new ArgumentNullException(nameof(movableObject), "Movable object cannot be null.");
            _movableRigidBody = movableObject.GetComponent<Rigidbody>();
            if (_movableRigidBody == null)
                throw new InvalidOperationException("The provided GameObject does not contain a Rigidbody component.");
        }
        [Inject]
    public void Construct( ISwipeDetector swipeDetector)
    {
        _swipeDetector = swipeDetector;
     }

    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }


    private void Subscribe()
    {
        _swipeDetector.OnSwipeEnd += OnSwipeEnd;
    }

    private void Unsubscribe()
    {
        if (_swipeDetector == null)
            return;

        _swipeDetector.OnSwipeEnd -= OnSwipeEnd;
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        if (_movableRigidBody == null)
            return;

        
        _movableRigidBody.AddForce(_movableRigidBody.transform.forward * _force, ForceMode.Impulse);

        
        _movableRigidBody = null;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}

}