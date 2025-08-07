using System;
using UnityEngine;

namespace Cube2024.Inputs
{
    public interface ISwipeDetector
    {
        event Action<Vector2> OnSwipeStart;
        event Action<Vector2> OnSwipeMove;
        event Action<Vector2> OnSwipeEnd;
    }

}
