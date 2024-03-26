using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static SceneLoader;



public interface ILoadingScreen
{
    public UniTask ShowLoadingScreen(LoadingAnimationType animationType);
    public UniTask HideLoadingScreen();
    public void SetProgress(float progress);
}


public class LoadingScreen : MonoBehaviour, ILoadingScreen
{
    [SerializeField] Slider _progressBar;
    [SerializeField] TMP_Text _progressLabel;
    [SerializeField] CanvasGroup _canvasGroup;







    public async UniTask HideLoadingScreen()
    {
        await _canvasGroup.DOFade(0, 0.2f).AsyncWaitForCompletion();
    }

    public async UniTask ShowLoadingScreen(LoadingAnimationType animationType)
    {
        if (animationType == LoadingAnimationType.WithAnimation)
        {
            _canvasGroup.alpha = 0;
            await _canvasGroup.DOFade(1, 0.2f).AsyncWaitForCompletion();
            return;
        }
        
        
        if (animationType == LoadingAnimationType.NoAnimation)
        {
            _canvasGroup.alpha = 1;
            await _canvasGroup.DOFade(1, 0.2f).AsyncWaitForCompletion();
            return;
        }
        

        Debug.LogError("[###] Loading screen animation not supported");
    }






    public void SetProgress(float progress)
    {
        if (progress < 0)
        {
            progress = 0;
        }


        UpgradeProgress(progress);
    }

    public void UpgradeProgress(float progress)
    {
        _progressBar.value = progress;
        _progressLabel.text = "Loading: " + Mathf.CeilToInt(progress * 100) + "%";
    }
}