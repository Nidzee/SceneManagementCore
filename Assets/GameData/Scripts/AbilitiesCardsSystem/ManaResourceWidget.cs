using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;





public class ManaResourceWidget : MonoBehaviour
{
    [SerializeField] TMP_Text _resourceCounter;
    [SerializeField] Image _resourceProgressImage;





    public void Initialize(int amount, float progress)
    {
        _resourceCounter.text = amount.ToString();
        _resourceProgressImage.fillAmount = progress;
    }

    public void UpdateProgressBar(float progress)
    {
        _resourceProgressImage.fillAmount = progress;
    }

    public void UpdateResourcesAmount(int amount)
    {
        _resourceCounter.text = amount.ToString();
    }
}