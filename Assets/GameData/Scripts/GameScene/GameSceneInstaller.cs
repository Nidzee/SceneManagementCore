using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;




public class GameSceneInstaller : MonoInstaller
{

    // List of widgets
    [SerializeField] ManaResourceHandler _manaHandler;
    [SerializeField] GameSceneTopPanelController _topPanelHandler;
    [SerializeField] GameCoinsWidget _gameCoinsWidget;
    [SerializeField] GameTestWidget _gameTestWidget;
    [SerializeField] TowerCardsHandler _towerCardsHandler;
    [SerializeField] GameTowersControllerGameTestWidget _gameTowersControllerGameTestWidget;
    [SerializeField] EnemySpawnerController _enemySpawnerController;
    [SerializeField] Castle _castle;
    [SerializeField] GameUIController _gameUIController;


    public override void InstallBindings()
    {
        BindCustomWidgets();
        BindControllersLogic();
    }




    void BindCustomWidgets()
    {
        Container.BindInstance(_manaHandler);
        Container.BindInstance(_topPanelHandler);
        Container.BindInstance(_gameCoinsWidget);
        Container.BindInstance(_gameTestWidget);
        Container.BindInstance(_towerCardsHandler);
        Container.BindInstance(_gameTowersControllerGameTestWidget);
        Container.BindInstance(_enemySpawnerController);
        Container.BindInstance(_castle);
        Container.BindInstance(_gameUIController);
    }

    void BindControllersLogic()
    {
        Container.BindInterfacesAndSelfTo<GameCoinsController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameTestWidgetController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PauseController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameTowersController>().AsSingle().NonLazy();
        


        Container.BindInterfacesAndSelfTo<GameSceneController>().AsSingle().NonLazy();
    }
}