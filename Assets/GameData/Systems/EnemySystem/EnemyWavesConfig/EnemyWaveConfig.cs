using System.Collections.Generic;
using UnityEngine;
using System.Linq;





[CreateAssetMenu(fileName = "EnemyWaveConfig", menuName = "SoskaGames/EnemyConfig/EnemyWaveConfig", order = 2)]
public class EnemyWaveConfig : ScriptableObject
{
    [Header("Wave income")]
    public LevelIncomeResultData WaveWinIncome;
    public LevelIncomeResultData WaveLoseIncome;


    [Header("Wave steps")]
    public WaveConfig WaveConfig;
}