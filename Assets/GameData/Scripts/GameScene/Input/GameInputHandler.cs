using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;






public class GameInputHandler : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] LayerMask _detectLayers;

    bool _isPaused;





    public void Initialize()
    {
        _isPaused = false;


        PauseController.PauseControllerRef.OnPauseEmited.AddListener(Pause);
        PauseController.PauseControllerRef.OnResumeEmited.AddListener(Resume);
    }

    void Pause() => _isPaused = true;
    void Resume() => _isPaused = false;







    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (_isPaused)
            return;
        

        CustomLogger.LogInputHandler("CLICK");


        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;
        if (!Physics.Raycast(ray, out raycastHit, float.MaxValue, _detectLayers))
        {
            CustomLogger.LogInputHandler("Click fail because of layers");
            return;
        }




        var interractive = raycastHit.collider.gameObject.GetComponent<IInteractiveItem>();
        if (interractive != null)
        {
            CustomLogger.LogInputHandler("Detected interractive");
            interractive.Interract();
            return;
        }


        


        if (raycastHit.collider.transform.parent != null)
        {
            var castle = raycastHit.collider.transform.parent.GetComponent<Castle>();
            if (castle != null)
            {
                CustomLogger.LogInputHandler("Detected castle");
                castle.HandleClickOnCastle();
                return;
            }


            var tower = raycastHit.collider.transform.parent.GetComponent<TowerPlace>();
            if (tower != null)
            {
                CustomLogger.LogInputHandler("Detected tower");
                tower.HandleClickOnTower();
            }
        }
    }
}