using UnityEngine.Events;
using UnityEngine;
using TMPro;




public class GameSettingsPopUp : BasicPopUp
{

    [Header("Custom buttons config")]
    [SerializeField] OnOffButton _vibrationsButton;
    [SerializeField] OnOffButton _soundsButton;
    [SerializeField] OnOffButton _musicButton;




    [HideInInspector] public UnityEvent<ButtonStatus> OnButtonTriggered_Vibrations = new UnityEvent<ButtonStatus>();
    [HideInInspector] public UnityEvent<ButtonStatus> OnButtonTriggered_Sounds = new UnityEvent<ButtonStatus>();
    [HideInInspector] public UnityEvent<ButtonStatus> OnButtonTriggered_Music = new UnityEvent<ButtonStatus>();





    public void Initialize()
    {

        _closeButton?.RemoveAllListeners();
        _closeButton?.AddListener(OnClosePopUp_Clicked.Invoke);






        // Connect on-off-buttons
        if (_vibrationsButton != null)
        {
            _vibrationsButton.Initialize();
            _vibrationsButton.OnStatusUpdated.AddListener((status) => OnButtonTriggered_Vibrations.Invoke(status));
        }


        if (_soundsButton != null)
        {
            _soundsButton.Initialize(AudioManager.Instance.IsSoundsOn);
            _soundsButton.OnStatusUpdated.AddListener((status) => OnButtonTriggered_Sounds.Invoke(status));
        }


        if (_musicButton != null)
        {
            _musicButton.Initialize(AudioManager.Instance.IsMusicOn);
            _musicButton.OnStatusUpdated.AddListener((status) => OnButtonTriggered_Music.Invoke(status));
        }
    }
}