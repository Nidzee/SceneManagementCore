using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using UnityEngine;




public class GameCoinsController
{
    const int START_COINS_AMOUNT = 500;
    GameCoinsWidget _gameCoinsWidget;
    int _gameCoinsAmount;
    public int ActualCoinsAmount => _gameCoinsAmount;
    public UnityEvent<int> OnGameCoinsUpdated = new UnityEvent<int>();







    public GameCoinsController(
        GameCoinsWidget gameCoinsWidget
    )
    {
        _gameCoinsWidget = gameCoinsWidget;
    }


    public void Initialize()
    {
        _gameCoinsAmount = START_COINS_AMOUNT;
        _gameCoinsWidget.Initialize();

        OnGameCoinsUpdated.RemoveAllListeners();
        OnGameCoinsUpdated.AddListener(_gameCoinsWidget.NotifyCoinsUpdated);

        NotifyCoinsUpdated();
    }











    public void AddGameCoins(int coinsIncome)
    {
        _gameCoinsAmount += coinsIncome;
        NotifyCoinsUpdated();
    }

    public void TryRemoveCoins(int amountToRemove)
    {
        if (_gameCoinsAmount < amountToRemove)
            return;

        _gameCoinsAmount -= amountToRemove;
        NotifyCoinsUpdated();
    }

    void NotifyCoinsUpdated()
    {
        OnGameCoinsUpdated.Invoke(_gameCoinsAmount);
    }
}