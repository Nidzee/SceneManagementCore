using System.Collections.Generic;
using System.Collections;
using UnityEngine;





public class SoundsHandler : MonoBehaviour
{
    [Header("Sounds config")]
    [SerializeField] List<AudioClip> _attackSounds;
    [SerializeField] List<AudioClip> _hitSounds;
    [SerializeField] List<AudioClip> _deathSounds;



    public void PlayAcceptDamageSound() => PlayRandomSound(_hitSounds);
    public void PlayDeathSound() => PlayRandomSound(_deathSounds);
    public void PlayAttackSound() => PlayRandomSound(_attackSounds);

    


    void PlayRandomSound(List<AudioClip> sounds)
    {
        var randomSound = RandomElementFromList.GetRandomElement(sounds);
        PlaySound(randomSound);
    }

    public void PlaySound(AudioClip sound)
    {
        AudioManager.Instance.PlaySound(sound);
    }
}