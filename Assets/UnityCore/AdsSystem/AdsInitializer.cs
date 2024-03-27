using UnityEngine.Advertisements;
using Cysharp.Threading.Tasks;
using UnityEngine;
 
public class AdsInitializer : GeneralManager<AdsInitializer>, IUnityAdsInitializationListener
{

    // Ads service related data
    const string LOGGER_KEY = "[AdsInitializer]";
    const string AndroidGameId = "5584239";
    const string IOSGameId = "5584238";
    bool _testMode = true;
    private string _gameId;



    // Custom initialization values
    bool _isInitialized = false;
    bool _isAvailable = false;
    public bool IsAvailable => _isAvailable;
 




    public override async UniTask Initialize()
    {
        Debug.Log(LOGGER_KEY + " initialization started");
        InitializeAds();
        await UniTask.WaitUntil(() => _isInitialized);
        Debug.Log(LOGGER_KEY + " initialization finished");
    }
 

    void InitializeAds()
    {
        #if UNITY_IOS
                _gameId = IOSGameId;
        #elif UNITY_ANDROID
                _gameId = AndroidGameId;
        #elif UNITY_EDITOR
                _gameId = AndroidGameId; //Only for testing the functionality in the Editor
        #endif



        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    




    public void OnInitializationComplete()
    {
        // Debug.Log("Unity Ads initialization complete.");
        _isInitialized = true;
        _isAvailable = true;
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        // Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        _isInitialized = true;
        _isAvailable = false;
    }
}