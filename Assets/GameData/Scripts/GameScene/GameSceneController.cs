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
    GameTowersController _gameTowersController;
    EnemySpawnerController _enemySpawnerController;
    Castle _castle;
    GameUIController _gameUIController;




    public GameSceneController(
        ManaResourceHandler manaHandler,
        GameSceneTopPanelController topPanelController,
        GameTestWidgetController gameTestWidgetController,
        GameCoinsController gameCoinsController,
        GameTowersController gameTowersController,
        EnemySpawnerController enemySpawnerController,
        Castle castle,
        GameUIController gameUIController,
        TowerCardsHandler towerCardsHandler
    )
    {
        _manaHandler = manaHandler;
        _topPanelController = topPanelController;
        _gameCoinsController = gameCoinsController;
        _gameTestWidgetController = gameTestWidgetController;
        _towerCardsHandler = towerCardsHandler;
        _gameTowersController = gameTowersController;
        _enemySpawnerController = enemySpawnerController;
        _gameUIController = gameUIController;
        _castle = castle;
    }





    public void Initialize()
    {
        PauseController controller = new PauseController();

        _castle.Initalize();
        _manaHandler.Initialize();
        _topPanelController.Initialize();
        _gameCoinsController.Initialize();
        _gameTestWidgetController.Initialize();
        _towerCardsHandler.Initialize(_gameCoinsController);
        _gameTowersController.Initialize();
        _enemySpawnerController.Initialize();
        _gameUIController.Initalize();




        _gameTowersController.OnTowerPlaceClicked.RemoveAllListeners();
        _gameTowersController.OnTowerPlaceClicked.AddListener(DetectTowerPlaceSeleted);



        _castle.OnLevelLost.RemoveAllListeners();
        _castle.OnLevelLost.AddListener(DetectLevelLost);




        LaunchGameLogic();
    }

    void LaunchGameLogic()
    {
        _manaHandler.StartResourceGathering();
    }













    void DetectTowerPlaceSeleted(TowerPlace towerPlace)
    {


        var selectionStatus = towerPlace.GetThisTowerSelectionType();


        if (selectionStatus == TowerPlaceSelectionType.TryToBuild)
        {
            _gameTowersController.ActivateSelectionOverlay();
            _gameUIController.ActivateTowersWindow();
            _towerCardsHandler.OnCloseClicked.RemoveAllListeners();
            _towerCardsHandler.OnCloseClicked.AddListener(StopTowerSelection);
            _towerCardsHandler.OnSuccessCardSelected.RemoveAllListeners();
            _towerCardsHandler.OnSuccessCardSelected.AddListener(DetectTowerCardSuccessPressed);
        }
        else
        {
            Debug.Log("Tower is already build. Add logic for inspect or destroy");
            _gameTowersController.InspectionOverlayTESTER();
        }
    }

    void DetectTowerCardSuccessPressed(TowerCardWidget cardToApply)
    {
        _gameTowersController.ApplyTowerCard(cardToApply.Data);
        StopTowerSelection();
    }

    void StopTowerSelection()
    {
        _gameUIController.HideTowersWindow();
        _gameTowersController.StopSelection();
    }






















    
    void DetectLevelLost()
    {
        
    }
}