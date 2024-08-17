using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;





public class Castle : BasicAliveUnit
{

    [SerializeField] HealthBarHandler _healthBarHandler;
    [SerializeField] SoundsHandler _soundsHandler;
    [SerializeField] CastleConfig _castleConfig;
    [SerializeField] Renderer _castleCoreRenderer;


    CastleConfig _myConfig;
    int _currentHealth;






    [HideInInspector] public UnityEvent OnLevelLost = new UnityEvent();






    public void Initalize()
    {
        _myConfig = _castleConfig;


        HealthMax = _myConfig.HealthAmount;
        _currentHealth = HealthMax;
        _castleCoreRenderer.material.color = _myConfig.Color;


        _healthBarHandler.Initialize(HealthMax, _currentHealth);
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