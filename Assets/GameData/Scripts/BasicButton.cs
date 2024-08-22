using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;




public class BasicButton : MonoBehaviour
{
    [SerializeField] Button _thisButton;
    [SerializeField] AudioClip _buttonPressSound;
    [SerializeField] bool _animateButtonPump;
    [SerializeField] Image _buttonBackgroundImage;


    bool _isSoundAttached = false;
    public Button ThisButton => _thisButton;
    Sequence _pumpSequence;







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
        _pumpSequence.Append(_thisButton.transform.DOScale(1.1f, 0.1f));
        _pumpSequence.Append(_thisButton.transform.DOScale(1, 0.1f));
    }



    public void TriggerExternalPump()
    {
        AnimateButtonPump();
    }


    public void SetExternalButtonBackground(Sprite targetBackgroundSprite)
    {
        if (_buttonBackgroundImage == null)
        {
            CustomLogger.LogError("Try to change button background. No image provided");
            return;
        }


        _buttonBackgroundImage.sprite = targetBackgroundSprite;
    }
}