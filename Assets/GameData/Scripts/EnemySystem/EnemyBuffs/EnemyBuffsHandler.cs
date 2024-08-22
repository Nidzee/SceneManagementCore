using System.Collections.Generic;
using UnityEngine;



public enum EnemyBuffType
{
    None = 0,

    Poison = 1,
    FrostBite = 2,
    FireDamage = 3,
}




public class EnemyBuffsHandler : MonoBehaviour
{
    [SerializeField] bool _canApplyFrost = true;
    [SerializeField] bool _canApplyPoison = true;
    [SerializeField] bool _canApplyFire = true;



    Dictionary<EnemyBuffType, EnemyBasicBuff> _appliedEnemyBuff = new Dictionary<EnemyBuffType, EnemyBasicBuff>();
    BasicEnemy _thisEnemy;












    // Initialization logic
    public void Initialize(BasicEnemy enemyUnit)
    {
        _thisEnemy = enemyUnit;
        _appliedEnemyBuff = new Dictionary<EnemyBuffType, EnemyBasicBuff>();
    }

    public void RegisterNewWeaponBuff(EnemyBuffType weaponBuff, int damagePoints)
    {

        // [0] Check if this buff is already present on enemy
        EnemyBuffType buffType = weaponBuff;
        if (_appliedEnemyBuff.TryGetValue(buffType, out var buffComponent))
        {
            buffComponent.RegisterHitAgain();
            return;
        }





        if (buffType == EnemyBuffType.Poison)
        {
            if (!_canApplyPoison)
                return;


            return;
        }

        if (buffType == EnemyBuffType.FrostBite)
        {
            if (!_canApplyFrost)
                return;


            return;
        }

        if (buffType == EnemyBuffType.FireDamage)
        {
            if (!_canApplyFire)
                return;

            return;
        }






        CustomLogger.LogError("---------------------------------------");
        CustomLogger.LogError("TRY TO APPLY BUFF THAT IS NOT SUPPORTED");
        CustomLogger.LogError("---------------------------------------");
    }

    public void RemoveBuffFromPlayer(EnemyBuffType buffType)
    {
        // [0] Check if we can remove buff from player
        if (_appliedEnemyBuff.ContainsKey(buffType))
        {
            var buffComponent = _appliedEnemyBuff[buffType];
            Destroy(buffComponent);
            _appliedEnemyBuff.Remove(buffType);
            return;
        }


        CustomLogger.LogError("---------------------------------------");
        CustomLogger.LogError("CAN NOT REMOVE BUFF -> NO BUFF ON ENEMY");
        CustomLogger.LogError("---------------------------------------");
    }












    void ApplyFrostBite()
    {

    }

    void ApplyPoison()
    {

    }

    void ApplyFire()
    {
        
    }











    // Pause resume logic
    public void PauseBuffsLogic()
    {
        foreach (var buff in _appliedEnemyBuff)
            buff.Value.PauseBuffsLogic();
    }
    public void ResumeBuffsLogic()
    {
        foreach (var buff in _appliedEnemyBuff)
            buff.Value.ResumeBuffsLogic();
    }
}