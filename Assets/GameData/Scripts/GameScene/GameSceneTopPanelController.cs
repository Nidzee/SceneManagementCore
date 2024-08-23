using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;




public class GameSceneTopPanelController : MonoBehaviour
{
    [SerializeField] BasicButton _pauseButton;

    [HideInInspector] public UnityEvent OnPauseButtonClicked = new UnityEvent();



    public void Initialize()
    {
        _pauseButton.RemoveAllListeners();
        _pauseButton.AddListener(DetectClickOnPauseButto);
    }

    void DetectClickOnPauseButto()
    {
        OnPauseButtonClicked.Invoke();
    }
}
