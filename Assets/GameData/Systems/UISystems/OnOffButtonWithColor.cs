using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;




[System.Serializable]
public class OnOffButtonWithColorVisualsConfig
{
    public Transform ActiveContainer;
    public Color ActiveColor;
    public Transform PassiveContainer;
    public Color PassiveColor;
}



public class OnOffButtonWithColor : MonoBehaviour
{

    [SerializeField] OnOffButtonWithColorVisualsConfig _visualsConfig;
    [SerializeField] Button _widgetButton;
    [SerializeField] Image _buttonBackground;
    [HideInInspector] public UnityEvent<ButtonStatus> OnStatusUpdated = new UnityEvent<ButtonStatus>();

    ButtonStatus _currentStatus = ButtonStatus.None;







    public void Initialize()
    {
        OnStatusUpdated.RemoveAllListeners();


        _widgetButton.onClick.RemoveAllListeners();
        _widgetButton.onClick.AddListener(HandleClickOnButton);
        


        // Get active status of the button
        _currentStatus = ButtonStatus.Active;
        UpdateButtonVisuals();
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
            return;
        }


        if (_currentStatus == ButtonStatus.Passive)
        {
            _currentStatus = ButtonStatus.Active;
            UpdateButtonVisuals();
            OnStatusUpdated.Invoke(_currentStatus);
            return;
        }
    }





    void UpdateButtonVisuals()
    {
        _visualsConfig.ActiveContainer.gameObject.SetActive(false);
        _visualsConfig.PassiveContainer.gameObject.SetActive(false);


        if (_currentStatus == ButtonStatus.Passive)
        {
            _buttonBackground.color = _visualsConfig.PassiveColor;
            _visualsConfig.PassiveContainer.gameObject.SetActive(true);
            return;
        }


        if (_currentStatus == ButtonStatus.Active)
        {
            _buttonBackground.color = _visualsConfig.ActiveColor;
            _visualsConfig.ActiveContainer.gameObject.SetActive(true);
            return;
        }
    }
}



public enum ButtonStatus
{
    None = 0,
    Active = 1,
    Passive = 2,
}
