using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class EnemyBasicBuff : MonoBehaviour
{
    public abstract void Initialize(BasicEnemy thisEnemy, int levelIndex);
    public abstract void RegisterHitAgain();



    public abstract void PauseBuffsLogic();
    public abstract void ResumeBuffsLogic();
}