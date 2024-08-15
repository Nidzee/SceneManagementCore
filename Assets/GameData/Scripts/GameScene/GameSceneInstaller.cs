using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;




public class GameSceneInstaller : MonoInstaller
{

    // List of widgets
    [SerializeField] ManaResourceHandler _manaHandler;





    public override void InstallBindings()
    {
        BindCustomWidgets();
        BindControllersLogic();
    }




    void BindCustomWidgets()
    {
        Container.BindInstance(_manaHandler);
    }

    void BindControllersLogic()
    {
        Container.BindInterfacesAndSelfTo<GameSceneController>().AsSingle().NonLazy();
    }
}