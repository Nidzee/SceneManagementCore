using System.Collections.Generic;
using System.Collections;
using UnityEngine;





public class GameTestWidgetController
{
    GameTestWidget _gameTestWidget;
    GameCoinsController _coinsController;





    public GameTestWidgetController(
        GameTestWidget gameTestWidget,
        GameCoinsController coinsController
    )
    {
        _gameTestWidget = gameTestWidget;
        _coinsController = coinsController;
    }


    public void Initialize()
    {
        _gameTestWidget.Initalize();

        _gameTestWidget.OnCoinsTriggered_Add.RemoveAllListeners();
        _gameTestWidget.OnCoinsTriggered_Add.AddListener(GameTest_AddCoins);

        
        _gameTestWidget.OnCoinsTriggered_Remove.RemoveAllListeners();
        _gameTestWidget.OnCoinsTriggered_Remove.AddListener(GameTest_RemoveCoins);


        _gameTestWidget.OnPauseEmited.RemoveAllListeners();
        _gameTestWidget.OnPauseEmited.AddListener(GameTest_UpdatePauseStatus);
    }











    void GameTest_AddCoins(int coinsIncome) => _coinsController.AddGameCoins(coinsIncome);
    void GameTest_RemoveCoins(int amountToRemove) => _coinsController.TryRemoveCoins(amountToRemove);

    void GameTest_UpdatePauseStatus(ButtonStatus status)
    {
        if (status == ButtonStatus.Active)
        {
            PauseController.PauseControllerRef.ResumeTheGame();
            return;
        }
        
        if (status == ButtonStatus.Passive)
        {
            PauseController.PauseControllerRef.PauseTheGame();
            return;
        }
    }
}