using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;




public class PauseMenuPopIp : BasicPopUp
{
    [SerializeField] UniversalButton _returnHomeButton;




    [HideInInspector] public UnityEvent OnReturnHomeButtonCLicked = new UnityEvent();





    public void Initialize()
    {
        _closeButton.RemoveAllListeners();
        _closeButton.AddListener(OnClosePopUp_Clicked.Invoke);


        _returnHomeButton.RemoveAllListeners();
        _returnHomeButton.AddListener(OnReturnHomeButtonCLicked.Invoke);
    }
}