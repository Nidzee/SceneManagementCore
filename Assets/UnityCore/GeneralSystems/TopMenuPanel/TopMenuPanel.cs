using System.Collections.Generic;
using System.Collections;
using UnityEngine;







public class TopMenuPanel : MonoBehaviour
{
    [SerializeField] CurrencyCoinsWidget _coinsWidget;
    [SerializeField] CurrencyCrystalsWidget _crystalsWidget;
    [SerializeField] UniversalButton _settingsPopUp;




    public void Initialize()
    {
        RefreshCurrencyPanel();

        CoinsManager.Instance.OnDataChanged_Coins.AddListener(() => _coinsWidget.SetCurrencyDisplay());
        CrystalsManager.Instance.OnDataChanged_Crystals.AddListener(() => _crystalsWidget.SetCurrencyDisplay());

        _settingsPopUp.RemoveAllListeners();
        _settingsPopUp.AddListener(DetectClickOnSettingsPopUp);
    }

    void RefreshCurrencyPanel()
    {
        _coinsWidget.SetCurrencyDisplay();
        _crystalsWidget.SetCurrencyDisplay();
    }

    void DetectClickOnSettingsPopUp()
    {
        GameSettingsRoute route = new GameSettingsRoute();
        route.StartRoute();
    }
}