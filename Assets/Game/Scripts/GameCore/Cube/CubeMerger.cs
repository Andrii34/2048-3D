using UnityEngine;
using Zenject;

namespace Cube2024.Cube 
{
    public class CubeMerger : MonoBehaviour
    {
        private CubValueContainer _thisCubValueContainer;
        private CollisionDetector<Cube> _detector;

        [Inject]
        private void Construct(CubValueContainer cubValueContainer)
        {
            _thisCubValueContainer = cubValueContainer;
        }
        private void Awake()
        {
            _detector = GetComponent<CollisionDetector<Cube>>();
        }

        private void OnEnable()
        {
            _detector.onCollisionContinue += OnMergerCollision;
        }

        private void OnDisable()
        {
            _detector.onCollisionContinue -= OnMergerCollision;
        }
        private void OnMergerCollision(Cube cube, Collision collision)
        {
            if ((_thisCubValueContainer.CubValue == cube.Value && gameObject.GetInstanceID() > cube.GetInstanceID()))
            {
                _thisCubValueContainer.CubValue *= 2;
                Destroy(cube.gameObject);
            }


        }
    }
}

