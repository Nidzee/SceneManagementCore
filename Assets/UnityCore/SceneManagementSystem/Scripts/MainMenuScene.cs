using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MainMenuScene : GameSceneHandler
{
    [SerializeField] TMP_Text _infoLabel;
    [SerializeField] Button _InterstitialButton;
    [SerializeField] Button _RewardVideoButton;



    public void SetPassingData(string passingData)
    {
        _infoLabel.text = passingData;
        _InterstitialButton.onClick.AddListener(() => LaunchInterstitial().Forget());
        _RewardVideoButton.onClick.AddListener(() => LaunchRewardedVideo().Forget());
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