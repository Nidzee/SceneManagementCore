using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;




public class PauseMenuPopIp : BasicPopUp
{
    [SerializeField] BasicButton _returnHomeButton;




    [HideInInspector] public UnityEvent OnReturnHomeButtonCLicked = new UnityEvent();





    public void Initialize()
    {
        _closeButton.OnClick.RemoveAllListeners();
        _closeButton.OnClick.AddListener(OnClosePopUp_Clicked.Invoke);


        _returnHomeButton.OnClick.RemoveAllListeners();
        _returnHomeButton.OnClick.AddListener(OnReturnHomeButtonCLicked.Invoke);
    }
}