using Cube2024.GamePlay;
using Sirenix.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public class FactoryInstaller:MonoInstaller
{
    [SerializeField] private string cubeAddressableKey; 
    

    public override void InstallBindings()
    {

        Container.Bind<ASyncCubeFactory>()
                .AsSingle()
                .WithArguments(cubeAddressableKey);


        Container.Bind<AsyncCubePool>()
            .AsSingle();
    }
}
