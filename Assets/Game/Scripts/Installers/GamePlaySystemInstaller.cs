using UnityEngine;
using Zenject;

public class GamePlaySystemInstaller:MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Score>().AsSingle().NonLazy();
    }
}
