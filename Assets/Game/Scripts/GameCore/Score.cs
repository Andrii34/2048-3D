using UnityEngine;
using UniRx;
using Cube2024.GamePlay;
using System;


public class Score : IDisposable
{
    private long _currentScore = 0;

    private readonly ReactiveProperty<long> _currentScoreReactive = new ReactiveProperty<long>(0);
    public IReadOnlyReactiveProperty<long> CurrentScoreReactive => _currentScoreReactive;

    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public void RegisterCub(Cube cube)
    {
        long previousValue = cube.Value;

        cube.ValueReactive
            .Subscribe(newValue =>
            {
                long delta = newValue - previousValue;
                if (delta > 0)
                {
                    _currentScore += delta / 2;
                    _currentScoreReactive.Value = _currentScore;
                    UnityEngine.Debug.Log($"Delta: {delta}, Half: {delta / 2}");
                }
                previousValue = newValue;
            })
            .AddTo(_disposables);
    }



    public void Dispose()
    {
        _disposables.Dispose();
        _currentScoreReactive.Dispose();
    }
}



