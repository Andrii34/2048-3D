using UnityEngine;
using Zenject;

public class ProvidersInstaller : MonoInstaller
{
   public override void InstallBindings()
    {
       Container.Bind<CubeConfigProvider>()
            .AsSingle()
            .NonLazy();
    }
}
