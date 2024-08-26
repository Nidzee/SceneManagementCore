using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;




public class PauseController
{

    public static PauseController PauseControllerRef;
    
    
    public UnityEvent OnPauseEmited = new UnityEvent();
    public UnityEvent OnResumeEmited = new UnityEvent();


    public PauseController()
    {
        PauseControllerRef = this;

        OnPauseEmited.RemoveAllListeners();
        OnResumeEmited.RemoveAllListeners();
    }




    public void PauseTheGame()
    {
        AudioManager.Instance.TryToHideMusic();
        CustomLogger.LogPauseController("PAUSE THE GAME");
        OnPauseEmited.Invoke();
    }

    public void ResumeTheGame()
    {
        AudioManager.Instance.TryToReturnMusic();
        CustomLogger.LogPauseController("RESUME THE GAME");
        OnResumeEmited.Invoke();
    }
}