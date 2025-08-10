using TMPro;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private TextMeshPro[] _texts;
    
    public void SetView(CubeConfigs configs)
    {
        if (configs == null)
        {
            Debug.LogError("CubeConfigs is null");
            return;
        }
        SetColor(configs.Color);
        SetText(configs.Points.ToString());
    }
    private void SetColor(Color color)
    {
        if (_renderer != null)
        {
            _renderer.material.color = color;
        }
    }
    private void SetText(string text)
    {
        if (_texts != null)
        {
            foreach (var t in _texts)
            {
                if (t != null)
                {
                    t.text = text;
                }
            }
        }
    }
}
