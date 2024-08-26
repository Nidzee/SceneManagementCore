using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ManaCardsController : MonoBehaviour
{
    [SerializeField] List<ManaAbilityCardWidget> _cardsWidgets = new List<ManaAbilityCardWidget>();

    ManaResourceHandler _handler;

    [HideInInspector] public UnityEvent<ManaAbilityCardWidget> OnManaAbilityCardClick = new UnityEvent<ManaAbilityCardWidget>();


    public void Initialize(ManaResourceHandler handler, List<ManaAbilityCardData> cards)
    {

        _handler = handler;


        EnsureAllWidgetsHidden();




        int cardsAmount = cards.Count;
        int widgetsAmount = _cardsWidgets.Count;
        int minValue = Mathf.Min(cardsAmount, widgetsAmount);
        for (int i = 0; i < minValue; i++)
        {
            var widget = _cardsWidgets[i];
            var card = cards[i];

            widget.Initialize(card);

            widget.OnManaAbilityClicked.RemoveAllListeners();
            widget.OnManaAbilityClicked.AddListener(DetectCardClick);
        }


    }

    void DetectCardClick(ManaAbilityCardWidget widgetClicked)
    {
        OnManaAbilityCardClick.Invoke(widgetClicked);
    }

    public void ManaAmountUpdated(int newManaAmount)
    {
        foreach (var widget in _cardsWidgets)
        {
            widget.NotifyManaResourcesUpdated(newManaAmount);
        }
    }












    void EnsureAllWidgetsHidden()
    {
        foreach (var widget in _cardsWidgets)
            widget.HideWidget();
    }

}