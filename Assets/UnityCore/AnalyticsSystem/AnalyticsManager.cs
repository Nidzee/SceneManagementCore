using Cysharp.Threading.Tasks;
using UnityEngine.Analytics;
using UnityEngine;
using Unity.Services.Analytics;

public class AnalyticsManager : GeneralManager<AnalyticsManager>
{
    const string LOGGER_KEY = "[Analytics-Manager]";


    public override async UniTask Initialize()
    {
        Debug.Log(LOGGER_KEY + " Initialization-started");
        await UniTask.Yield();
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log(LOGGER_KEY + " Initialization-finished");
    }





    public void LogSceneOpened(string sceneName)
    {
        CustomEvent myEvent = new CustomEvent("SceneOpened")
        {
            { "sceneName", sceneName },
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        Debug.Log(LOGGER_KEY + " launch event for scene: " + sceneName);
    }
}