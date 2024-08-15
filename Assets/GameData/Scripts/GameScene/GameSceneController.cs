using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;






public class GameSceneController : IInitializable
{
    ManaResourceHandler _manaHandler;





    public GameSceneController(
        ManaResourceHandler manaHandler
    )
    {
        _manaHandler = manaHandler;
    }





    public void Initialize()
    {
        _manaHandler.Initialize();



        LaunchGameLogic();
    }

    void LaunchGameLogic()
    {
        _manaHandler.StartResourceGathering();
    }
}