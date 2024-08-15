using UnityEngine.Events;
using UnityEngine;

public class GeneralSceneHandler : MonoBehaviour
{
    public string THIS_SCENE_NAME;
    [HideInInspector] public UnityEvent OnSceneHandlerWokeUp = new UnityEvent();
    [HideInInspector] public UnityEvent OnSceneHandlerStart = new UnityEvent();




    public virtual void Awake()
    {
        OnSceneHandlerWokeUp.Invoke();
    }

    public virtual void Start()
    {
        OnSceneHandlerStart.Invoke();
    }



    public string GetSceneHandlerName()
    {
        return THIS_SCENE_NAME;
    }
}