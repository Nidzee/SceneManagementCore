using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;





public class MainMenuSceneController : IInitializable
{
    TopMenuPanel _topMenuPanel;
    BottomMenuPanel _botomMenupanel;
    MainMenuSceneContent _content;
    GameSceneTransferingData _gameSceneTransferingData;




    public MainMenuSceneController(
        BottomMenuPanel botomMenupanel,
        GameSceneTransferingData gameSceneTransferingData,
        MainMenuSceneContent content,
        TopMenuPanel topMenuPanel
        )
    {
        _gameSceneTransferingData = gameSceneTransferingData;
        _botomMenupanel = botomMenupanel;
        _topMenuPanel = topMenuPanel;
        _content = content;
    }

    public void Initialize()
    {


        _gameSceneTransferingData.PutSomeData(100);


        _topMenuPanel.Initialize();
        _botomMenupanel.Initialize();
        _content.Initialize();
    }
}