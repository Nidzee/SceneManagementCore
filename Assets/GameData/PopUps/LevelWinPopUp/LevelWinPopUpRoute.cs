using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;








public class LevelWinPopUpRoute
{
    string _customInfo;
    LevelWinPopUp _popUp;
    const string SCENE_NAME = "LevelWinPopUp";


    public void StartRoute()
    {
        PopUpController.OpenPopUp<LevelWinPopUp>(
            "LevelWinPopUp", 
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

                _popUp.OnHomeButtonClicked.RemoveAllListeners();
                _popUp.OnHomeButtonClicked.AddListener(TriggerHomeButtonClicked);





                _popUp.Initialize();

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

    void TriggerHomeButtonClicked()
    {
        SceneLoader.LoadScene<MainMenuScene>("MainMenuScene", SceneLoader.LoadingAnimationType.WithAnimation).Forget();
    }
}