using UnityEngine;
using UniRx;
using Zenject;
using System;

namespace Cube2024.GamePlay 
{
    public class Cube : MonoBehaviour
    {
        private CubValueContainer _container;
        public event Action OnCuInit;
        public long Value => _container.CubValue;
        public IReadOnlyReactiveProperty<long> ValueReactive => _container?.CubValueReactive;

        [Inject]
        public void Construct(CubValueContainer container)
        {
            
            _container = container;
            Debug.Log(_container.CubValue);
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }
        public void Initialize(long startCubValue)
        {
            if (_container == null)
            {
                throw new InvalidOperationException("CubValueContainer is not set. Call Construct first.");
            }
            _container.Initialize(startCubValue);
            gameObject.SetActive(true);
            OnCuInit?.Invoke();
        }
        public void SetValue(long value)
        {
            _container.CubValue = value;
        }
       
        
        public void ResetCube()
        {
          
            gameObject.SetActive(false);
        }
       
    }
}




