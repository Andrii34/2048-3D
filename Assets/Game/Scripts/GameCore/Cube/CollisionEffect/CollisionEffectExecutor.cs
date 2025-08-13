using UnityEngine;
using Cube2024.Cube;
namespace Cube2024.Effects
{
    public class CollisionEffectExecutor : MonoBehaviour
    {
        [SerializeField] private CollisionEffectBase<Cube>[] effects;

        private CollisionDetector<Cube> _detector;

        private void Awake()
        {
            _detector = GetComponent<CollisionDetector<Cube>>();
        }

        private void OnEnable()
        {
            _detector.onCollisionStart += OnCollisionStart;
        }

        private void OnDisable()
        {
            _detector.onCollisionStart -= OnCollisionStart;
        }

        private void OnCollisionStart(Cube other, Collision collisionData)
        {
            foreach (var effect in effects)
            {
                effect.Execute(other, collisionData);
            }
        }
    }
}
