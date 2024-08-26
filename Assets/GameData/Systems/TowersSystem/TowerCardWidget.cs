using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerCardWidget : MonoBehaviour
{
    [SerializeField] Transform _contentHolder;
    [SerializeField] Image _cardIcon;
    [SerializeField] TMP_Text _amountNeedLabel;
    [SerializeField] Button _widgetButton;

    UniversalTowerConfig _data;
    public UniversalTowerConfig Data => _data;

    [HideInInspector] public UnityEvent<TowerCardWidget> OnTowerCardWidgetClicked = new UnityEvent<TowerCardWidget>();





    public void HideWidget()
    {
        _contentHolder.gameObject.SetActive(false);
    }

    public void Initialize(UniversalTowerConfig data)
    {
        _contentHolder.gameObject.SetActive(true);


        _data = data;

        _amountNeedLabel.text = _data.PriceCost.ToString();
        _cardIcon.sprite = _data.Icon;

        _widgetButton.onClick.RemoveAllListeners();
        _widgetButton.onClick.AddListener(TriggerCardClick);
    }

    public void NotifyCoinsResourcesUpdated(int currentAmount)
    {
        if (_data == null)
            return;


        if (currentAmount >= _data.PriceCost)
        {
            _amountNeedLabel.color = Color.white;
        }
        else
        {
            _amountNeedLabel.color = Color.red;
        }
    }

    void TriggerCardClick()
    {
        OnTowerCardWidgetClicked.Invoke(this);
    }
}