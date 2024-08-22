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
    TowerInfoUIHandler _towerInfoUIHandler;
    GameSceneTransferingData _gameSceneTransferingData;








    int _wavesAmount = 0;
    int _currentWaveIndex = 0;











    public GameSceneController(
        ManaResourceHandler manaHandler,
        GameSceneTopPanelController topPanelController,
        GameTestWidgetController gameTestWidgetController,
        GameCoinsController gameCoinsController,
        GameTowersController gameTowersController,
        EnemySpawnerController enemySpawnerController,
        Castle castle,
        GameSceneTransferingData gameSceneTransferingData,
        GameUIController gameUIController,
        TowerInfoUIHandler towerInfoUIHandler,
        TowerCardsHandler towerCardsHandler
    )
    {
        _gameSceneTransferingData = gameSceneTransferingData;
        _manaHandler = manaHandler;
        _topPanelController = topPanelController;
        _gameCoinsController = gameCoinsController;
        _gameTestWidgetController = gameTestWidgetController;
        _towerCardsHandler = towerCardsHandler;
        _gameTowersController = gameTowersController;
        _enemySpawnerController = enemySpawnerController;
        _gameUIController = gameUIController;
        _castle = castle;
        _towerInfoUIHandler = towerInfoUIHandler;
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
        _towerInfoUIHandler.Initialize();




        _gameTowersController.OnTowerPlaceClicked.RemoveAllListeners();
        _gameTowersController.OnTowerPlaceClicked.AddListener(DetectTowerPlaceSeleted);
        _castle.OnLevelLost.RemoveAllListeners();
        _castle.OnLevelLost.AddListener(DetectLevelLost);



        _currentWaveIndex = 0;
        _wavesAmount = 3;




        Debug.Log(_gameSceneTransferingData.GetSOmeData());

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
            _gameUIController.ActivateTowersWindow();
            _gameTowersController.ActivateSelectionOverlay();

            _towerCardsHandler.OnCloseClicked.RemoveAllListeners();
            _towerCardsHandler.OnCloseClicked.AddListener(StopTowerSelection);
            _towerCardsHandler.OnSuccessCardSelected.RemoveAllListeners();
            _towerCardsHandler.OnSuccessCardSelected.AddListener(DetectTowerCardSuccessPressed);
            return;
        }

        if (selectionStatus == TowerPlaceSelectionType.Inspect)
        {
            _gameUIController.ActivateTowerInfoUI();
            _gameTowersController.ActivateSelectionOverlay();

            _towerInfoUIHandler.InitializeInfoForTower(towerPlace);
            _towerInfoUIHandler.OnCloseButtonClicked.RemoveAllListeners();
            _towerInfoUIHandler.OnCloseButtonClicked.AddListener(StopTowerSelection);

            _towerInfoUIHandler.OnSellButtonClicked.RemoveAllListeners();
            _towerInfoUIHandler.OnSellButtonClicked.AddListener(SellTower);
            return;
        }


        CustomLogger.LogError("Error! Wrong type of tower-selection passed. Aborted.");
        StopTowerSelection();
    }

    void DetectTowerCardSuccessPressed(TowerCardWidget cardToApply)
    {
        _gameTowersController.ApplyTowerCard(cardToApply.Data);
        StopTowerSelection();
    }

    void SellTower(TowerPlace towerPlace)
    {
        _gameCoinsController.AddGameCoins(towerPlace.CalculateCoinsIncomeForTowerDestroy());
        towerPlace.DestroyTower();
        StopTowerSelection();
    }

    void StopTowerSelection()
    {
        _gameUIController.ActivateManaScreen();
        _gameTowersController.StopSelection();
    }






















    
    void DetectLevelLost()
    {
        
    }
































    // Task runners logic
    bool _isTasksPending = false;

}










public enum GameTaskType
{
    LaunchLevelWin = 0,
    LaunchLastChance = 1,
    LaunchLevelLose = 2,
    LaunchWaveChange = 3,

    GameSettingsTask = 4,
}