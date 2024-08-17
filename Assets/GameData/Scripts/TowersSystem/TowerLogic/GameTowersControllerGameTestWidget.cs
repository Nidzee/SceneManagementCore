using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;




public class GameTowersControllerGameTestWidget : MonoBehaviour
{
    [SerializeField] List<TowerPlace> _availableTowers;



    [HideInInspector] public UnityEvent<TowerPlace> OnTowerPlaceClick = new UnityEvent<TowerPlace>();



    public void Initialize()
    {
        foreach (var towerPlace in _availableTowers)
        {
            towerPlace.Initialize();
            towerPlace.OnTowerPlaceClick.RemoveAllListeners();
            towerPlace.OnTowerPlaceClick.AddListener(DetectClickOnTowerPlace);
        }
    }





    void DetectClickOnTowerPlace(TowerPlace towerPlace)
    {
        OnTowerPlaceClick.Invoke(towerPlace);
    }
}