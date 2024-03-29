using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BasicButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] AudioClip _clickSound;

    [HideInInspector] public UnityEvent OnClick => _button.onClick;

    public Button BaseButton => _button;
}