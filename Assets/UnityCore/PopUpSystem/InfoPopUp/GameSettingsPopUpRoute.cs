using Cysharp.Threading.Tasks;
using UnityEngine;




public class GameSettingsRoute
{
    string _customInfo;
    GameSettingsPopUp _popUp;
    const string SCENE_NAME = "GameSettingsPopUp";


    public void InitializeRoute(string customInfo)
    {
        _customInfo = customInfo;
    }

    public void StartRoute()
    {
        PopUpController.OpenPopUp<GameSettingsPopUp>(
            "GameSettingsPopUp", 
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

                _popUp.OnButtonTriggered_Vibrations.RemoveAllListeners();
                _popUp.OnButtonTriggered_Vibrations.AddListener(HandleUpdateOfVibrations);

                _popUp.OnButtonTriggered_Sounds.RemoveAllListeners();
                _popUp.OnButtonTriggered_Sounds.AddListener(HandleUpdateOfSounds);
                
                _popUp.OnButtonTriggered_Music.RemoveAllListeners();
                _popUp.OnButtonTriggered_Music.AddListener(HandleUpdateOfMusic);




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
    

    void HandleUpdateOfVibrations(ButtonStatus newStatus)
    {
        Debug.Log("NOW VIBRATIONS: " + newStatus);
    }

    void HandleUpdateOfSounds(ButtonStatus newStatus)
    {
        bool isActive = false;
        if (newStatus == ButtonStatus.Passive)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }


        AudioManager.Instance.UpdateStatus_Sounds(isActive);
    }

    void HandleUpdateOfMusic(ButtonStatus newStatus)
    {
        bool isActive = false;
        if (newStatus == ButtonStatus.Passive)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }


        AudioManager.Instance.UpdateStatus_Music(isActive);
    }
}