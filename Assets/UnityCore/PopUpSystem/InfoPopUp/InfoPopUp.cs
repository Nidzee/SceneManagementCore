using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;




public class InfoPopUp : BasicPopUp
{
    [SerializeField] TMP_Text _infoLabel;










    public void Initialize(string customInfo)
    {
        _closeButton.OnClick.RemoveAllListeners();
        _closeButton.OnClick.AddListener(() => OnClosePopUp_Clicked.Invoke());



        // Initialize custom popup content
        _infoLabel.text = customInfo;
    }
}