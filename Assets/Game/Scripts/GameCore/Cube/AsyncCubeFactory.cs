using Cube2024.GamePlay;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class ASyncCubeFactory
{
    private DiContainer _container;
    private readonly string _cubeAddressableKey;
    

    [Inject]
    private void Construct( DiContainer container)
    {
        _container = container;
       
    }

    public ASyncCubeFactory(string cubeAddressableKey)
    {
        _cubeAddressableKey = cubeAddressableKey;
    }

    public async UniTask<Cube> CreateAsync(Vector3 position, long value)
    {

        var prefabGO = await Addressables.LoadAssetAsync<GameObject>(_cubeAddressableKey).ToUniTask();


        var containerValue = new CubValueContainer();
        

        
        var cube = _container.InstantiatePrefabForComponent<Cube>(
            prefabGO,
            position,
            Quaternion.identity,
            null,
            new object[] { containerValue }
        );

       
        return cube;
    }   
}



