using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using UnityEngine;




public class GameCoinsController
{
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
        _gameCoinsAmount = 0;
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