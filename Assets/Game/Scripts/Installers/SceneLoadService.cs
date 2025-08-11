using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoadService
{
    private readonly DiContainer _container;
    private string _currentSceneName;
    private const string LoadingScreenKay = "Assets/Game/Scripts/LoadingsScreen/loadingScreen.prefab";
    private GameObject _loadingScreenInstance;
    public SceneLoadService(DiContainer container)
    {
        _container = container;
    }

    public async Task LoadLevelAsync(string sceneName)
    {
        await ShowLoadingScreenAsync();

        var loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        loadOp.allowSceneActivation = false;

        while (loadOp.progress < 0.9f)
            await Task.Yield();

        loadOp.allowSceneActivation = true;

        while (!loadOp.isDone)
            await Task.Yield();

        var entryPoint = UnityEngine.Object.FindFirstObjectByType<SceneEnter>();
        if (entryPoint == null)
            throw new Exception("ISceneEntryPoint not found in the loaded scene.");

        await entryPoint.InitializeAsync();

        await HideLoadingScreenAsync();
    }

    private async Task ShowLoadingScreenAsync()
    {
        if (_loadingScreenInstance != null)
            return; // Уже показан

        var handle = Addressables.InstantiateAsync(LoadingScreenKay); 
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadingScreenInstance = handle.Result;
        }
        else
        {
            Debug.LogWarning("Failed to load loading screen via Addressables.");
        }
    }

    private async Task HideLoadingScreenAsync()
    {
        if (_loadingScreenInstance != null)
        {
            
            Addressables.ReleaseInstance(_loadingScreenInstance);
            _loadingScreenInstance = null;
            await Task.Yield();
        }
    }

}


