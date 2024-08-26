using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TMPro;






public class LevelWinPopUp : BasicPopUp
{
    [SerializeField] TMP_Text _infoLabel;
    [SerializeField] UniversalButton _homeButton;


    [HideInInspector] public UnityEvent OnHomeButtonClicked = new UnityEvent();






    public void Initialize()
    {
        // _closeButton.RemoveAllListeners();
        // _closeButton.AddListener(() => OnClosePopUp_Clicked.Invoke());


        _homeButton.RemoveAllListeners();
        _homeButton.AddListener(OnHomeButtonClicked.Invoke);
    }
}