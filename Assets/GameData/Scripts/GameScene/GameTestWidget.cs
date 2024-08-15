using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;





public class GameTestWidget : MonoBehaviour
{

    [Header("General config")]
    [SerializeField] Transform _panelContent;
    [SerializeField] Button _onOffButton;




    [Header("Resources config")]
    [SerializeField] int _coinsAmount;


    [Header("Add coins config")]
    [SerializeField] Button _addCoinsButton;
    [SerializeField] TMP_Text _addCoinsButtonLabel;


    [Header("Remove coins config")]
    [SerializeField] Button _removeCoinsButton;
    [SerializeField] TMP_Text _removeCoinsButtonLabel;



    bool _widgetStatus;



    [HideInInspector] public UnityEvent<int> OnCoinsTriggered_Add = new UnityEvent<int>();
    [HideInInspector] public UnityEvent<int> OnCoinsTriggered_Remove = new UnityEvent<int>();







    public void Initalize()
    {


        _onOffButton.onClick.RemoveAllListeners();
        _onOffButton.onClick.AddListener(TriggerOnOffButtonClick);





        // Initialize coins buttons
        _addCoinsButtonLabel.text = "+" + _coinsAmount + "$";
        _removeCoinsButtonLabel.text = "-" + _coinsAmount + "$";

        _addCoinsButton.onClick.RemoveAllListeners();
        _addCoinsButton.onClick.AddListener(TriggerCoinsAddButtonPress);
        
        _removeCoinsButton.onClick.RemoveAllListeners();
        _removeCoinsButton.onClick.AddListener(TriggerCoinsRemoveButtonPress);


        _widgetStatus = false;
        _panelContent.gameObject.SetActive(_widgetStatus);
    }






    void TriggerOnOffButtonClick()
    {
        _widgetStatus = !_widgetStatus;
        _panelContent.gameObject.SetActive(_widgetStatus);
    }

    void TriggerCoinsAddButtonPress() => OnCoinsTriggered_Add.Invoke(_coinsAmount);
    void TriggerCoinsRemoveButtonPress() => OnCoinsTriggered_Remove.Invoke(_coinsAmount);
}