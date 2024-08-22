using System.Collections.Generic;
using System.Collections;
using UnityEngine;




[CreateAssetMenu(fileName = "GameLevelConfig", menuName = "SoskaGames/GameConfig/GameLevelConfig", order = 3)]
public class GameLevelConfig : ScriptableObject
{
    public List<EnemyWaveConfig> EnemiesWavesConfig = new List<EnemyWaveConfig>();
}