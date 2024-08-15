using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;




public class AudioManager : MonoSingleton<AudioManager>
{

    [SerializeField] AudioSource _musicSource;


    bool _isSoundsOn = true;
    bool _isMusicOn = true;

    public bool IsMusicOn => _isMusicOn;
    public bool IsSoundsOn => _isSoundsOn;






    public int poolSize = 10; // Initial pool size
    public GameObject audioSourcePrefab; // Prefab for the AudioSource object

    private List<AudioSource> audioSources; // List to hold pooled AudioSources

    private void Start()
    {
        // Initialize the list of AudioSources
        audioSources = new List<AudioSource>();

        _musicSource.volume = 0f;


        // Create the initial pool of AudioSources
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            audioSources.Add(audioSource);
        }



        gameObject.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }







    // Sounds logic
    public void PlaySound(AudioClip clip)
    {
        // Skip if sounds are off
        if (!_isSoundsOn)
        {
            return;
        }

        if (clip == null)
        {
            return;
        }



        // Find an inactive AudioSource to use
        AudioSource availableSource = audioSources.Find(source => !source.isPlaying);
        if (availableSource == null)
        {
            // If no available AudioSource is found, create a new one
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            availableSource = obj.GetComponent<AudioSource>();
            audioSources.Add(availableSource);
        }


        // Assign the AudioClip and play the sound
        availableSource.clip = clip;
        availableSource.volume = 0.4f;
        availableSource.Play();
    }

    public void UpdateStatus_Sounds(bool status)
    {
        if (status == _isSoundsOn)
        {
            return;
        }

        _isSoundsOn = status;
    }





    public void UpdateStatus_Music(bool status)
    {
        if (status == _isMusicOn)
        {
            return;
        }

        _isMusicOn = status;


        if (!_isMusicOn)
        {
            _musicSource.volume = 0;
        }
        else
        {
            _musicSource.volume = MUSIC_VOLUME;
        }
    }














    // Custom music logic
    const float MUSIC_FADE_DURATION = 1f;
    bool _isMusicPlaying;
    bool _isMusicFading;

    const float MUSIC_VOLUME = 0.2f;

    public void TryToRemoveMusic()
    {
        if (!_isMusicPlaying)
            return;


        _musicSource.DOFade(0.0f, MUSIC_FADE_DURATION / 2f)
        .OnComplete(() => 
        {
            if (!_isMusicFading)
            {
                _isMusicPlaying = false;
            }
        });
    }

    public void TryToPlayMusic(AudioClip musicClip)
    {
        if (musicClip == null)
            return;



        _isMusicFading = true;



        if (_isMusicPlaying)
        {
            // Fade out current music and then play musicClip1
            _musicSource.DOFade(0.0f, MUSIC_FADE_DURATION / 2f).OnComplete(() =>
            {
                _musicSource.clip = musicClip;
                _musicSource.Play();

                float ToValue = MUSIC_VOLUME;
                if (!_isMusicOn)
                {
                    ToValue = 0;
                }

                _musicSource.DOFade(ToValue, MUSIC_FADE_DURATION / 2f)
                .OnComplete(() =>
                {
                    _isMusicPlaying = true;
                    _isMusicFading = false;
                });
            });
        }
        else
        {
            _musicSource.clip = musicClip;
            _musicSource.Play();

            float ToValue = MUSIC_VOLUME;
            if (!_isMusicOn)
            {
                ToValue = 0;
            }

            _musicSource.DOFade(ToValue, MUSIC_FADE_DURATION / 2f)
            .OnComplete(() =>
            {
                _isMusicPlaying = true;
                _isMusicFading = false;
            });
        }
    }




    public void TryToHideMusic()
    {
        if (!_isMusicPlaying)
            return;

        if (_isMusicFading)
            return;


        float ToValue = MUSIC_VOLUME;
        if (!_isMusicOn)
        {
            ToValue = 0;
        }


        _musicSource.DOFade((ToValue / 3), MUSIC_FADE_DURATION / 2f);
    }

    public void TryToReturnMusic()
    {
        if (!_isMusicPlaying)
            return;

        if (_isMusicFading)
            return;

        float ToValue = MUSIC_VOLUME;
        if (!_isMusicOn)
        {
            ToValue = 0;
        }

        _musicSource.DOFade((ToValue), MUSIC_FADE_DURATION / 2f);
    }
}