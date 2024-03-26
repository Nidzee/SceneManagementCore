using UnityEngine.Events;
using UnityEngine;

public class GameSceneHandler : MonoBehaviour
{
    public string THIS_SCENE_NAME;
    public UnityEvent OnSceneHandlerWokeUp = new UnityEvent();
    public UnityEvent OnSceneHandlerStart = new UnityEvent();




    public void Awake()
    {
        OnSceneHandlerWokeUp.Invoke();
    }

    public void Start()
    {
        OnSceneHandlerStart.Invoke();
    }



    public string GetSceneHandlerName()
    {
        return THIS_SCENE_NAME;
    }
}