using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    GameLevelConfig _levelConfig;
    GameSoundsConfig _gameSoundsConfig;
    GameInputHandler _gameInputHandler;
    CastleInfoUIHandler _castleInfoHandler;
    GameCameraController _gameCameraController;



    int _wavesTopIndex = 0;
    int _currentWaveIndex = 0;




    List<LevelIncomeResultData> _completedWavesIncomes = new List<LevelIncomeResultData>();







    public GameSceneController(
        ManaResourceHandler manaHandler,
        GameSceneTopPanelController topPanelController,
        GameTestWidgetController gameTestWidgetController,
        GameCoinsController gameCoinsController,
        GameTowersController gameTowersController,
        EnemySpawnerController enemySpawnerController,
        Castle castle,
        CastleInfoUIHandler castleInfoHandler,
        GameSceneTransferingData gameSceneTransferingData,
        GameUIController gameUIController,
        GameSoundsConfig gameSoundsConfig,
        GameLevelConfig levelConfig,
        GameCameraController gameCameraController,
        GameInputHandler gameInputHandler,
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
        _castleInfoHandler = castleInfoHandler;
        _levelConfig = levelConfig;
        _gameSoundsConfig = gameSoundsConfig;
        _towerInfoUIHandler = towerInfoUIHandler;
        _gameInputHandler = gameInputHandler;
        _gameCameraController = gameCameraController;
    }





    public void Initialize()
    {
        PauseController controller = new PauseController();

        _castle.Initalize();
        _manaHandler.Initialize();
        _topPanelController.Initialize();
        _gameCoinsController.Initialize();
        _gameTestWidgetController.Initialize(this);
        _towerCardsHandler.Initialize(_gameCoinsController);
        _gameTowersController.Initialize();
        _enemySpawnerController.Initialize();
        _gameUIController.Initalize();
        _towerInfoUIHandler.Initialize();
        _gameInputHandler.Initialize();
        _castleInfoHandler.Initialize();
        _gameCameraController.Initialize(_gameInputHandler);



        _castle.OnCastleClicked.RemoveAllListeners();
        _castle.OnCastleClicked.AddListener(DetectClickOnCastle);
        _gameTowersController.OnTowerPlaceClicked.RemoveAllListeners();
        _gameTowersController.OnTowerPlaceClicked.AddListener(DetectTowerPlaceSeleted);
        _castle.OnLevelLost.RemoveAllListeners();
        _castle.OnLevelLost.AddListener(DetectLevelLost);
        _enemySpawnerController.OnWaveFinished.RemoveAllListeners();
        _enemySpawnerController.OnWaveFinished.AddListener(AddWaveFinishedTask);
        _topPanelController.OnPauseButtonClicked.RemoveAllListeners();
        _topPanelController.OnPauseButtonClicked.AddListener(AddPausePopUpTask);


        _currentWaveIndex = 0;
        _wavesTopIndex = _levelConfig.EnemiesWavesConfig.Count-1;




        Debug.Log(_gameSceneTransferingData.GetSOmeData());

        LaunchGameLogic();
    }

    void LaunchGameLogic()
    {

        AudioManager.Instance.TryToPlayMusic(_gameSoundsConfig.GetgameSceneMusic());

        // Start resource gathering
        _manaHandler.StartResourceGathering();

       LaunchWave();
    }

    void LaunchWave()
    {
        AudioManager.Instance.PlaySound(_gameSoundsConfig.GetSoundByType(GameSoundType.WaveStart));

        // Get this wave
        var waveData = _levelConfig.EnemiesWavesConfig[_currentWaveIndex];

        // Initialize batch
        _enemySpawnerController.SetBatchForEnemiesSpawner(waveData);
        _enemySpawnerController.StartEnemyWaveSpawnLogic();
    }



































































    // Logic when wave is finished
    void AddWaveFinishedTask()
    {

        var currentWave = _levelConfig.EnemiesWavesConfig[_currentWaveIndex];
        _completedWavesIncomes.Add(currentWave.WaveWinIncome);


        _currentWaveIndex++;


        // Try to trigger level win
        if (_currentWaveIndex > _wavesTopIndex)
        {
            AddLevelWinTask();
        }
        else
        {
            AddWaveChangeTask();
        }
    }










































    void DetectClickOnCastle()
    {
        _gameUIController.ActivateCastleInfoUI();
        _castle.ActivateSelectionOverlay();

        _castleInfoHandler.InitializeInfoForCastle(_castle);
        _castleInfoHandler.OnCloseButtonClicked.RemoveAllListeners();
        _castleInfoHandler.OnCloseButtonClicked.AddListener(StopTowerSelection);
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
        _castle.HideSelectionOverlay();
    }





















    





























    // Task runners logic
    bool _isTasksPending = false;
    Queue<GameTaskType> _tasks = new Queue<GameTaskType>();

    // Pause control section
    void PauseTheGame() => PauseController.PauseControllerRef.PauseTheGame();
    void ResumeTheGame() => PauseController.PauseControllerRef.ResumeTheGame();



    async UniTask TryToLaunchTasks()
    {

        // [0] Skip if no tasks required
        if (_tasks == null || _tasks?.Count <= 0)
        {
            _isTasksPending = false;
            ResumeTheGame();
            return;
        }



        
        // If any tasks provided -> pause the game
        _isTasksPending = true;
        PauseTheGame();





        // [1] Pop the top task
        var targetTaskType = _tasks.Dequeue();

        

        if (targetTaskType == GameTaskType.LaunchLevelWin)
        {
            await HandleLevelWinTask();
            // await TryToLaunchTasks();
            return;
        }

        if (targetTaskType == GameTaskType.LaunchLevelLose)
        {
            await LaunchLevelLoseTask();
            // await TryToLaunchTasks();
            return;
        }

        if (targetTaskType == GameTaskType.LaunchWaveChange)
        {
            LaunchWave();
            await TryToLaunchTasks();
            return;
        }
        
        if (targetTaskType == GameTaskType.PauseMenu)
        {
            bool result = await LaunchPauseMenuPopUp();
            if (!result)
                await TryToLaunchTasks();
        }
    }

    void AddLevelWinTask() => AddTask(GameTaskType.LaunchLevelWin);
    void AddWaveChangeTask() => AddTask(GameTaskType.LaunchWaveChange);
    void DetectLevelLost() => AddTask(GameTaskType.LaunchLevelLose);
    void AddPausePopUpTask() => AddTask(GameTaskType.PauseMenu);
    
    void AddTask(GameTaskType task)
    {
        _tasks.Enqueue(task);
        if (!_isTasksPending)
        {
            TryToLaunchTasks().Forget();
        }
    }



    public void GAMETEST_Win() => AddLevelWinTask();
    public void GAMETEST_Lose() => DetectLevelLost();


























    
    async UniTask HandleLevelWinTask()
    {

        // [1] Launch music
        AudioManager.Instance.TryToRemoveMusic();
        AudioManager.Instance.PlaySound(_gameSoundsConfig.GetSoundByType(GameSoundType.LevelWin));


        // [2] Add income
        // var levelWinIncomeData = GetLevelFinishResources();
        // AddResourcesIncomeForLevel(levelWinIncomeData);
        // SaveThisLevelAsFinished();
        // LaunchDataToSave();



        // [3] Launch popup
        LevelWinPopUpRoute route = new LevelWinPopUpRoute();
        route.StartRoute();
    }
    

    async UniTask LaunchLevelLoseTask()
    {

        var currentWave = _levelConfig.EnemiesWavesConfig[_currentWaveIndex];
        _completedWavesIncomes.Add(currentWave.WaveLoseIncome);


        AudioManager.Instance.TryToRemoveMusic();
        AudioManager.Instance.PlaySound(_gameSoundsConfig.GetSoundByType(GameSoundType.LevelLose));


        // var levelLoseResourcesIncomeData = GetLevelFinishResources();
        // AddResourcesIncomeForLevel(levelLoseResourcesIncomeData);
        // LaunchDataToSave();


        // [3] Launch popup
        LevelLosePopUpRoute route = new LevelLosePopUpRoute();
        route.StartRoute();
    }

    async UniTask<bool> LaunchPauseMenuPopUp()
    {
        PauseMenuPopIpRoute route = new PauseMenuPopIpRoute();
        route.StartRoute();

        return await route.CallbackTryToQuitLevel.WaitForEventAsync();
    }
}










public enum GameTaskType
{
    LaunchWaveChange = 0,
    LaunchLevelWin = 1,
    LaunchLevelLose = 2,
    PauseMenu = 3,
}