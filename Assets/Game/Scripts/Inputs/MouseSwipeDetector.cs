using System;
using UnityEngine;
using Zenject;

namespace Cube2024.Inputs
{
    public class MouseSwipeDetector : ISwipeDetector,ITickable
    {
        public event Action<Vector2> OnSwipeStart;
        public event Action<Vector2> OnSwipeMove;
        public event Action<Vector2> OnSwipeEnd;

        private bool _isSwipe;
        private Vector3 _lastPosition = new Vector2();

        public void Tick()
        {
            if (!Input.GetMouseButton(0))
            {
                if (_isSwipe)
                {
                    _isSwipe = false;
                    OnSwipeEnd?.Invoke(_lastPosition);
                }

                _lastPosition = Input.mousePosition;
                return;
            }

            if (!_isSwipe)
            {
                _isSwipe = true;
                OnSwipeStart?.Invoke(Input.mousePosition - _lastPosition);
            }
            
            OnSwipeMove?.Invoke(Input.mousePosition - _lastPosition);
            _lastPosition = Input.mousePosition;
        }

       
    }
}


