using System.Collections.Generic;
using System.Collections;
using UnityEngine;



[CreateAssetMenu(fileName = "EnemyConfig", menuName = "SoskaGames/EnemySystem/EnemyConfig")]
public class BasicEnemy : ScriptableObject
{
    public EnemyType EnemyType;
    public EnemyImpactInfo ImpactInfo;

    public float MoveSpeed;
    public int HealthMax;
    public int ArmourMax;
    public int Damage;
    public int CoinsDrop;
}


[System.Serializable]
public enum EnemyType
{
    None = 0,
    Knight = 1,
    Skeleton = 2,
    Bomber = 3,
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