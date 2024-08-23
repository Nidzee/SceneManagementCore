using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;








public class LevelLosePopUpRoute
{
    string _customInfo;
    LevelLosePopUp _popUp;
    const string SCENE_NAME = "LevelLosePopUp";


    public void StartRoute()
    {
        PopUpController.OpenPopUp<LevelLosePopUp>(
            "LevelLosePopUp", 
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