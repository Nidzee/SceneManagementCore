using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;




public class GameSceneInstaller : MonoInstaller
{

    // List of widgets
    [SerializeField] ManaResourceHandler _manaHandler;
    [SerializeField] GameSceneTopPanelHandler _topPanelHandler;




    public override void InstallBindings()
    {
        BindCustomWidgets();
        BindControllersLogic();
    }




    void BindCustomWidgets()
    {
        Container.BindInstance(_manaHandler);
        Container.BindInstance(_topPanelHandler);
    }

    void BindControllersLogic()
    {
        Container.BindInterfacesAndSelfTo<GameSceneController>().AsSingle().NonLazy();
    }
}