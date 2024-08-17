using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;





public class UniversalTowerUI : MonoBehaviour
{
    [SerializeField] Button _imitateClickButton;


    TowerPlace _myTower;



    [HideInInspector] public UnityEvent OnTowerPlaceClick = new UnityEvent();







    public void Initialize(TowerPlace thisTower)
    {
        
        _myTower = thisTower;


        // Connect buttons
        _imitateClickButton.onClick.RemoveAllListeners();
        _imitateClickButton.onClick.AddListener(HandleClickOnTower);
    }





    void HandleClickOnTower()
    {
        OnTowerPlaceClick.Invoke();
    }
}