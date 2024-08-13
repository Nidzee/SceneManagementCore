using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TutorialsManager : GeneralManager<TutorialsManager>
{
    const string LOGGER_KEY = "[Tutorials-Manager]";





    public override async UniTask Initialize()
    {
        Debug.Log(LOGGER_KEY + " initialization-started");
        
        Debug.Log(LOGGER_KEY + " initialization-finished");
    }











}