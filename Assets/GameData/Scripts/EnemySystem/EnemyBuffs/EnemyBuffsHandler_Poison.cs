using DG.Tweening;
using UnityEngine;




public class EnemyBuffsHandler_Poison : EnemyBasicBuff
{
    public EnemyBuffType EnemyBuffType = EnemyBuffType.Poison;

    bool _isBlocked = true;
    int _damagePoints;
    float _duration;
    float _currentTimePassed;
    float _currentTimePassed_secondTracker;

    //BuffData_Poison _buffData;
    BasicEnemy _thisEnemy;


    ParticleSystem _auraParticles;






    public override void Initialize(BasicEnemy thisEnemy, int levelIndex)
    {

        // _isBlocked = true;
        // _thisEnemy = thisEnemy;
        // _currentTimePassed = 0f;
        // _currentTimePassed_secondTracker = 0f;
        // _damagePoints = configData.DamagePointsPerSecond;
        // _duration = configData.Duration_Seconds;

        // TryToCreateParticles(_buffData.ApplyBuffParticles);
        // _auraParticles = TryToCreateParticles(_buffData.AuraParticles);

        // LaunchBuffAction();
    }

    public void LaunchBuffAction()
    {
        // _isBlocked = false;

        // if (_buffData.UseCustomHealthBarColor)
        //     _thisEnemy.ChangeHealthSliderColor(_buffData.HealthColor);

        // if (_buffData.UseCustomModelBarColor)
        //     _thisEnemy.ChangeModelColor(_buffData.ModelColor);
    }

    public override void RegisterHitAgain()
    {
        // TryToCreateParticles(_buffData.ImpactParticles);
    }







    public override void PauseBuffsLogic() => _isBlocked = true;
    public override void ResumeBuffsLogic() => _isBlocked = false;









    public void Update()
    {
        // if (_isBlocked)
        //     return;


        // _currentTimePassed += Time.deltaTime;
        // _currentTimePassed_secondTracker += Time.deltaTime;



        // // If whole second passed -> apply damage
        // if (_currentTimePassed_secondTracker >= 1)
        // {
        //     _currentTimePassed_secondTracker -= 1;
        //     _thisEnemy.AcceptDamage(_damagePoints, false);

        //     // Launch attack sound
        //     var randomDamageSound = RandomElementFromList.GetRandomElement(_buffData.ApplyDamageSound);
        //     AudioManager.Instance.PlaySound(randomDamageSound);
        // }



        // // Check if time is up
        // if (_currentTimePassed >= _duration)
        // {
        //     _isBlocked = true;
        //     _thisEnemy.TryToRemoveBuff(EnemyBuffType);

        //     if (_auraParticles != null)
        //         _auraParticles.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(_auraParticles.gameObject));

        //     if (_buffData.UseCustomModelBarColor)
        //         _thisEnemy.ReturnHealthBarOriginalColor();

        //     if (_buffData.UseCustomModelBarColor)
        //         _thisEnemy.ReturnModelOriginalColor();
        // }
    }
}