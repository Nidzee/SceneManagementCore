using Cysharp.Threading.Tasks;
using System;

public abstract class GeneralManager<T> : IManager  where T : class, IManager, new() {
    
    protected static T instance;
    public Guid instanceId { get; } = Guid.NewGuid();
    public abstract UniTask Initialize();


    
    public static T Instance
    {
        get 
        {
            if (instance == null) 
            {
                instance = new T();
            }

            return instance;
        }
    }
}



public interface IManager {
    Guid instanceId { get; }
    
    UniTask Initialize();
}