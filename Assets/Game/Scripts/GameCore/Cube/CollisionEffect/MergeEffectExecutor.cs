using UnityEngine;
using Cube2024.GamePlay;
namespace Cube2024.Effects
{
    public class MergeEffectExecutor : MonoBehaviour
    {
        [SerializeField] private MergeEffectBase[] effects;

        private CubeMerger _cubeMerger;

        private void Awake()
        {
            _cubeMerger = GetComponent<CubeMerger>();
        }

        private void OnEnable()
        {
            _cubeMerger.OnCubeMerged += OnMerge;
        }

        private void OnDisable()
        {
            _cubeMerger.OnCubeMerged -= OnMerge;
        }

        private void OnMerge()
        {
            foreach (var effect in effects)
            {
                effect.Execute();
            }
        }
    }
}
