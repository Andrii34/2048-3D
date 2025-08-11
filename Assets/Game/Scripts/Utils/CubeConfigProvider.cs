using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class CubeConfigProvider
{
    
    private const string ConfigsLabel = "CubeConfigs";
    private Dictionary<long, CubeConfigs> _configByPoints;
    private AsyncOperationHandle<IList<CubeConfigs>> _handle;
    public async UniTask LoadConfigsAsync()
    {
        if (_handle.IsValid())
            return;
       _handle =  Addressables.LoadAssetsAsync<CubeConfigs>(ConfigsLabel, null);

        _configByPoints = new Dictionary<long, CubeConfigs>();
        var configs = await _handle.ToUniTask();
        foreach (var config in configs)
        {
            if (_configByPoints.ContainsKey(config.Points))
            {
                Debug.LogWarning($"Duplicate Points: {config.Points}");
                continue; 
            }
            _configByPoints[config.Points] = config;
        }
    }
    public CubeConfigs GetConfig(long points)
    {
        return _configByPoints.TryGetValue(points, out var config) ? config : null;
    }
    public void ReleaseConfigs()
    {
        if (_handle.IsValid())
        {
            Addressables.Release(_handle);
            _handle = default;
            _configByPoints?.Clear();
        }
    }
    public IReadOnlyCollection<CubeConfigs> GetAllConfigs()
    {
        return _configByPoints != null ? _configByPoints.Values.ToList() : new List<CubeConfigs>();
    }
}
