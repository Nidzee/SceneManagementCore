using UnityEngine.Advertisements;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using System;



// ---------------------------------------------------------------------
// TODO: ADD analytics after interstitial is shown or not and log status

public class InterstitialsManager : GeneralManager<InterstitialsManager>, IUnityAdsLoadListener, IUnityAdsShowListener
{

    // General values
    const string LOGGER_KEY = "[InterstitialsManager]";
    const string AndroidAdUnitId = "Interstitial_Android";
    const string IOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;





    // Interstitial load values
    bool _isInterstitialLoadFinished = false;
    bool _loadStatus = false;



    // Interstitial show values
    bool _interstitialFinished = false;









    public override async UniTask Initialize()
    {
        Debug.Log(LOGGER_KEY + " initialization start");
        Init();
        await UniTask.Yield();
        Debug.Log(LOGGER_KEY + " initialization finish");
    }

    void Init()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? IOsAdUnitId
            : AndroidAdUnitId;
    }








    public async UniTask TryToShowInterstitial(Action<bool> callback = null)
    {

        // Skip if ads not initialized
        if (!AdsInitializer.Instance.IsAvailable)
        {
            Debug.LogError(LOGGER_KEY + " interstitial show fail: Ads not initialized");
            return;
        }


        // Reset status values
        _isInterstitialLoadFinished = false;
        _loadStatus = false;
        _interstitialFinished = false;


        // Wait for loading
        LoadInterstitial();
        await UniTask.WaitUntil(() => _isInterstitialLoadFinished);



        // Skip if ad was not loaded
        if (!_loadStatus)
        {
            Debug.LogError(LOGGER_KEY + " loading interstitial failed -> aborted");
            return;
        }



        // Wait for interstitial to show
        ShowInterstitial();
        await UniTask.WaitUntil(() => _interstitialFinished);



        // Launch signal callback
        callback?.Invoke(true);
    }








    
    public void LoadInterstitial()
    {
        Advertisement.Load(_adUnitId, this);
    }
 
    public void ShowInterstitial()
    {
        Advertisement.Show(_adUnitId, this);
        LoadInterstitial();
    }
 







    // Interstitial loading callbacks
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Debug.Log("[###] AD IS LOADED");
        _isInterstitialLoadFinished = true;
        _loadStatus = true;
    }
 
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        // Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        _isInterstitialLoadFinished = true;
        _loadStatus = false;
    }
 








    // Interstitial show callbacks
    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        // Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        _interstitialFinished = true;
    }
 
    public void OnUnityAdsShowStart(string _adUnitId) 
    { 

    }

    public void OnUnityAdsShowClick(string _adUnitId) 
    { 

    }
    
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
    {
        _interstitialFinished = true;
    }
}