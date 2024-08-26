using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TMPro;






public class LevelWinPopUp : BasicPopUp
{
    [SerializeField] TMP_Text _infoLabel;
    [SerializeField] UniversalButton _homeButton;
    [SerializeField] TutorialOverlay _overlay;


    [HideInInspector] public UnityEvent OnHomeButtonClicked = new UnityEvent();






    public void Initialize()
    {
        // _closeButton.RemoveAllListeners();
        // _closeButton.AddListener(() => OnClosePopUp_Clicked.Invoke());


        _homeButton.RemoveAllListeners();
        _homeButton.AddListener(OnHomeButtonClicked.Invoke);



        _overlay.ResetTutorial();
        OnPopUpOpened.AddListener(StartTestTutorial);
    }

    void StartTestTutorial()
    {
        _overlay.ActivateTutorial();
        Vector3 sizeOfRect = _infoLabel.rectTransform.rect.size + new Vector2(15,15);
        Vector3 targetPos = _overlay.ScreenRect.transform.InverseTransformDirection(_infoLabel.transform.localPosition);


        Debug.Log("[CORRECT]: " + _overlay.ScreenRect.transform.InverseTransformDirection(_infoLabel.transform.localPosition));
        Debug.Log("[CORRECT]: " + _overlay.ScreenRect.transform.InverseTransformPoint(_infoLabel.transform.position));


        _overlay.PlaceUnmask(targetPos, sizeOfRect);
    }
}