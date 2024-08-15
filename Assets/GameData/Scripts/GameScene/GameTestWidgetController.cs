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
    }











    void GameTest_AddCoins(int coinsIncome) => _coinsController.AddGameCoins(coinsIncome);
    void GameTest_RemoveCoins(int amountToRemove) => _coinsController.TryRemoveCoins(amountToRemove);
}