using Cube2024.Inputs;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject]
    private void Construct(ISwipeDetector swipeDetector)
    {
        Debug.Log($"SwipeDetector injected successfully!: {swipeDetector}");
    }
    private void Start()
    {
        Debug.Log("Test script is running!");
    }
}
