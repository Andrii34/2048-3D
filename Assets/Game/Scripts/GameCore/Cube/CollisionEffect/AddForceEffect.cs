using UnityEngine;
using Cube2024.GamePlay;
namespace Cube2024.Effects
{


    [RequireComponent(typeof(Rigidbody))]
    public class AddForceEffect : MergeEffectBase
    {
        [SerializeField] private Vector3 _force = Vector3.up * 10f;
        [SerializeField] private ForceMode _mode = ForceMode.Impulse;
        private Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public override void Execute()
        {

            rb.AddForce(_force, _mode);

        }
    }

}