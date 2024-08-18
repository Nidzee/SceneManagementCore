using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;





public class TowerInfoUIHandler : MonoBehaviour
{

    [Header("Buttons config")]
    [SerializeField] Button _closeButton;
    [SerializeField] Button _sellButton;


    [SerializeField] TMP_Text _towerName;
    [SerializeField] Image _towerIcon;
    [SerializeField] TMP_Text _damageLabel;
    [SerializeField] TMP_Text _rangeLabel;


    TowerPlace _thisTowerPlace;

    // Events config
    [HideInInspector] public UnityEvent OnCloseButtonClicked = new UnityEvent();
    [HideInInspector] public UnityEvent<TowerPlace> OnSellButtonClicked = new UnityEvent<TowerPlace>();




    public void Initialize()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(DetectClick_CloseButton);

        _sellButton.onClick.RemoveAllListeners();
        _sellButton.onClick.AddListener(DetectClick_SellTower);
    }

    public void InitializeInfoForTower(TowerPlace towerPlace)
    {
        _thisTowerPlace = towerPlace;

        var data = _thisTowerPlace.ThisTowerData;

        _towerName.text = data.Name;
        _towerIcon.sprite = data.Icon;
    }






    void DetectClick_CloseButton()
    {
        OnCloseButtonClicked.Invoke();
    }
    
    void DetectClick_SellTower()
    {
        OnSellButtonClicked.Invoke(_thisTowerPlace);
    }
}