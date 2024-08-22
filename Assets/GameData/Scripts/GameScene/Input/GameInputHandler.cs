using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;






public class GameInputHandler : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] LayerMask _detectLayers;




    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        

        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;
        if (!Physics.Raycast(ray, out raycastHit, float.MaxValue, _detectLayers))
        {
            Debug.Log("Ignore : ");
            return;
        }


        Debug.Log("CLICK ON: " + raycastHit.collider.gameObject.name);


        var interractive = raycastHit.collider.gameObject.GetComponent<IInteractiveItem>();
        if (interractive != null)
        {
            interractive.Interract();
        }


        if (raycastHit.collider.transform.parent != null)
        {
            var tower = raycastHit.collider.transform.parent.GetComponent<TowerPlace>();
            if (tower != null)
            {
                tower.HandleClickOnTower();
            }
        }
    }
}