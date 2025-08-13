using UnityEngine;
using Cube2024.Cube;
namespace Cube2024.Effects
{


    [RequireComponent(typeof(Rigidbody))]
    public class AddForceEffect : CollisionEffectBase<CubValueContainer>
    {
        [SerializeField] private Vector3 force = Vector3.up * 10f;
        [SerializeField] private ForceMode mode = ForceMode.Impulse;
        private Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public override void Execute(CubValueContainer other, Collision collisionData)
        {

            rb.AddForce(force, mode);

        }
    }

}