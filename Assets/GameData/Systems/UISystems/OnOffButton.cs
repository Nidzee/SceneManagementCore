using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;



[System.Serializable]
public class OnOffButtonVisualsConfig
{
    public Transform ActiveContainer;
    public Transform PassiveContainer;
}


public class OnOffButton : MonoBehaviour
{
    [SerializeField] OnOffButtonVisualsConfig _visualsConfig;
    [SerializeField] Button _widgetButton;
    [HideInInspector] public UnityEvent<ButtonStatus> OnStatusUpdated = new UnityEvent<ButtonStatus>();

    [SerializeField] AudioClip _clickSound_Active;
    [SerializeField] AudioClip _clickSound_Passive;


    ButtonStatus _currentStatus = ButtonStatus.None;







    public void Initialize(bool status = true)
    {
        if (status)
        {
            _currentStatus = ButtonStatus.Active;
        }
        else
        {
            _currentStatus = ButtonStatus.Passive;
        }


        // Get active status of the button
        UpdateButtonVisuals();


        OnStatusUpdated.RemoveAllListeners();

        _widgetButton.onClick.RemoveAllListeners();
        _widgetButton.onClick.AddListener(HandleClickOnButton);
    }

    void HandleClickOnButton()
    {
        // Swtict current status
        if (_currentStatus == ButtonStatus.None)
        {
            CustomLogger.LogError("ERROR! Button status was not initialized");
            return;
        }



        if (_currentStatus == ButtonStatus.Active)
        { 
            _currentStatus = ButtonStatus.Passive;
            UpdateButtonVisuals();
            OnStatusUpdated.Invoke(_currentStatus);

            AudioManager.Instance.PlaySound(_clickSound_Active);

            return;
        }


        if (_currentStatus == ButtonStatus.Passive)
        {
            _currentStatus = ButtonStatus.Active;
            UpdateButtonVisuals();
            OnStatusUpdated.Invoke(_currentStatus);

            AudioManager.Instance.PlaySound(_clickSound_Passive);

            return;
        }
    }





    void UpdateButtonVisuals()
    {
        _visualsConfig.ActiveContainer.gameObject.SetActive(false);
        _visualsConfig.PassiveContainer.gameObject.SetActive(false);


        if (_currentStatus == ButtonStatus.Passive)
        {
            _visualsConfig.PassiveContainer.gameObject.SetActive(true);
            return;
        }


        if (_currentStatus == ButtonStatus.Active)
        {
            _visualsConfig.ActiveContainer.gameObject.SetActive(true);
            return;
        }
    }
}