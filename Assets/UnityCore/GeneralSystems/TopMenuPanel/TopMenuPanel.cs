using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMenuPanel : MonoBehaviour
{
    [SerializeField] CurrencyCoinsWidget _coinsWidget;
    [SerializeField] CurrencyCrystalsWidget _crystalsWidget;


    public void Initialize()
    {
        RefreshCurrencyPanel();

        CoinsManager.Instance.OnDataChanged_Coins.AddListener(() => _coinsWidget.SetCurrencyDisplay());
        CrystalsManager.Instance.OnDataChanged_Crystals.AddListener(() => _crystalsWidget.SetCurrencyDisplay());
    }

    void RefreshCurrencyPanel()
    {
        _coinsWidget.SetCurrencyDisplay();
        _crystalsWidget.SetCurrencyDisplay();
    }
}