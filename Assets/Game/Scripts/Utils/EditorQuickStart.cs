#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[InitializeOnLoad]
public static class EditorQuickStart
{
    static EditorQuickStart()
    {
        EditorApplication.playModeStateChanged += state =>
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                var activeScene = SceneManager.GetActiveScene().name;
                if (activeScene != "Bootstrap")
                {
                    PlayerPrefs.SetString("EditorTargetScene", activeScene);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Bootstrap");
                }
            }
        };
    }
}
#endif

