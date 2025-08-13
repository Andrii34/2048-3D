using UnityEngine;

using UniRx;
using Cube2024.Cube;
using Zenject;
using System;

public class Score 
{
    private long _currentScore;
    public IReadOnlyReactiveProperty<long> CurrentScoreReactive => _currentScoreReactive;
    private ReactiveProperty<long> _currentScoreReactive = new ReactiveProperty<long>(0);
    private CompositeDisposable _disposables = new CompositeDisposable();

    [Inject]
    private void Construct(CubValueContainer cube)
    {
        long previousValue = cube.CubValue;

        cube.CubValueReactive
            .Skip(1) 
            .Subscribe(newValue =>
            {
                long delta = newValue - previousValue;
                _currentScore += delta;
                _currentScoreReactive.Value = _currentScore;
                previousValue = newValue;
            })
            .AddTo(_disposables);
    }
}

