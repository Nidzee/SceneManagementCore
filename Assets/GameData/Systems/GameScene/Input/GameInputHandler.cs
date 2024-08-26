using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;






public class GameInputHandler : MonoBehaviour
{
    InputMap _inputMap;
    [SerializeField] Camera _mainCamera;
    [SerializeField] LayerMask _detectLayers;



    [Header("Click detection rect")]
    [SerializeField] RectTransform _clickDetectionRect;




    [Header("Click visualizer")]
    [SerializeField] RectTransform _clickVisualizerRect;
    [SerializeField] RectTransform _uiAreaRect;

    bool _isPaused;




    [HideInInspector] public UnityEvent<Vector2> OnCameraMoveRecieved = new UnityEvent<Vector2>();








    public void Initialize()
    {
        _isPaused = false;
        PauseController.PauseControllerRef.OnPauseEmited.AddListener(Pause);
        PauseController.PauseControllerRef.OnResumeEmited.AddListener(Resume);
        _clickVisualizerRect.gameObject.SetActive(false);



        _inputMap = new InputMap();
        _inputMap.Enable();
        _inputMap.Gameplay.TouchPosition.performed += OnTouchPosition_Performed;
        _inputMap.Gameplay.TouchPress.started += OnTouchPress_Started;
        _inputMap.Gameplay.TouchPress.canceled += OnTouchPress_Canceled;



        CustomLogger.LogInputHandler("INITIALIZED");
    }








    // Touch press logic
    void OnTouchPress_Started(InputAction.CallbackContext context)
    {
        // Get touch screen point
        var touchPos = _inputMap.Gameplay.TouchPosition.ReadValue<Vector2>();
        
        // Skip if click detected out of rect
        bool isTouchedInRect = RectTransformUtility.RectangleContainsScreenPoint(_clickDetectionRect, touchPos);
        if (!isTouchedInRect)
        {
            OnTouchPress_Canceled(context);
            return;
        }

        if (TryToDetectClick(touchPos))
        {
            OnTouchPress_Canceled(context);
            return;
        }


        _inputMap.Gameplay.TouchDelta.performed += OnTouchDelta_Performed;
    }

    void OnTouchPress_Canceled(InputAction.CallbackContext context)
    {
        _inputMap.Gameplay.TouchDelta.performed -= OnTouchDelta_Performed;

        _clickVisualizerRect.gameObject.SetActive(false);
    }







    void OnTouchPosition_Performed(InputAction.CallbackContext context)
    {
        _clickVisualizerRect.gameObject.SetActive(true);
        var screenPos = context.ReadValue<Vector2>();
        PlaceUIVisualizer(screenPos);
    }

    void OnTouchDelta_Performed(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<Vector2>();
        OnCameraMoveRecieved.Invoke(delta);
    }














    bool TryToDetectClick(Vector2 touchStartPos)
    {
        if (_isPaused)
            return false;
        


        var ray = _mainCamera.ScreenPointToRay(touchStartPos);
        RaycastHit raycastHit;
        if (!Physics.Raycast(ray, out raycastHit, float.MaxValue, _detectLayers))
        {
            CustomLogger.LogInputHandler("Click fail because of layers");
            return false;
        }




        var interractive = raycastHit.collider.gameObject.GetComponent<IInteractiveItem>();
        if (interractive != null)
        {
            CustomLogger.LogInputHandler("Detected interractive");
            interractive.Interract();
            return true;
        }


        


        if (raycastHit.collider.transform.parent != null)
        {
            var castle = raycastHit.collider.transform.parent.GetComponent<Castle>();
            if (castle != null)
            {
                CustomLogger.LogInputHandler("Detected castle");
                castle.HandleClickOnCastle();
                return true;
            }


            var tower = raycastHit.collider.transform.parent.GetComponent<TowerPlace>();
            if (tower != null)
            {
                CustomLogger.LogInputHandler("Detected tower");
                tower.HandleClickOnTower();
                return true;
            }
        }



        return false;
    }













    // Helper method
    void PlaceUIVisualizer(Vector2 screenCoords)
    {
        Vector3 screenPos = screenCoords;
        Vector2 screenPos2D = new Vector2(screenPos.x, screenPos.y);
        Vector2 anchoredPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_uiAreaRect, screenPos2D, null, out anchoredPos);
        _clickVisualizerRect.anchoredPosition = anchoredPos;
    }









    // Controlling methods
    void Pause() => _isPaused = true;
    void Resume() => _isPaused = false;
}