using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;






public class CastleInfoUIHandler : MonoBehaviour
{
    [SerializeField] CastleHealthWidget _castleHealthWidget;

    [Header("Buttons config")]
    [SerializeField] Button _closeButton;


    [SerializeField] TMP_Text _castleName;
    [SerializeField] Image _castleIcon;


    Castle _thisCastleData;

    // Events config
    [HideInInspector] public UnityEvent OnCloseButtonClicked = new UnityEvent();




    public void Initialize()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(DetectClick_CloseButton);
    }

    public void InitializeInfoForCastle(Castle castle)
    {
        _thisCastleData = castle;
        
        _castleName.text = _thisCastleData.Config.Name;
        _castleIcon.sprite = _thisCastleData.Config.Icon;

        _thisCastleData.OnDamageTaken.RemoveAllListeners();
        _thisCastleData.OnDamageTaken.AddListener(UpdateCastleHealthSliderInUI);

        UpdateCastleHealthSliderInUI();
    }

    void UpdateCastleHealthSliderInUI()
    {
        int maxHealth = _thisCastleData.Config.HealthAmount;
        int currentHealth = _thisCastleData.CurrentHealth;
        _castleHealthWidget.Initialize(maxHealth, currentHealth);
    }






    void DetectClick_CloseButton()
    {
        OnCloseButtonClicked.Invoke();
        _thisCastleData.OnDamageTaken.RemoveAllListeners();
    }
}