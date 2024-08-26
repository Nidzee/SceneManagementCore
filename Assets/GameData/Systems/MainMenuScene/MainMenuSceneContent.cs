using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;






public class MainMenuSceneContent : MonoBehaviour
{
    [SerializeField] UniversalButton _playButton;




    public void Initialize()
    {
        _playButton.RemoveAllListeners();
        _playButton.AddListener(LaunchGameScene);
    }








    void LaunchGameScene()
    {
        SceneLoader.LoadScene<GameSceneHandler>("GameScene", SceneLoader.LoadingAnimationType.WithAnimation).Forget();
    }













    // Testing methods
    async UniTask LaunchInterstitial()
    {
        Debug.Log("[###] START INTERSTITIAL");
        await InterstitialsManager.Instance.TryToShowInterstitial(
            (status) => 
            {
                if (status)
                {
                    Debug.Log("[###] INTERSTITIAL FINISHED -> SUCCESS");
                }
                else
                {
                    Debug.Log("[###] INTERSTITIAL FINISHED -> FAIL");
                }
            });
    }
    
    async UniTask LaunchRewardedVideo()
    {
        Debug.Log("[###] START RV");
        await RewardedAdsManager.Instance.TryToShowRV(
            (status) => 
            {
                if (status)
                {
                    Debug.Log("[###] RV FINISHED -> SUCCESS");
                }
                else
                {
                    Debug.Log("[###] RV FINISHED -> FAIL");
                }
            });
    }
}