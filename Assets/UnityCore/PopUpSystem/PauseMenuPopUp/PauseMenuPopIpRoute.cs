using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;




public class PauseMenuPopIpRoute
{
    PauseMenuPopIp _popUp;
    const string SCENE_NAME = "PauseMenuPopUp";


    public void StartRoute()
    {
        PopUpController.OpenPopUp<PauseMenuPopIp>(
            SCENE_NAME, 
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




                _popUp.Initialize();

                _popUp.AnimatePopUp_Show().Forget();

                _popUp.OnReturnHomeButtonCLicked.RemoveAllListeners();
                _popUp.OnReturnHomeButtonCLicked.AddListener(DetectReturnHomeEmited);
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

    void DetectReturnHomeEmited()
    {
        SceneLoader.LoadScene<MainMenuScene>("MainMenuScene", SceneLoader.LoadingAnimationType.WithAnimation).Forget();
    }




    
    async void TriggerClosePopUp()
    {
        await _popUp.AnimatePopUp_Hide();
    }
}
