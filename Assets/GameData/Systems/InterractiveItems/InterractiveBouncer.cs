using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;




public class InterractiveBouncer : MonoBehaviour, IInteractiveItem
{
    [SerializeField] Transform _modelOrigin;
    [SerializeField] AudioClip _interractSound;
    [SerializeField] ParticleSystem _interractParticleSystem;


    Sequence _animation;
    Vector3 _originalScale;



    void Awake()
    {
        _originalScale = _modelOrigin.transform.localScale;
    }

    public void Interract()
    {
        LaunchModelAnimation();

        if (_interractSound != null)
            AudioManager.Instance.PlaySound(_interractSound);

        if (_interractParticleSystem != null)
            _interractParticleSystem.Play();
    }



    void LaunchModelAnimation()
    {

        if (_animation != null)
        {
            _animation.Kill();
            _modelOrigin.transform.localScale = _originalScale;
        }


        _animation = DOTween.Sequence();
        _animation.Append(_modelOrigin.transform.DOScale(_originalScale * 1.2f, 0.2f));
        _animation.Append(_modelOrigin.transform.DOScale(_originalScale, 0.1f));
    }
}