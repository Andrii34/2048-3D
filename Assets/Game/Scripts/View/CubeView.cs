using TMPro;
using UniRx;
using UnityEngine;
using Zenject;
using Cube2024.GamePlay;
using System;
public class CubeView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private TextMeshPro[] _texts;
    private CubeConfigProvider _configProvider;
    private IDisposable _subscription;
    private Cube _cube;
    [Inject]
    private void Construct(CubeConfigProvider cubeConfigProvider)
    {
        
        _configProvider = cubeConfigProvider;
    }
    private void Awake()
    {
        _cube = GetComponent<Cube>();
       
    }
    private void OnEnable()
    {
        _cube.OnCuInit += Init;
    }
    private void OnDisable()
    {
        if (_cube != null)
        {
            _cube.OnCuInit -= Init;
        }
        _subscription?.Dispose();
    }
    private void Init()
    {
        _subscription?.Dispose(); 

        _subscription = _cube.ValueReactive
            .Subscribe(value =>
            {
                SetColor(value);
                SetText(value.ToString());
                
            });
    }
    private void SetColor(long value)
    {
        Color color = _configProvider.GetConfig(value).Color;
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
