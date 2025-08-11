using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    private SceneLoadService _levelLoadService;

    [Inject]
    public void Construct(SceneLoadService levelLoadService)
    {
        _levelLoadService = levelLoadService;
    }

    private async void Start()
    {
#if UNITY_EDITOR
        string target = PlayerPrefs.GetString("EditorTargetScene", null);
        if (!string.IsNullOrEmpty(target))
        {
            PlayerPrefs.DeleteKey("EditorTargetScene");
            await _levelLoadService.LoadLevelAsync(target);
            return;
        }
        await _levelLoadService.LoadLevelAsync("GamePlayScene");

    }
#endif
}
