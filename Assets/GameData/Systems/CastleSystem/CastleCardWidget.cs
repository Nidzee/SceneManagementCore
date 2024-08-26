using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;






public class CastleCardWidget : MonoBehaviour
{
    [SerializeField] Transform _contentHolder;
    [SerializeField] Image _cardIcon;
    [SerializeField] TMP_Text _amountNeedLabel;
    [SerializeField] Button _widgetButton;

    CastleConfig _data;
    public CastleConfig Data => _data;

    [HideInInspector] public UnityEvent<CastleCardWidget> OnTowerCardWidgetClicked = new UnityEvent<CastleCardWidget>();








    public void Initialize(CastleConfig castleConfig)
    {

        _data = castleConfig;



        _contentHolder.gameObject.SetActive(true);
        _cardIcon.sprite = _data.Icon;

        _widgetButton.onClick.RemoveAllListeners();
        _widgetButton.onClick.AddListener(TriggerCardClick);
    }

    void TriggerCardClick()
    {
        OnTowerCardWidgetClicked.Invoke(this);
    }
}