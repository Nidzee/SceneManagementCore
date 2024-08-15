using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;






public class GameSceneController : IInitializable
{
    ManaResourceHandler _manaHandler;
    GameSceneTopPanelHandler _topPanelHandler;




    public GameSceneController(
        ManaResourceHandler manaHandler,
        GameSceneTopPanelHandler topPanelHandler
    )
    {
        _manaHandler = manaHandler;
        _topPanelHandler = topPanelHandler;
    }





    public void Initialize()
    {
        _manaHandler.Initialize();
        _topPanelHandler.Initialize();



        LaunchGameLogic();
    }

    void LaunchGameLogic()
    {
        _manaHandler.StartResourceGathering();
    }
}