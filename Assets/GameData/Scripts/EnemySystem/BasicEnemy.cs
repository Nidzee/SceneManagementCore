using UnityEngine.Events;
using UnityEngine;
using System;




public class BasicEnemy : BasicAliveUnit
{

    [Header("Handlers")]
    [SerializeField] HealthBarHandler _healthBarHandler;
    [SerializeField] VisualsHandler _visualsHandler;
    [SerializeField] SoundsHandler _soundsHandler;

    


    // Enemy data
    EnemyConfig _myConfig;
    bool _isDead = false;
    bool _isPaused = false;
    int _healthCurrent;
    float _moveSpeed;




    // Enemy events
    [HideInInspector] public UnityEvent OnEnemyDied = new UnityEvent();





    

    // Initialization logic
    public virtual void Initialize(
        EnemyConfig config,
        Transform walltTarget,
        Action<int> action)
    {
        
        _myConfig = config;
        HealthMax = _myConfig.HealthMax;
        _healthCurrent = _myConfig.HealthMax;
        _moveSpeed = _myConfig.MoveSpeed;


        _healthBarHandler.Initialize(HealthMax, _healthCurrent);
        _visualsHandler?.Initialize();


        PauseController.PauseControllerRef.OnPauseEmited.AddListener(PauseUnit);
        PauseController.PauseControllerRef.OnResumeEmited.AddListener(ResumeUnit);

        OnEnemyDied.AddListener(() => action.Invoke(_myConfig.CoinsDrop));
    }

    public override void AcceptDamage(AspectType damageAspect, int damageIncomeValue)
    {

        int resultDamage = 0;


        // Calculate damage
        var impactInfo = _myConfig.ImpactInfo;
        if (impactInfo == null)
        {
            resultDamage = damageIncomeValue;
        }
        else
        {
            if (impactInfo.DamageIncome_Sucking.AspectType == damageAspect)
                resultDamage = (int)(damageIncomeValue * impactInfo.DamageIncome_Sucking.Multiplyer);

            if (impactInfo.DamageIncome_Tanking.AspectType == damageAspect)
                resultDamage = (int)(damageIncomeValue * impactInfo.DamageIncome_Tanking.Multiplyer);
        }



        // APply damage
        _healthCurrent -= resultDamage;
        if (_healthCurrent <= 0)
        {
            Die();
            return;
        }


        LaunchModelHitAnimation();
        UpdateHealthBar(resultDamage, true);
    }

    public override void Die()
    {

        // [0] Prevent double-death
        if (_isDead) { CustomLogger.LogError("ERROR! Enemy die multiple times"); return; }
        _isDead = true;


        // [1] Add coins to player
        OnEnemyDied.Invoke();


        // [2] Additional logic
        KillVisualsAnimations();
        PlayeDeathParticles();
        PlayDeathSound();

        // [3] Destroy enemy object
        Destroy(gameObject);
    }








    public override void PauseUnit()
    {
    }

    public override void ResumeUnit()
    {
    }



















    
    // Visuals handler
    public void PlayCustomParticles(ParticleSystem ps) => _visualsHandler?.TryToPlayParticles(ps);
    void KillVisualsAnimations() => _visualsHandler?.KillAnimation();
    void PlayeDeathParticles() => _visualsHandler?.PlayDeathParticles();
    void LaunchModelHitAnimation() => _visualsHandler?.LaunchEnemyModelHitAnimation();
    public void ChangeModelColor(Color targetColor) => _visualsHandler?.ChangeModelColor(targetColor);
    public void ReturnModelOriginalColor() => _visualsHandler?.ReturnModelOriginalColor();
    public ParticleSystem TryToCreateParticles(ParticleSystem ps, bool release = false, float scaleMultiplyer = 1) => _visualsHandler?.TryToCreateParticles(ps, release, scaleMultiplyer);




    // Health bar handler
    void UpdateHealthBar(int damagePoints, bool animteDamageLabel) => _healthBarHandler?.UpdateHealthSlider(_healthCurrent, animteDamageLabel, damagePoints);
    public void ChangeHealthSliderColor(Color targetColor) => _healthBarHandler?.SetHealthSliderColor(targetColor);
    public void ReturnHealthBarOriginalColor() => _healthBarHandler?.SetOriginalHealthProgressBarColor();




    // Sounds handler
    public void PlayAttackSound() => _soundsHandler?.PlayAttackSound();
    void PlayAcceptDamageSound() => _soundsHandler?.PlayAcceptDamageSound();
    void PlayDeathSound() => _soundsHandler?.PlayDeathSound();
    public void TryToPlaySound(AudioClip clip) => _soundsHandler?.PlaySound(clip);
}