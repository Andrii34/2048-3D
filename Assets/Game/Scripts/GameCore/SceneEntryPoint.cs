using Cube2024.Inputs;
using Cysharp.Threading.Tasks;

using UnityEngine;
using Zenject;

public class SceneEntryPoint : SceneEnter
{
    private CubeConfigProvider _cubeConfigProvider;
    [Inject] 
    private void Construct(CubeConfigProvider cubeConfigProvider)
    {
        _cubeConfigProvider = cubeConfigProvider;
    }
    
    public override async  UniTask InitializeAsync()
    {
        await _cubeConfigProvider.LoadConfigsAsync();

    }
    private void OnDestroy()
    {
        _cubeConfigProvider.ReleaseConfigs();
    }
}
