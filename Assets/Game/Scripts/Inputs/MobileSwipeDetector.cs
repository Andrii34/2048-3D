
using System;
using UnityEngine;
using Zenject;


namespace Cube2024.Inputs
{


    public class MobileSwipeDetector : ISwipeDetector, ITickable
    {



        public event Action<Vector2> OnSwipeStart;
        public event Action<Vector2> OnSwipeMove;
        public event Action<Vector2> OnSwipeEnd;


        private bool _isSwipe;
        private Vector2 _lastPosition;

        

        public void Tick()
        {
            if (Input.touchCount == 0)
            {
                if (_isSwipe)
                {
                    _isSwipe = false;
                    OnSwipeEnd?.Invoke(_lastPosition);
                }

                return;
            }

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isSwipe = true;
                    _lastPosition = touch.position;
                    OnSwipeStart?.Invoke(Vector2.zero);
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    var delta = touch.position - _lastPosition;
                    OnSwipeMove?.Invoke(delta);
                    _lastPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isSwipe = false;
                    OnSwipeEnd?.Invoke(_lastPosition);
                    break;
            }
        }
    }

}
