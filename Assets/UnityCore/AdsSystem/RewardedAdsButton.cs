using UnityEngine.Advertisements;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using System;

public class RewardedAdsManager : GeneralManager<RewardedAdsManager>, IUnityAdsLoadListener, IUnityAdsShowListener
{
    const string LOGGER_KEY = "[RewardedAds-Manager]";
    const string AndroidAdUnitId = "Rewarded_Android";
    const string IOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms
 




    bool _isRVLoadFinished;
    bool _loadStatus;
    bool _RVFinished;
    bool _RVFinishedStatus;




    // Events
    public UnityEvent<bool> OnRVFinished = new UnityEvent<bool>();






    // Initialization logic
    public override async UniTask Initialize()
    {   
        Debug.Log(LOGGER_KEY + " initialization-start");
        Init();
        await UniTask.Yield();
        Debug.Log(LOGGER_KEY + " initialization-finished");
    }
 
    void Init()
    {
        #if UNITY_IOS
                _adUnitId = IOSAdUnitId;
        #elif UNITY_ANDROID
                _adUnitId = AndroidAdUnitId;
        #endif
    }






    public void LoadRewardedVideo()
    {
        Advertisement.Load(_adUnitId, this);
    }
 
    public void ShowRewardedVideo()
    {
        Advertisement.Show(_adUnitId, this);
    }
 
 









   

    public async UniTask TryToShowRV(Action<bool> callback = null)
    {

        // Skip if ads not initialized
        if (!AdsInitializer.Instance.IsAvailable)
        {
            Debug.LogError(LOGGER_KEY + " RV show fail: Ads not initialized");
            return;
        }


        // Reset status values
        _isRVLoadFinished = false;
        _loadStatus = false;
        _RVFinished = false;
        _RVFinishedStatus = false;


        // Wait for loading
        LoadRewardedVideo();
        await UniTask.WaitUntil(() => _isRVLoadFinished);



        // Skip if ad was not loaded
        if (!_loadStatus)
        {
            Debug.LogError(LOGGER_KEY + " loading RV failed -> aborted");
            return;
        }



        // Wait for interstitial to show
        ShowRewardedVideo();
        await UniTask.WaitUntil(() => _RVFinished);



        // Launch signal callback
        callback?.Invoke(_RVFinishedStatus);
    }























    // Load callbacks
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        _isRVLoadFinished = true;
        _loadStatus = false;
    }
 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            // ???
        }

        _isRVLoadFinished = true;
        _loadStatus = true;
    }
 
 









    // Show callbacks
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {

        // Determone is RV completed
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            // Debug.Log("Unity Ads Rewarded Ad Completed");
            _RVFinishedStatus = true;
        } 
        else
        {
            _RVFinishedStatus = false;
        }


        _RVFinished = true;
    }
    
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        // Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        _RVFinished = true;
        _RVFinishedStatus = false;
    }










    
    public void OnUnityAdsShowStart(string adUnitId) 
    { 

    }
    
    public void OnUnityAdsShowClick(string adUnitId) 
    {

    }
}