using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;







public class UniversalButton  : MonoBehaviour
{
    [SerializeField] Button _thisButton;

    [Header("Additional settings")]
    [SerializeField] AudioClip _buttonPressSound;
    [SerializeField] bool _animateButtonPump;


    bool _isSoundAttached = false;
    Sequence _pumpSequence;

    const float PUMP_UP_DURATION = 0.1f;
    const float PUMP_BACK_DURATION = 0.1f;
    const float PUMP_VALUE = 1.1f;



    // Public references
    public Button ThisButton => _thisButton;







    public void RemoveAllListeners()
    {
        _thisButton.onClick.RemoveAllListeners();
        _isSoundAttached = false;
    }

    public void AddListener(UnityAction action)
    {
        // [0] Add logic signal
        _thisButton.onClick.AddListener(action);

        // [1] Add pump if required
        if (_animateButtonPump)
            _thisButton.onClick.AddListener(AnimateButtonPump);

        // [2] Add sound if required
        if (!_isSoundAttached)
        {
            _isSoundAttached = true;
            _thisButton.onClick.AddListener(LaunchButtonSoundClick);
        }
    }







    void LaunchButtonSoundClick()
    {
        AudioManager.Instance.PlaySound(_buttonPressSound);
    }

    void AnimateButtonPump()
    {
        // Kill previous animation if provided
        _pumpSequence?.Kill();
        _thisButton.transform.DOKill();

        // Reset label object
        _thisButton.transform.localScale = Vector3.one;

        // Launch new animation
        _pumpSequence = DOTween.Sequence();
        _pumpSequence.Append(_thisButton.transform.DOScale(PUMP_VALUE, PUMP_UP_DURATION));
        _pumpSequence.Append(_thisButton.transform.DOScale(1, PUMP_BACK_DURATION));
    }
}