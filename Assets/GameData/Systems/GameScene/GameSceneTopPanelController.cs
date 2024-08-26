using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;




public class GameSceneTopPanelController : MonoBehaviour
{
    [SerializeField] UniversalButton _pauseButton;

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
