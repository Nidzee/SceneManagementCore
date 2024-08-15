using System.Collections.Generic;
using System.Collections;
using UnityEngine;




public class GameSceneTopPanelController : MonoBehaviour
{
    [SerializeField] BasicButton _pauseButton;



    public void Initialize()
    {
        _pauseButton.OnClick.RemoveAllListeners();
        _pauseButton.OnClick.AddListener(DetectClickOnPauseButto);
    }

    void DetectClickOnPauseButto()
    {
        PauseMenuPopIpRoute route = new PauseMenuPopIpRoute();

        route.StartRoute();
    }
}
