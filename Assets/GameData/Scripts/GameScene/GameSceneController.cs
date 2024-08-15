using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;






public class GameSceneController : IInitializable
{
    ManaResourceHandler _manaHandler;
    GameSceneTopPanelController _topPanelController;
    GameCoinsController _gameCoinsController;
    GameTestWidgetController _gameTestWidgetController;
    TowerCardsHandler _towerCardsHandler;






    public GameSceneController(
        ManaResourceHandler manaHandler,
        GameSceneTopPanelController topPanelController,
        GameTestWidgetController gameTestWidgetController,
        GameCoinsController gameCoinsController,
        TowerCardsHandler towerCardsHandler
    )
    {
        _manaHandler = manaHandler;
        _topPanelController = topPanelController;
        _gameCoinsController = gameCoinsController;
        _gameTestWidgetController = gameTestWidgetController;
        _towerCardsHandler = towerCardsHandler;
    }





    public void Initialize()
    {
        _manaHandler.Initialize();
        _topPanelController.Initialize();
        _gameCoinsController.Initialize();
        _gameTestWidgetController.Initialize();
        _towerCardsHandler.Initialize(_gameCoinsController);


        LaunchGameLogic();
    }

    void LaunchGameLogic()
    {
        _manaHandler.StartResourceGathering();
    }
}