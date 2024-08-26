using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;




public class ManaAbilityCardWidget : MonoBehaviour
{
    [SerializeField] Transform _contentHolder;
    [SerializeField] Image _cardIcon;
    [SerializeField] TMP_Text _amountNeedLabel;
    [SerializeField] Button _widgetButton;

    ManaAbilityCardData _data;
    public ManaAbilityCardData Data => _data;

    [HideInInspector] public UnityEvent<ManaAbilityCardWidget> OnManaAbilityClicked = new UnityEvent<ManaAbilityCardWidget>();





    public void HideWidget()
    {
        _contentHolder.gameObject.SetActive(false);
    }

    public void Initialize(ManaAbilityCardData data)
    {
        _contentHolder.gameObject.SetActive(true);


        _data = data;

        _amountNeedLabel.text = _data.ManaAmountNeed.ToString();
        _cardIcon.sprite = _data.Icon;

        _widgetButton.onClick.RemoveAllListeners();
        _widgetButton.onClick.AddListener(TriggerCardClick);
    }

    public void NotifyManaResourcesUpdated(int currentAmount)
    {
        if (_data == null)
            return;


        if (currentAmount >= _data.ManaAmountNeed)
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
        OnManaAbilityClicked.Invoke(this);
    }
}