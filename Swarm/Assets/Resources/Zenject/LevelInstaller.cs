using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] GameObject _inputGO;

    public override void InstallBindings()
    {
        BindInput();
    }

    void BindInput()
    {
        IInput inputComponent;

        if (Application.isMobilePlatform) inputComponent = _inputGO.AddComponent<MobileInput>();
        else inputComponent = _inputGO.AddComponent<DesktopInput>();

        Container.Inject(inputComponent);
        Container.Bind<IInput>().FromInstance(inputComponent).AsSingle();
    }
}
