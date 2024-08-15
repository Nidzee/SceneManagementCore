using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScene : GeneralSceneHandler
{
    [SerializeField] BottomMenuPanel _bottomMenuPanel;
    [SerializeField] TopMenuPanel _topMenuPanel;
    [SerializeField] Button _InterstitialButton;
    [SerializeField] Button _RewardVideoButton;

    [SerializeField] BasicButton _openPopUpButton;
    [SerializeField] BasicButton _playButton;




    public override void Start()
    {
        base.Start();

        _bottomMenuPanel.Initialize();
        _topMenuPanel.Initialize();

        _InterstitialButton.onClick.AddListener(() => LaunchInterstitial().Forget());
        _RewardVideoButton.onClick.AddListener(() => LaunchRewardedVideo().Forget());


        _openPopUpButton.OnClick.RemoveAllListeners();
        _openPopUpButton.OnClick.AddListener(LaunchInfoPopUp);

        _playButton.OnClick.RemoveAllListeners();
        _playButton.OnClick.AddListener(LaunchGameScene);
    }

    public void EXTERNAL_TEST()
    {
        Debug.Log("EXTERNAL TEST");
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