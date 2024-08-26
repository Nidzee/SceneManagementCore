using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;






public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] TopMenuPanel _topMenuPanel;
    [SerializeField] MainMenuSceneContent _mainMenuSceneContent;
    [SerializeField] BottomMenuPanel _bottomPanel;





    public override void InstallBindings()
    {
        BindCustomWidgets();
        BindControllersLogic();
    }




    void BindCustomWidgets()
    {
        Container.BindInstance(_topMenuPanel);
        Container.BindInstance(_mainMenuSceneContent);
        Container.BindInstance(_bottomPanel);
    }

    void BindControllersLogic()
    {
        Container.BindInterfacesAndSelfTo<MainMenuSceneController>().AsSingle().NonLazy();
    }
}