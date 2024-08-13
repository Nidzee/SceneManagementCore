using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScene : GameSceneHandler
{
    [SerializeField] BottomMenuPanel _bottomMenuPanel;
    [SerializeField] TopMenuPanel _topMenuPanel;
    [SerializeField] Button _InterstitialButton;
    [SerializeField] Button _RewardVideoButton;

    [SerializeField] BasicButton _openPopUpButton;




    public override void Start()
    {
        base.Start();

        _bottomMenuPanel.Initialize();
        _topMenuPanel.Initialize();

        _InterstitialButton.onClick.AddListener(() => LaunchInterstitial().Forget());
        _RewardVideoButton.onClick.AddListener(() => LaunchRewardedVideo().Forget());


        _openPopUpButton.OnClick.RemoveAllListeners();
        _openPopUpButton.OnClick.AddListener(LaunchInfoPopUp);
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