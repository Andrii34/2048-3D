using Cube2024.GamePlay;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

using UnityEngine;
using Cysharp.Threading.Tasks;

public class ASyncCubeFactory
{
    private readonly DiContainer _container;
    private readonly string _cubeAddressableKey;

    public ASyncCubeFactory(DiContainer container, string cubeAddressableKey)
    {
        _container = container;
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
            new object[] { containerValue } // Inject container
        );

        cube.Initialize(value);

        return cube;
    }
}




