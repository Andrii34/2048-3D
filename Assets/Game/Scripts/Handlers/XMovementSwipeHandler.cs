using Cube2024.Inputs;
using System;
using UnityEngine;
using Zenject;

namespace Cube2024.Handlers
{

    public class XMovementSwipeHandler : MonoBehaviour
    {
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField, Range(0.5f, 1.5f)] private float _normalizedCoefficient = 1.0f;

        private GameObject _movableObject;
        private ISwipeDetector _swipeDetector;


        [Inject]
        public void Construct(GameObject movableObject, ISwipeDetector swipeDetector)
        {
            _swipeDetector = swipeDetector;
            _movableObject = movableObject;
        }

        private void Start()
        {
            if (_leftBorder == null || _rightBorder == null)
            {
                Debug.LogError($"{nameof(XMovementSwipeHandler)}: Left or Right border is not assigned.");
                enabled = false;
                return;
            }

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
            _swipeDetector.OnSwipeMove += OnSwipeMove;
            _swipeDetector.OnSwipeEnd += OnSwipeEnd;
        }

        private void Unsubscribe()
        {
            if (_swipeDetector == null)
                return;

            _swipeDetector.OnSwipeMove -= OnSwipeMove;
            _swipeDetector.OnSwipeEnd -= OnSwipeEnd;
        }

        private void OnSwipeMove(Vector2 delta)
        {
            if (_movableObject == null)
                return;

            if (Mathf.Approximately(delta.x, 0f))
                return;

            var borderDistance = Mathf.Abs(_rightBorder.position.x - _leftBorder.position.x);
            var offset = borderDistance * _normalizedCoefficient * delta.x / Screen.width;

            var transform = _movableObject.transform;
            var currentPos = transform.position;

            var newX = Mathf.Clamp(currentPos.x + offset, _leftBorder.position.x, _rightBorder.position.x);
            transform.position = new Vector3(newX, currentPos.y, currentPos.z);
        }

        private void OnSwipeEnd(Vector2 delta)
        {
            _movableObject = null;
        }

    }
}