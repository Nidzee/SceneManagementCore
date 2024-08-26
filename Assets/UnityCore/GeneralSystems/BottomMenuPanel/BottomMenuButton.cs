using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;







public class BottomMenuButton : MonoBehaviour
{
    
    [Header("VisualsBasic components")]
    [SerializeField] UniversalButton _button;
    [SerializeField] Image _buttonIcon;
    [SerializeField] Image _buttonImage;



    [Header("Visuals")]
    [SerializeField] Color _activatedButtonSprite;
    [SerializeField] Color _passiveButtonSprite;
    [SerializeField] Sprite _activatedIconSprite;
    [SerializeField] Sprite _passiveIconSprite;




    [HideInInspector] public UnityEvent OnButtonClick = new UnityEvent();










    public void Initialize(bool isActivated)
    {
        if (isActivated)
        {
            _buttonImage.color = _activatedButtonSprite;
            _buttonIcon.sprite = _activatedIconSprite;
        }
        else
        {
            _button.RemoveAllListeners();
            _button.AddListener(OnButtonClick.Invoke);
            _buttonImage.color = _passiveButtonSprite;
            _buttonIcon.sprite = _passiveIconSprite;
        }
    }
}