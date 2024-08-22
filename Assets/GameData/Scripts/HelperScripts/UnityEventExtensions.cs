using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.Events;




public static class UnityEventExtensions
{
    // Extension method to create a Task that awaits the firing of a UnityEvent
    public static UniTask WaitForEventAsync(this UnityEvent unityEvent)
    {
        // Create a new TaskCompletionSource
        var tcs = new TaskCompletionSource<bool>();


        // Define a handler to complete the task when the event fires
        UnityAction handler = null;
        handler = () =>
        {
            // Remove the handler from the event
            unityEvent.RemoveListener(handler);

            // Set the TaskCompletionSource result to true
            tcs.SetResult(true);
        };


        // Add the handler to the event
        unityEvent.AddListener(handler);


        // Return the Task associated with the TaskCompletionSource
        return tcs.Task.AsUniTask();
    }


    // Extension method to create a Task that awaits the firing of a UnityEvent
    public static UniTask<bool> WaitForEventAsync(this UnityEvent<bool> unityEvent)
    {
        // Create a new TaskCompletionSource
        var tcs = new TaskCompletionSource<bool>();

        // Define a handler to complete the task when the event fires
        UnityAction<bool> handler = null;
        handler = (value) =>
        {
            // Remove the handler from the event
            unityEvent.RemoveListener(handler);

            // Set the TaskCompletionSource result to the received value
            tcs.SetResult(value);
        };

        // Add the handler to the event
        unityEvent.AddListener(handler);

        // Return the Task associated with the TaskCompletionSource
        return tcs.Task.AsUniTask();
    }



    // Extension method to create a Task that awaits the firing of a UnityEvent
    public static UniTask<T> WaitForEventAsync<T>(this UnityEvent<T> unityEvent)
    {
        // Create a new TaskCompletionSource
        var tcs = new TaskCompletionSource<T>();

        // Define a handler to complete the task when the event fires
        UnityAction<T> handler = null;
        handler = (value) =>
        {
            // Remove the handler from the event
            unityEvent.RemoveListener(handler);

            // Set the TaskCompletionSource result to the received value
            tcs.SetResult(value);
        };

        // Add the handler to the event
        unityEvent.AddListener(handler);

        // Return the Task associated with the TaskCompletionSource
        return tcs.Task.AsUniTask();
    }
}