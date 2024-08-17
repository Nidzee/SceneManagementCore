using System.Collections.Generic;
using System.Collections;
using UnityEngine;




public class GameUIController : MonoBehaviour
{
    [SerializeField] Transform _manaWidget;
    [SerializeField] Transform _towersUI;



    public void Initalize()
    {
        _manaWidget.gameObject.SetActive(false);
        _towersUI.gameObject.SetActive(false);


        ActivateStartUI();
    }

    void ActivateStartUI()
    {
        _manaWidget.gameObject.SetActive(true);
    }




    public void ActivateTowersWindow()
    {
        _manaWidget.gameObject.SetActive(false);
        _towersUI.gameObject.SetActive(true);
    }

    public void HideTowersWindow()
    {
        _manaWidget.gameObject.SetActive(true);
        _towersUI.gameObject.SetActive(false);
    }
}