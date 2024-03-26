using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class InstanceManager<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null) 
        {
			Instance = this as T;
			DontDestroyOnLoad (this);
            return;
		} 

        Destroy (gameObject);
    }

    public abstract UniTask Initialize();
}