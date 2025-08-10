using Cube2024.Inputs;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInputForCurrentPlatform();
    }
    private void BindInputForCurrentPlatform()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        Container.BindInterfacesAndSelfTo<MouseSwipeDetector>().AsSingle();
#elif UNITY_ANDROID || UNITY_IOS
    Container.BindInterfacesAndSelfTo<MobileSwipeDetector>().AsSingle();
#else
    throw new System.PlatformNotSupportedException("Platform not supported for input binding");
#endif
    }
}
