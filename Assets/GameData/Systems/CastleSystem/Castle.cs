using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;





public class Castle : BasicAliveUnit
{

    [SerializeField] HealthBarHandler _healthBarHandler;
    [SerializeField] CastleConfig _castleConfig;
    [SerializeField] Renderer _castleCoreRenderer;
    [SerializeField] Transform _selectionOverlay;
    [SerializeField] SoundsHandler_Tower _soundsHandler;


    [SerializeField] AudioClip _interractSound;
    [SerializeField] List<AudioClip> _acceptDamageSound;



    CastleConfig _myConfig;
    public CastleConfig Config => _myConfig;
    public int CurrentHealth => _currentHealth;
    int _currentHealth;






    [HideInInspector] public UnityEvent OnLevelLost = new UnityEvent();
    [HideInInspector] public UnityEvent OnDamageTaken = new UnityEvent();
    [HideInInspector] public UnityEvent OnCastleClicked = new UnityEvent();






    public void Initalize()
    {
        _myConfig = _castleConfig;


        HealthMax = _myConfig.HealthAmount;
        _currentHealth = HealthMax;
        _castleCoreRenderer.material.color = _myConfig.Color;


        _healthBarHandler.Initialize(HealthMax, _currentHealth);
    }

    public void HandleClickOnCastle()
    {
        _soundsHandler.PlaySound(_interractSound);
        OnCastleClicked.Invoke();
    }

    public void ActivateSelectionOverlay()
    {
        _selectionOverlay.gameObject.SetActive(true);
    }
    public void HideSelectionOverlay()
    {
        _selectionOverlay.gameObject.SetActive(false);
    }






























    public override void AcceptDamage(AspectType damageAspect, int damageIncomeValue)
    {

        int resultDamage = damageIncomeValue;


        // Apply damage
        _currentHealth -= resultDamage;
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }


        UpdateHealthBar(resultDamage, true);
        OnDamageTaken.Invoke();
    }






    public override void Die()
    {
        Debug.Log("Castle Die");
        OnLevelLost.Invoke();
    }

    public override void PauseUnit()
    {
    }

    public override void ResumeUnit()
    {
    }







    void UpdateHealthBar(int damagePoints, bool animteDamageLabel) => _healthBarHandler?.UpdateHealthSlider(_currentHealth, animteDamageLabel, damagePoints);
}