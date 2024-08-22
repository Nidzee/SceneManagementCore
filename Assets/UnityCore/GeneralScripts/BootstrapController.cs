using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

// --------------------------------
// This is entry point for the game
// --------------------------------
public class BootstrapController : IInitializable
{
    public delegate UniTask ExecuteTask();




    public void Initialize() 
    {
        List<ExecuteTask> initializationTasks = new List<ExecuteTask>()
        {
            // Initialize alll unity services
            () => UnityServices.InitializeAsync().AsUniTask(),
            () => AdsInitializer.Instance.Initialize(),
            () => InterstitialsManager.Instance.Initialize(),
            () => RewardedAdsManager.Instance.Initialize(),


            // Initialize custom game services
            () => AnalyticsManager.Instance.Initialize(),
            () => AuthenticationManager.Instance.Initialize(),
            () => PlayerDataManager.Instance.Initialize(),
        };
        
        
        SceneLoader.AddAdditionalTasks(initializationTasks); 
        SceneLoader.LoadScene<MainMenuScene>("MainMenuScene", SceneLoader.LoadingAnimationType.NoAnimation, null).Forget(); 
    }   
}