using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;






public class TutorialOverlay : MonoBehaviour
{
    public Image ScreenRect;
    [SerializeField] Image _tutorialShade;
    [SerializeField] RectTransform _unmaskArea;




    public void ResetTutorial()
    {
        _tutorialShade.DOFade(0,0);
        _unmaskArea.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void ActivateTutorial()
    {
        _tutorialShade.DOFade(0.3f,0);
        _unmaskArea.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    public void PlaceUnmask(Vector3 targetPos, Vector2 sizeDelta)
    {
        _unmaskArea.transform.localPosition = targetPos;
        _unmaskArea.sizeDelta = sizeDelta;
    }
}