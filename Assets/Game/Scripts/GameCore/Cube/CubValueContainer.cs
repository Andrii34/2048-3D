using UniRx;
using UnityEngine;

namespace Cube2024.Cube 
{
    using UniRx;
    using System;
    using UnityEngine;

    public class CubValueContainer : IDisposable
    {
        private long _cubValue;
        private ReactiveProperty<long> _cubValueReactive;

        public IReadOnlyReactiveProperty<long> CubValueReactive => _cubValueReactive;

        public void Initialize(long startCubValue)
        {
            _cubValue = startCubValue;

           
            _cubValueReactive?.Dispose();
            _cubValueReactive = new ReactiveProperty<long>(_cubValue);
        }

        public long CubValue
        {
            get => _cubValueReactive.Value;
            set
            {
                if (value < 2 || value % 2 != 0)
                {
                    Debug.LogWarning($"[CubValueContainer] Invalid value: {value}");
                    return;
                }
                _cubValueReactive.Value = value;
            }
        }

        public void Dispose()
        {
            _cubValueReactive?.Dispose();
            _cubValueReactive = null;
        }
    }


}

