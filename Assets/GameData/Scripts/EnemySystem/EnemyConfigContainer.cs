using System.Collections.Generic;
using UnityEngine;







[CreateAssetMenu(fileName = "EnemyConfigContainer", menuName = "SoskaGames/EnemyConfigContainer", order = 1)]
public class EnemyConfigContainer : ScriptableObject
{
    [SerializeField] List<EnemyTypeConfig> _allEnemiesConfigCollect = new List<EnemyTypeConfig>();






    public EnemyTypeConfig TryToGetEnemyConfig(EnemyType enemyType)
    {
        if (_allEnemiesConfigCollect == null || _allEnemiesConfigCollect?.Count <= 0)
        {
            CustomLogger.LogError("ERROR! MISSING ALL CONFIGS FOR ENEMIES!");
            return null;
        }


        foreach (var enemyConfig in _allEnemiesConfigCollect)
        {
            if (enemyConfig.EnemyType == enemyType)
            {
                return enemyConfig;
            }
        }


        CustomLogger.LogError("MISSING CONFIG FOR: " + enemyType);
        return null;
    }
}



[System.Serializable]
public class EnemyTypeConfig
{
    [SerializeField] EnemyType _enemyType;
    [SerializeField] BasicEnemy _enemyPrefab;
    [SerializeField] EnemyUnitStats _enemyUnitStats;



    public EnemyType EnemyType => _enemyType;
    public BasicEnemy EnemyPrefab => _enemyPrefab;
    public EnemyUnitStats EnemyStats => _enemyUnitStats;
}





[System.Serializable]
public class EnemyUnitStats
{
    public int MaxHealth;
    public float MoveSpeed;
    public int CoinsDrop;
    public int AttackPoints;

    public EnemyImpactInfo ImpactInfo;
}



[System.Serializable]
public enum EnemyType
{
    None = 0,
    Skeleton = 1,
}


[System.Serializable]
public class EnemyImpactInfo
{
    public AspectTypeToMultiplyer DamageIncome_Sucking;
    public AspectTypeToMultiplyer DamageIncome_Tanking;
}


[System.Serializable]
public class AspectTypeToMultiplyer
{
    public AspectType AspectType;
    public float Multiplyer;
}



[System.Serializable]
public enum AspectType
{
    None = 0,
    Fire = 1,
    Ice = 2,
    Stone = 3,
    Magic = 4,
}