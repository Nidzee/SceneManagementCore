using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuScene : GameSceneHandler
{
    [SerializeField] TMP_Text _infoLabel;



    public void SetPassingData(string passingData)
    {
        _infoLabel.text = passingData;
    }
}