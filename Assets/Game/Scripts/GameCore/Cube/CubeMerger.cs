using System;
using UnityEngine;
using Zenject;

namespace Cube2024.GamePlay
{
    [RequireComponent(typeof(ÑubeDetector))]
    public class CubeMerger : MonoBehaviour
    {
        public event Action OnCubeMerged;
        private  Cube _thisCube;
        private ÑubeDetector _detector;

      
        private void Awake()
        {
            _thisCube = GetComponent<Cube>();
            _detector = GetComponent<ÑubeDetector>();
        }

        private void OnEnable()
        {
            _detector.OnCollisionStart += OnMergerCollision;
        }

        private void OnDisable()
        {
            _detector.OnCollisionContinue -= OnMergerCollision;
        }
        private void OnMergerCollision(Cube cube, Collision collision)
        {
            if (cube == null || cube.ValueReactive == null || _thisCube == null || _thisCube.ValueReactive == null)
            {
                
                return;
            }
            if ((_thisCube.Value == cube.Value && gameObject.GetInstanceID() > cube.GetInstanceID()))
            {
                
                long curentValue= _thisCube.Value;
                _thisCube.SetValue (curentValue *= 2) ;
                cube.ResetCube();
                OnCubeMerged?.Invoke();
            }


        }
    }
}

