using System.Collections.Generic;
using System.Collections;
using UnityEngine;





public class SoundsHandler_Tower : MonoBehaviour
{
    public void PlaySound(List<AudioClip> sounds)
    {
        var randomSound = RandomElementFromList.GetRandomElement(sounds);
        PlaySound(randomSound);
    }

    public void PlaySound(AudioClip sound)
    {
        AudioManager.Instance.PlaySound(sound);
    }
}