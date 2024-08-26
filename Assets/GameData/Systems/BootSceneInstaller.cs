using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;





public class BootSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BootstrapController>().AsSingle();

        InstallService();
    }

    private void InstallService()
    {

    }
}