using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;






public class ManaResourceHandler : MonoBehaviour
{
    [SerializeField] float _resourceGatherTime;
    [SerializeField] int _startAmount;
    [SerializeField] ManaResourceWidget _widget;
    [SerializeField] ManaCardsController _cardsController;

    [SerializeField] List<ManaAbilityCardData> _TEST_Abilities = new List<ManaAbilityCardData>();


    [HideInInspector] public UnityEvent<int> OnManaAmountUpdated = new UnityEvent<int>();

    bool _isWorking = false;
    bool _isPaused = false;
    float _currentTime;
    int _resourcesAmount;







    public void Initialize()
    {
        // During initialization timer is not working
        _isWorking = false;
        _isPaused = false;


        // Init resource amount data
        _resourcesAmount = _startAmount;


        // Init timer data
        _currentTime = 0;
        float progress = 0f;


        OnManaAmountUpdated.RemoveAllListeners();
        OnManaAmountUpdated.AddListener(NotifyResourcesUpdated);

        // Initialize widget
        _widget.Initialize(_resourcesAmount, progress);

        // Initialize cards controller
        _cardsController.Initialize(this, _TEST_Abilities);
        _cardsController.OnManaAbilityCardClick.RemoveAllListeners();
        _cardsController.OnManaAbilityCardClick.AddListener(TryToUseCard);
        OnManaAmountUpdated.Invoke(_resourcesAmount);



        PauseController.PauseControllerRef.OnPauseEmited.AddListener(Pause);
        PauseController.PauseControllerRef.OnResumeEmited.AddListener(Resume);
    }



    public void Update()
    {
        UpdateGatherTimer();
    }

    void UpdateGatherTimer()
    {
        if (!_isWorking)
            return;

        if (_isPaused)
            return;

        if (_currentTime > _resourceGatherTime)
            return;



        _currentTime += Time.deltaTime;
        if (_currentTime >= _resourceGatherTime)
            _currentTime = _resourceGatherTime;


        float progressValue = _currentTime / _resourceGatherTime;
        _widget.UpdateProgressBar(progressValue);

        if (progressValue >= 1)
        {
            ProduceResource();
        }
    }

    void ProduceResource()
    {
        _resourcesAmount++;
        OnManaAmountUpdated.Invoke(_resourcesAmount);

        _currentTime = 0;
    }







    void NotifyResourcesUpdated(int amount)
    {
        _widget.UpdateResourcesAmount(amount);
        _cardsController.ManaAmountUpdated(amount);
    }





    // Cards respond logic
    public void TryToUseCard(ManaAbilityCardWidget widget)
    {
        var needMana = widget.Data.ManaAmountNeed;
        if (_resourcesAmount < needMana)
        {
            Debug.Log("[###] NOT ENOUGH MANA");
            return;
        }
        

        Debug.Log("[###] TRIGGER MANA ABILITY: " + widget.Data.ManaAbilityCardType.ToString());

        _resourcesAmount -= needMana;
        OnManaAmountUpdated.Invoke(_resourcesAmount);
    }









    void Pause() => _isPaused = true;
    void Resume() => _isPaused = false;










    // Control methods
    public void StartResourceGathering()
    {
        _isWorking = true;
    }
    
    public void StopResourceGathering()
    {
        _isWorking = false;
    }
}