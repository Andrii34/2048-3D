using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Cube2024.GamePlay;
using Zenject;

public class AsyncCubePool
{
    private readonly ASyncCubeFactory _factory;
    private readonly Queue<Cube> _available = new Queue<Cube>();
    private bool _initialized = false;
    private Transform _root; 

    [Inject]
    public AsyncCubePool(ASyncCubeFactory factory)
    {
        _factory = factory;

        
        var rootGO = new GameObject("CubePool");
        _root = rootGO.transform;
    }

    public async UniTask InitializeAsync(int initialSize)
    {
        if (_initialized) return;
        _initialized = true;

        for (int i = 0; i < initialSize; i++)
        {
            var cube = await _factory.CreateAsync(Vector3.zero, 2);

           
            cube.transform.SetParent(_root);
            cube.gameObject.SetActive(false);

            _available.Enqueue(cube);
        }
    }

    public async UniTask<Cube> SpawnAsync(Vector3 position, long value)
    {
        Cube cube;

        if (_available.Count > 0)
        {
            cube = _available.Dequeue();
            cube.Initialize(value);
        }
        else
        {
            cube = await _factory.CreateAsync(position, value);
        }

        
        cube.transform.SetParent(_root);
        cube.transform.position = position;
        cube.gameObject.SetActive(true);

        return cube;
    }

    public void Despawn(Cube cube)
    {
        cube.ResetCube();

        cube.gameObject.SetActive(false);
        cube.transform.SetParent(_root);

        _available.Enqueue(cube);
    }
}

