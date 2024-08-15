using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;




public class InfoPopUpRoute
{
    string _customInfo;
    InfoPopUp _popUp;
    const string SCENE_NAME = "InfoPopUp";


    public void InitializeRoute(string customInfo)
    {
        _customInfo = customInfo;
    }

    public void StartRoute()
    {
        PopUpController.OpenPopUp<InfoPopUp>(
            "InfoPopUp", 
            (popUp) => 
            {

                // Basic popup logic
                _popUp = popUp;

                _popUp.OnPopUpOpened.RemoveAllListeners();
                _popUp.OnPopUpOpened.AddListener(DetectPopUpOpened);

                _popUp.OnPopUpClosed.RemoveAllListeners();
                _popUp.OnPopUpClosed.AddListener(DetectPopUpClosed);

                _popUp.OnClosePopUp_Clicked.RemoveAllListeners();
                _popUp.OnClosePopUp_Clicked.AddListener(TriggerClosePopUp);




                _popUp.Initialize(_customInfo);

                _popUp.AnimatePopUp_Show().Forget();
            });
    }



    void DetectPopUpOpened()
    {
        Debug.Log("[@@@] POPUP IS OPENED: " + SCENE_NAME);
    }
    
    void DetectPopUpClosed()
    {
        Debug.Log("[@@@] POPUP IS CLOSED: " + SCENE_NAME);
        PopUpController.ReleasePopUp(SCENE_NAME);
    }




    
    async void TriggerClosePopUp()
    {
        await _popUp.AnimatePopUp_Hide();
    }
}