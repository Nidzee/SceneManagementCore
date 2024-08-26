using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;



public class HealthBarHandler : MonoBehaviour
{
    const float DAMAGE_POINTS_INDICATOR_ANIMATION_DURATION = 0.2f;

    [Header("Health-slider component")]
    [SerializeField] Transform _hpBarWidget;
    [SerializeField] Slider _healthProgressBar;
    

    [Header("Fake-Health-slider component")]
    [SerializeField] Slider _healthProgressBar_tweener;
    [SerializeField] float _tweenDuration;

    [Header("Additional damage-label")]
    [SerializeField] TMP_Text _takeDamageLabel;
    [SerializeField] Image _fillImage;
    Sequence _damageLabelAnimation;


    Color _originalColor;




    public void Initialize(int maxHealth, int currentHealth)
    {

        _originalColor = _fillImage.color;


        // Initialize progress bar
        RebalanceProgressBar(maxHealth, currentHealth);


        // Hide all elements
        _hpBarWidget.gameObject.SetActive(false);
        _takeDamageLabel?.gameObject.SetActive(false);
    }

    public void RebalanceProgressBar(int maxHealth, int currentHealth)
    {
        // Initialize progress bar
        _healthProgressBar.maxValue = maxHealth;
        _healthProgressBar.minValue = 0;
        _healthProgressBar.value = currentHealth;


        _healthProgressBar_tweener.maxValue = maxHealth;
        _healthProgressBar_tweener.minValue = 0;
        _healthProgressBar_tweener.value = currentHealth;

    }






    // After-damage logic
    public void UpdateHealthSlider_IncomeHealth(int currentHealth)
    {
        // Activate health-bar and update value
        _hpBarWidget.gameObject.SetActive(true);
        _healthProgressBar.value = currentHealth;


        _healthProgressBar_tweener.DOKill();
        _healthProgressBar_tweener.DOValue(currentHealth, _tweenDuration);


        ResetShowCooldown();
    }

    public void UpdateHealthSlider(int currentHealth, bool animteDamageLabel, int damagePoints)
    {
        // Activate health-bar and update value
        _hpBarWidget.gameObject.SetActive(true);
        _healthProgressBar.value = currentHealth;


        _healthProgressBar_tweener.DOKill();
        _healthProgressBar_tweener.DOValue(currentHealth, _tweenDuration);



        ResetShowCooldown();



        // [2] If required animation of label -> do it
        if (animteDamageLabel)
        {
            AnimateDamageTakenLabel(damagePoints);
        }
    }

    public void AnimateDamageTakenLabel(int damageTaken)
    {
        // Skip if damage-label is not provided
        if (_takeDamageLabel == null)
            return;


        // Kill previous animation if provided
        _takeDamageLabel.color = Color.white;
        _damageLabelAnimation?.Kill();
        _takeDamageLabel.transform.DOKill();



        // Reset label object
        _takeDamageLabel.gameObject.SetActive(true);
        _takeDamageLabel.transform.localScale = Vector3.zero;
        _takeDamageLabel.text = damageTaken.ToString();



        // Launch new animation
        _damageLabelAnimation = DOTween.Sequence();
        _damageLabelAnimation.Append(
            _takeDamageLabel.transform.DOScale(1, DAMAGE_POINTS_INDICATOR_ANIMATION_DURATION)
            .SetEase(Ease.OutBack));
        _damageLabelAnimation.AppendInterval(1f);
        _damageLabelAnimation.Append(
            _takeDamageLabel.transform.DOScale(0, DAMAGE_POINTS_INDICATOR_ANIMATION_DURATION)
            .SetEase(Ease.InBack));
    }









    // Timer logic to show widget
    const float VISUALS_STAY_DURATION = 3f;
    float _currentShowDuration = 0;

    void ResetShowCooldown()
    {
        _currentShowDuration = VISUALS_STAY_DURATION;
    }

    public void Update()
    {
        UpdateTimerHideCoolDown();
    }

    void UpdateTimerHideCoolDown()
    {
        // Skip if time is up
        if (_currentShowDuration <= 0)
            return;


        // Reduce time
        _currentShowDuration -= Time.deltaTime;

        // If time is up -> hide indicator
        if (_currentShowDuration <= 0)
        {
            _currentShowDuration = 0;
            _hpBarWidget.gameObject.SetActive(false);
            _takeDamageLabel?.gameObject.SetActive(false);
        }
    }








    public void SetHealthSliderColor(Color sliderColor)
    {
        _fillImage.color = sliderColor;
    }

    public void SetOriginalHealthProgressBarColor()
    {
        _fillImage.color = _originalColor;
    }
}