using System.Collections.Generic;
using System.Collections;
using UnityEngine;




public class GameUIController : MonoBehaviour
{
    [SerializeField] Transform _manaWidget;
    [SerializeField] Transform _towersUI;
    [SerializeField] Transform _towerInfoUI;
    [SerializeField] Transform _castleInfoUI;



    public void Initalize()
    {
        ActivateManaScreen();
    }




    public void ActivateManaScreen()
    {
        _manaWidget.gameObject.SetActive(false);
        _towersUI.gameObject.SetActive(false);
        _towerInfoUI.gameObject.SetActive(false);
        _castleInfoUI.gameObject.SetActive(false);
        
        _manaWidget.gameObject.SetActive(true);
    }

    public void ActivateTowersWindow()
    {
        _manaWidget.gameObject.SetActive(false);
        _towersUI.gameObject.SetActive(false);
        _towerInfoUI.gameObject.SetActive(false);
        _castleInfoUI.gameObject.SetActive(false);

        _towersUI.gameObject.SetActive(true);
    }

    public void ActivateTowerInfoUI()
    {
        _manaWidget.gameObject.SetActive(false);
        _towersUI.gameObject.SetActive(false);
        _towerInfoUI.gameObject.SetActive(false);
        _castleInfoUI.gameObject.SetActive(false);

        _towerInfoUI.gameObject.SetActive(true);
    }
    
    public void ActivateCastleInfoUI()
    {
        _manaWidget.gameObject.SetActive(false);
        _towersUI.gameObject.SetActive(false);
        _towerInfoUI.gameObject.SetActive(false);
        _castleInfoUI.gameObject.SetActive(false);

        _castleInfoUI.gameObject.SetActive(true);
    }
}