using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;





public class CastleHealthWidget : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] TMP_Text _healthLabel;




    public void Initialize(int maxHealth, int currentHealth)
    {
        _healthLabel.text = currentHealth + "/" + maxHealth;


        float fillRate = currentHealth / maxHealth;
        _fillImage.fillAmount = fillRate;
    }
}