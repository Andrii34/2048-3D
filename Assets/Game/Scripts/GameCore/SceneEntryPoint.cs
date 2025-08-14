using Cube2024.Inputs;
using Cysharp.Threading.Tasks;

using UnityEngine;
using Zenject;

public class SceneEntryPoint : SceneEnter
{
    [SerializeField] private int _cubePoolSize=20;
    [SerializeField] private CubeSpawner _spawner;
    private CubeConfigProvider _cubeConfigProvider;
    private AsyncCubePool _asyncCubePool;
    
    [Inject] 
    private void Construct(CubeConfigProvider cubeConfigProvider, AsyncCubePool asyncCubePool)
    {
        _cubeConfigProvider = cubeConfigProvider;
        _asyncCubePool = asyncCubePool;
    }
    
    public override async  UniTask InitializeAsync()
    {
        await _cubeConfigProvider.LoadConfigsAsync();
        await _asyncCubePool.InitializeAsync(_cubePoolSize);
        await _spawner.SpawnFirstCubeAsync();

    }
    private void OnDestroy()
    {
        _cubeConfigProvider.ReleaseConfigs();
    }
}
