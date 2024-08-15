using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;




public class GameCoinsWidget : MonoBehaviour
{
    [SerializeField] TMP_Text _gameCoinsLabel;



    public void Initialize()
    {

    }    

    public void NotifyCoinsUpdated(int coinsAmount)
    {
        _gameCoinsLabel.text = coinsAmount.ToString();
    }
}