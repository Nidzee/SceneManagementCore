using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.Events;
using DG.Tweening;




public class BasicPopUp : MonoBehaviour
{
    [SerializeField] protected Image _blackOverlay;
    [SerializeField] protected RectTransform _contentHolder;

    [SerializeField] protected BasicButton _closeButton;

    string _sceneName;


    const float BACKGROUND_FADE_IN_TIME_S = 0.5f;
    const float CONTENT_SCALE_FULL_TIME_S = 0.5f;

    const float BACKGROUND_FADE_OUT_TIME_S = 0.2f;
    const float CONTENT_SCALE_HIDDEN_TIME_S = 0.2f;





    [HideInInspector] public UnityEvent OnPopUpOpened = new UnityEvent();
    [HideInInspector] public UnityEvent OnPopUpClosed = new UnityEvent();
    [HideInInspector] public UnityEvent OnClosePopUp_Clicked = new UnityEvent();








    public void InitSceneData(string sceneName) => _sceneName = sceneName;
    



    public async UniTask AnimatePopUp_Show(Action onShowFinished = null)
    {
        // Prepare logic
        _contentHolder.transform.localScale = Vector3.zero;
        _blackOverlay.DOFade(0,0);


        // Animation logic
        var sequence = DOTween.Sequence();
        sequence.Append(_blackOverlay.DOFade(0.5f, BACKGROUND_FADE_IN_TIME_S));
        sequence.Join(_contentHolder.transform.DOScale(1, CONTENT_SCALE_FULL_TIME_S).SetEase(Ease.OutBack));
        await sequence.AsyncWaitForCompletion();


        // Fire event that popup is opened
        OnPopUpOpened.Invoke();
        onShowFinished?.Invoke();
    }

    public async UniTask AnimatePopUp_Hide(Action onHideFinished = null)
    {
        
        // Animation logic
        var sequence = DOTween.Sequence();
        sequence.Append(_blackOverlay.DOFade(0f, BACKGROUND_FADE_OUT_TIME_S));
        sequence.Join(_contentHolder.transform.DOScale(0, CONTENT_SCALE_HIDDEN_TIME_S).SetEase(Ease.InBack));
        await sequence.AsyncWaitForCompletion();


        // Fire event that popup is closed
        OnPopUpClosed.Invoke();
        onHideFinished?.Invoke();
    }
}