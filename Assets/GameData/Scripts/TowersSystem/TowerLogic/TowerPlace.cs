using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;



public enum TowerPlaceSelectionType
{
    TryToBuild = 0,
    Inspect = 1,
}


public class TowerPlace : MonoBehaviour
{
    [SerializeField] Transform _selectionOverlay;

    [HideInInspector] public UnityEvent<TowerPlace> OnTowerPlaceClick = new UnityEvent<TowerPlace>();


    UniversalTowerConfig _thisTowerData;
    public UniversalTowerConfig ThisTowerData => _thisTowerData;

    Transform _createdTowerContainer;





    public void Initialize()
    {
        _thisTowerData = null;
        _createdTowerContainer = null;
    }

    public void UpdateSelectionStatus(bool status)
    {
        if (status)
        {
            _selectionOverlay.gameObject.SetActive(true);
        }
        else
        {
            _selectionOverlay.gameObject.SetActive(false);
        }
    }

    public void HandleClickOnTower()
    {
        OnTowerPlaceClick.Invoke(this);
    }
















    // Creation and destroy logic
    public void BuildTower(UniversalTowerConfig towerConfig)
    {
        _thisTowerData = towerConfig;
        var towerPrefab = towerConfig.TowerPrefab;
        var createdTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        _createdTowerContainer = createdTower.transform;


        if (towerConfig.TowerType == TowerType.WarTower)
        {
            var warTower = createdTower.GetComponent<WarTower>();
            warTower.Initialize(towerConfig);
            return;
        }



        Debug.Log("Another tower types does not supported yet");
    }

    public void DestroyTower()
    {
        Destroy(_createdTowerContainer.gameObject);
        _thisTowerData = null;
    }





























    // Calculations logic
    public TowerPlaceSelectionType GetThisTowerSelectionType()
    {
        if (_thisTowerData == null)
        {
            return TowerPlaceSelectionType.TryToBuild;
        }

        return TowerPlaceSelectionType.Inspect;
    }

    public int CalculateCoinsIncomeForTowerDestroy()
    {
        return 100;
    }
}