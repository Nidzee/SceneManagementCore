using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using System;
using static Bootstrap;


// ---------------------------------------------
// This class is responsible for scene management
// ---------------------------------------------
public static class SceneLoader
{
    const string LOGGER_KEY = "[Scene-Loader]";
    static string LastOpenedScene = null;

    static void LogMessage(string message)
    {
        Debug.LogError("[###] " + LOGGER_KEY + " " + message);
    }

    public enum LoadingAnimationType
    {
        WithAnimation = 0,
        NoAnimation = 1,
    }






    public static async UniTask LoadScene<T>(string sceneName, LoadingAnimationType animationType = LoadingAnimationType.WithAnimation, Action<T> callback = null) where T : GameSceneHandler
    {

        // Skip if data is corrupted
        if (string.IsNullOrEmpty(sceneName))
        {
            LogMessage("Target scene name is invalid. Scene loading aborted");
            return;
        }

        if (!string.IsNullOrEmpty(LastOpenedScene) && LastOpenedScene.Equals(sceneName))
        {
            LogMessage("Target scene is already opened");
            return;
        }

        // Launch loading of scene
        await LoadSceneAsync<T>(sceneName, animationType, callback);
        LastOpenedScene = sceneName;
        AnalyticsManager.Instance.LogSceneOpened(sceneName);
    }



    private static async UniTask LoadSceneAsync<T>(string sceneName, LoadingAnimationType animationType = LoadingAnimationType.WithAnimation, Action<T> callback = null) where T : GameSceneHandler
    {

        // [0] Wait for loading screen loading
        var loadingScreen = await OpenLoadingScene(animationType);
        if (loadingScreen == null)
        {
            return;
        }


        // [1] Get reference to be able to show loading progress
        var progress = Progress.Create<float>(loadingScreen.SetProgress);


        // [2] Wait for other unnecessary scenes to unload
        await UnloadAllScenesExcept(sceneName);


        // [3] Wait for scene to load
        await WaitForSceneToLoad<T>(sceneName, callback, progress);
        

        // [4] Hide loading scene
        await HideLoadingScreen();
    }



    static async UniTask WaitForSceneToLoad<T>(string sceneName, Action<T> callback, IProgress<float> progress)
    {
        
        // Create operation for scene loading
        var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);


        // Add callback for scene opening
        operation.completed += (s) => {
            var targetScene = SceneManager.GetSceneByName(sceneName);
            var targetSceneHandler = targetScene.GetRootObject<T>();
            if (targetSceneHandler != null)
            {
                callback?.Invoke(targetSceneHandler);
            }
        };




        // Handle single scene opening task loading
        bool hasAdditionalTasksToPerform = AdditionalTasks.Count > 0;
        if (!hasAdditionalTasksToPerform)
        {
            // Update progress during scene loading
            while (!operation.isDone)
            {
                await UniTask.Yield();
                progress.Report(operation.progress);
            }
            return;
        }



        // Handle additional tasks loading
        await operation;
        progress.Report(0.1f);


        float percentageLeft = 0.9f;
        float startPercentage = 0.1f;
        float step = percentageLeft / AdditionalTasks.Count;

        for (int i = 0; i < AdditionalTasks.Count; i++)
        {
            Debug.Log("[###] TASK START : " + i);
            await AdditionalTasks[i]();
            Debug.Log("[###] TASK FINISHED: " + i);
            progress.Report(startPercentage += step);
        }

        AdditionalTasks = new List<ExecuteTask>();
        progress.Report(1f);
    }

    static List<ExecuteTask> AdditionalTasks = new List<ExecuteTask>();
    public static void AddAdditionalTasks(List<ExecuteTask> uniTasks)
    {
        AdditionalTasks = uniTasks;
    }















    public static async UniTask UnloadAllScenesExcept(params string[] scenesToLeave)
    {
        List<UniTask> scenesToUnload = new List<UniTask>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var sceneToCheck = SceneManager.GetSceneAt(i);
            if (scenesToLeave.Any(sceneName => sceneName == sceneToCheck.name))
            {
                continue;
            }

            if (sceneToCheck.name == "LoadingScene")
            {
                continue;
            }


            scenesToUnload.Add(SceneManager.UnloadSceneAsync(sceneToCheck).ToUniTask());
        }

        await UniTask.WhenAll(scenesToUnload);
    }


    // Loading scene logic
    public static async UniTask<ILoadingScreen> OpenLoadingScene(LoadingAnimationType animationType)
    {
        // Wait for loading loading-scene to game
        await SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);


        // Try to get root object from scene        
        var targetScene = SceneManager.GetSceneByName("LoadingScene");
        var loadingScreen = targetScene.GetRootObject<ILoadingScreen>();
        if (loadingScreen == null)
        {
            LogMessage("Error! Loading scene missing handler.");
            return null;
        }


        // Wait for fade-in animation
        await loadingScreen.ShowLoadingScreen(animationType);

        return loadingScreen;
    }
    
    public static async UniTask HideLoadingScreen()
    {

        // Try to get root object from scene        
        var targetScene = SceneManager.GetSceneByName("LoadingScene");
        var loadingScreen = targetScene.GetRootObject<ILoadingScreen>();
        if (loadingScreen == null)
        {
            return;
        }


        // Wait for fade-out animation
        await loadingScreen.HideLoadingScreen();


        // Unload scene
        await SceneManager.UnloadSceneAsync("LoadingScene");
    }
}




public static class SceneAssetExtensions
{
    internal static T GetRootObject<T> (this Scene sceneReference)
    {
        var targetObject = sceneReference
            .GetRootGameObjects()
            .FirstOrDefault(go => go.TryGetComponent<T>(out _));
        
        return targetObject != null ? targetObject.GetComponent<T>() : default;
    }
    
    internal static T GetAnyObject<T> (this Scene sceneReference)
    {
        return sceneReference
            .GetRootGameObjects()
            .SelectMany(go => go.GetComponentsInChildren<T>())
            .FirstOrDefault();
    }
}