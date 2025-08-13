using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AudioManager _audioManagerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>()
                 .FromComponentInNewPrefab(_audioManagerPrefab)
                 .AsSingle()
                 .NonLazy();
        Container.Bind<SceneLoadService>().AsSingle().NonLazy();
        Container.Bind<Score>().AsSingle().NonLazy();
    }
}
