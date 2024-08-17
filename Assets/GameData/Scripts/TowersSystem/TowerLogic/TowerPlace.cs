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
    

    [SerializeField] UniversalTowerUI _towerUI;
    [SerializeField] Transform _selectionOverlay;

    [HideInInspector] public UnityEvent<TowerPlace> OnTowerPlaceClick = new UnityEvent<TowerPlace>();


    bool _isSelected;
    UniversalTowerConfig _thisTowerData;







    public void Initialize()
    {
        _isSelected = false;
        _thisTowerData = null;




        _towerUI.Initialize(this);
        _towerUI.OnTowerPlaceClick.RemoveAllListeners();
        _towerUI.OnTowerPlaceClick.AddListener(HandleClickOnTower);
    }

    public void UpdateSelectionStatus(bool status)
    {
        if (status)
        {
            _isSelected = true;
            _selectionOverlay.gameObject.SetActive(true);
        }
        else
        {
            _isSelected = false;
            _selectionOverlay.gameObject.SetActive(false);
        }
    }

    public void BuildTower(UniversalTowerConfig towerConfig)
    {
        Debug.Log("BUILD: " + towerConfig.Name);

        _thisTowerData = towerConfig;
        var towerPrefab = towerConfig.TowerPrefab;
        var createdTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);


        if (towerConfig.TowerType == TowerType.WarTower)
        {
            var warTower = createdTower.GetComponent<WarTower>();
            warTower.Initialize(towerConfig);
            return;
        }

        Debug.Log("Another tower types does not supported yet");
    }

    public TowerPlaceSelectionType GetThisTowerSelectionType()
    {
        if (_thisTowerData == null)
        {
            return TowerPlaceSelectionType.TryToBuild;
        }

        return TowerPlaceSelectionType.Inspect;
    }



















    void HandleClickOnTower()
    {
        OnTowerPlaceClick.Invoke(this);
    }
}
