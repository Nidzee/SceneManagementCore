using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;





public class GameTowersController
{

    GameTowersControllerGameTestWidget _gameTowersControllerGameTestWidget;
    TowerPlace _selectedTowerPlace;







    public UnityEvent<TowerPlace> OnTowerPlaceClicked = new UnityEvent<TowerPlace>();







    public GameTowersController(GameTowersControllerGameTestWidget gameTowersControllerGameTestWidget)
    {
        _gameTowersControllerGameTestWidget = gameTowersControllerGameTestWidget;
    }

    public void Initialize()
    {

        _selectedTowerPlace = null;




        _gameTowersControllerGameTestWidget.Initialize();
        _gameTowersControllerGameTestWidget.OnTowerPlaceClick.RemoveAllListeners();
        _gameTowersControllerGameTestWidget.OnTowerPlaceClick.AddListener(DetectClickOnTowerPlace);
    }

    void DetectClickOnTowerPlace(TowerPlace towerPlace)
    {
        if (_selectedTowerPlace == towerPlace)
        {
            Debug.Log("Skip same tower selection");
            return;
        }


        _selectedTowerPlace?.UpdateSelectionStatus(false);
        _selectedTowerPlace = towerPlace;
        OnTowerPlaceClicked.Invoke(_selectedTowerPlace);
    }

    public void InspectionOverlayTESTER()
    {
        _selectedTowerPlace = null;
    }

    public void ActivateSelectionOverlay()
    {
        _selectedTowerPlace?.UpdateSelectionStatus(true);
    }

    public void StopSelection()
    {
        _selectedTowerPlace?.UpdateSelectionStatus(false);
        _selectedTowerPlace = null;
    }









    public void ApplyTowerCard(UniversalTowerConfig towerConfig)
    {
        _selectedTowerPlace.BuildTower(towerConfig);
    }
}