using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;






public class MainMenuSceneContent : MonoBehaviour
{
    [SerializeField] Button _InterstitialButton;
    [SerializeField] Button _RewardVideoButton;

    [SerializeField] BasicButton _openPopUpButton;
    [SerializeField] BasicButton _playButton;




    public void Initialize()
    {
        _InterstitialButton.onClick.AddListener(() => LaunchInterstitial().Forget());
        _RewardVideoButton.onClick.AddListener(() => LaunchRewardedVideo().Forget());

        _openPopUpButton.RemoveAllListeners();
        _openPopUpButton.AddListener(LaunchInfoPopUp);

        _playButton.RemoveAllListeners();
        _playButton.AddListener(LaunchGameScene);
    }








    void LaunchGameScene()
    {
        SceneLoader.LoadScene<GameSceneHandler>("GameScene", SceneLoader.LoadingAnimationType.WithAnimation).Forget();
    }

    void LaunchInfoPopUp()
    {
        InfoPopUpRoute route = new InfoPopUpRoute();

        route.InitializeRoute("Nikitos popravliasia!");
        route.StartRoute();
    }

    async UniTask LaunchInterstitial()
    {
        Debug.Log("[###] START INTERSTITIAL");
        await InterstitialsManager.Instance.TryToShowInterstitial((status) => Debug.Log("[###] INTERSTITIAL FINISHED"));
    }
    
    async UniTask LaunchRewardedVideo()
    {
        Debug.Log("[###] START RV");
        await RewardedAdsManager.Instance.TryToShowRV((status) => Debug.Log("[###] RV FINISHED: " + status));
    }
}