using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;

// --------------------------------
// This is entry point for the game
// --------------------------------
public class Bootstrap : MonoBehaviour
{
    public delegate UniTask ExecuteTask();

    public void Awake() 
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