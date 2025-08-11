using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Installing Project Bindings...");
        Container.Bind<SceneLoadService>().AsSingle().NonLazy();
    }
}
