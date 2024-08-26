using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public enum EnemyStepSpawnType
{
    Units = 0,
}










[System.Serializable]
public class WaveConfig
{
    public List<WaveStepConfig> StepsList;
}

[System.Serializable]
public class WaveStepConfig
{
    public float TimeToNextStep;
    public EnemyStepSpawnType EnemyStepSpawnType;
    public EnemySpawenPoint RespawnPosition;
    public EnemyType EnemyToSpawn;
    public int AmountToSpawn;
}

[System.Serializable]
public class EnemyRespawnConfig
{
    public float RespawnTime;
    public EnemyType EnemyToSpawn;
    public EnemySpawenPoint RespawnPosition;
}