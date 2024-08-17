using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;




public class TowerCardsHandler : MonoBehaviour
{
    [SerializeField] List<UniversalTowerConfig> _TEST_TowerCards = new List<UniversalTowerConfig>();
    [SerializeField] List<TowerCardWidget> _availableWidgets = new List<TowerCardWidget>();
    [SerializeField] Button _closeButton;


    GameCoinsController _coinsController;




    [HideInInspector] public UnityEvent<TowerCardWidget> OnSuccessCardSelected = new UnityEvent<TowerCardWidget>();
    [HideInInspector] public UnityEvent OnCloseClicked = new UnityEvent();






    public void Initialize(GameCoinsController coinsController)
    {

        _coinsController = coinsController;




        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(OnCloseClicked.Invoke);




        // Hide all widgets
        foreach (var widget in _availableWidgets)
            widget.HideWidget();


        

        int cardsAmount = _TEST_TowerCards.Count;
        int widgetsAmount = _availableWidgets.Count;
        int minValue = Mathf.Min(cardsAmount, widgetsAmount);
        for (int i = 0; i < minValue; i++)
        {
            var widget = _availableWidgets[i];
            var card = _TEST_TowerCards[i];

            widget.Initialize(card);

            widget.OnTowerCardWidgetClicked.RemoveAllListeners();
            widget.OnTowerCardWidgetClicked.AddListener(TryToUseCard);
        }


        _coinsController.OnGameCoinsUpdated.AddListener(NotifyCardsCoinsAmountChanged);
        NotifyCardsCoinsAmountChanged(_coinsController.ActualCoinsAmount);
    }

    void NotifyCardsCoinsAmountChanged(int coinsAmount)
    {
        foreach (var widget in _availableWidgets)
        {
            widget.NotifyCoinsResourcesUpdated(coinsAmount);
        }
    }



    // Cards respond logic
    public void TryToUseCard(TowerCardWidget widget)
    {
        int actualCoinsAmount = _coinsController.ActualCoinsAmount;
        int towerPrice = widget.Data.PriceCost;

        if (actualCoinsAmount < towerPrice)
        {
            Debug.Log("[###] NOT ENOUGH COINS");
            return;
        }


        _coinsController.TryRemoveCoins(towerPrice);
        OnSuccessCardSelected.Invoke(widget);
    }
}