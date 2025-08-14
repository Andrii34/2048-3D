using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using Cube2024.GamePlay;
using Cube2024.Inputs;
using Cube2024.Handlers;
using System;



public class CubeSpawner : MonoBehaviour
{
    [Header("Cube Spawn Settings")]
    [SerializeField, Tooltip("Delay between cube spawns in seconds")]
    private float _spawnDelay = 0.3f;

    [SerializeField, Tooltip("Probabilities of spawning different Po2 cube values")]
    private _cubeValueProbability[] cubeValueProbabilities = new _cubeValueProbability[]
    {
        new _cubeValueProbability { Value = 2, Probability = 0.75f },
        new _cubeValueProbability { Value = 4, Probability = 0.25f }
    };

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ForceYMovementSwipeHandler _forceYMovementSwipeHandler;
    [SerializeField] private XMovementSwipeHandler _xMovementSwipeHandler;
    private Score _score;
    private AsyncCubePool _cubePool;
    private ISwipeDetector _swipeDetector;
    private bool _isSpawning = false;
    private bool _canSpawn = false;

    [Inject]
    public void Construct(AsyncCubePool cubePool, ISwipeDetector swipeDetector,Score score)
    {
        _score = score;
        _cubePool = cubePool;
        _swipeDetector = swipeDetector;
    }

    private void OnEnable()
    {
        _swipeDetector.OnSwipeEnd += OnSwipeEnd;
    }

    private void OnDisable()
    {
        _swipeDetector.OnSwipeEnd -= OnSwipeEnd;
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        if (!_isSpawning)
        {
            _ = SpawnWithDelayAsync(GetRandomPo2Value());
        }
    }

    private async UniTaskVoid SpawnWithDelayAsync(long value)
    {
        if (!_canSpawn)
            return;

        _isSpawning = true;
        await UniTask.Delay(TimeSpan.FromSeconds(_spawnDelay));
        await SpawnCubeAsync(_spawnPoint.position, value);
        _isSpawning = false;
    }

    private async UniTask<Cube> SpawnCubeAsync(Vector3 position, long value)
    {
        var cube = await _cubePool.SpawnAsync(position, value);
        SetMovables(cube.gameObject);
        cube.gameObject.SetActive(true);

        Debug.Log($"Spawned cube at {position} with value {value}");
        
        _score.RegisterCub(cube);
        return cube;
    }

    private void SetMovables(GameObject cube)
    {
        _forceYMovementSwipeHandler.SetMovable(cube);
        _xMovementSwipeHandler.SetMovable(cube);
    }

    public async UniTask SpawnFirstCubeAsync()
    {
        _canSpawn = true;
        long value = GetRandomPo2Value();
        await SpawnCubeAsync(_spawnPoint.position, value);
    }

    private long GetRandomPo2Value()
    {
        float roll = UnityEngine.Random.value;
        float cumulative = 0f;

        foreach (var entry in cubeValueProbabilities)
        {
            cumulative += entry.Probability;
            if (roll <= cumulative)
                return entry.Value;
        }

        
        return cubeValueProbabilities[^1].Value;
    }

    [Serializable]
    private struct _cubeValueProbability
    {
        public long Value;
        [Range(0f, 1f)] public float Probability;
    }
}



