using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using UnityEngine;




public class AFKRotator : MonoBehaviour
{
    [SerializeField] Vector3 _rotateVector;
    [SerializeField] float roataionDurationFullCycle = 0.5f;



    void Start()
    {
        LaunchAFKRotation();
    }

    void LaunchAFKRotation()
    {
        transform.DORotate(
            _rotateVector,
            roataionDurationFullCycle, 
            RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
    }
}
