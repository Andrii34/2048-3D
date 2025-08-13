using UnityEngine;
using UniRx;
using Zenject;

namespace Cube2024.Cube 
{
    public class Cube : MonoBehaviour
    {
        private CubValueContainer _container;

        [Inject]
        public void Construct(CubValueContainer container)
        {
            _container = container;
        }

        public void Initialize(long startValue)
        {
            _container.Initialize(startValue);
        }

        public long Value => _container.CubValue;
        public IReadOnlyReactiveProperty<long> ValueReactive => _container.CubValueReactive;
    }
}




