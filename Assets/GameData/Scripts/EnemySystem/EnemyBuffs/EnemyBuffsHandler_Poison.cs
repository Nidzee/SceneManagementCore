using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;




public class EnemyBuffsHandler_Poison : EnemyBasicBuff
{
    public EnemyBuffType EnemyBuffType = EnemyBuffType.Poison;




    bool _isBlocked = true;
    int _damagePoints;
    float _duration;
    float _currentTimePassed;
    float _currentTimePassed_secondTracker;
    EnemyBuffData_Poison _buffData;
    BasicEnemy _thisEnemy;
    ParticleSystem _auraParticles;










    public override void Initialize(BasicEnemy thisEnemy)
    {
        _isBlocked = true;
        _thisEnemy = thisEnemy;
        _currentTimePassed = 0f;
        _currentTimePassed_secondTracker = 0f;
        _buffData = TemproraryDistributor.Instance.EnemyBuffData_Poison;
        _damagePoints = _buffData.DamagePointsPerSecond;
        _duration = _buffData.Duration_Seconds;


        LaunchBuffAction();
    }

    public void LaunchBuffAction()
    {
        _isBlocked = false;


        if (_buffData.ImpactParticles != null)
            _thisEnemy.TryToCreateParticles(_buffData.ImpactParticles, false);

        if (_buffData.AuraParticles != null)
            _thisEnemy.TryToCreateParticles(_buffData.AuraParticles, false);

        if (_buffData.UseCustomHealthBarColor)
            _thisEnemy.ChangeHealthSliderColor(_buffData.HealthColor);

        if (_buffData.UseCustomModelBarColor)
            _thisEnemy.ChangeModelColor(_buffData.ModelColor);
    }

    public override void RegisterHitAgain()
    {
        if (_buffData.ImpactParticles != null)
            _thisEnemy.TryToCreateParticles(_buffData.ImpactParticles, false);
    }

    public void Update()
    {
        if (_isBlocked)
            return;


        _currentTimePassed += Time.deltaTime;
        _currentTimePassed_secondTracker += Time.deltaTime;



        // If whole second passed -> apply damage
        if (_currentTimePassed_secondTracker >= 1)
        {
            _currentTimePassed_secondTracker -= 1;
            _thisEnemy.AcceptDamage(AspectType.None, _damagePoints);

            // Launch attack sound
            var randomDamageSound = RandomElementFromList.GetRandomElement(_buffData.ApplyDamageSound);
            AudioManager.Instance.PlaySound(randomDamageSound);
        }



        // Check if time is up
        if (_currentTimePassed >= _duration)
        {
            _isBlocked = true;
            _thisEnemy.TryToRemoveBuff(EnemyBuffType);

            if (_auraParticles != null)
                _auraParticles.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(_auraParticles.gameObject));

            if (_buffData.UseCustomModelBarColor)
                _thisEnemy.ReturnHealthBarOriginalColor();

            if (_buffData.UseCustomModelBarColor)
                _thisEnemy.ReturnModelOriginalColor();
        }
    }






    

    public override void PauseBuffsLogic() => _isBlocked = true;
    public override void ResumeBuffsLogic() => _isBlocked = false;
}