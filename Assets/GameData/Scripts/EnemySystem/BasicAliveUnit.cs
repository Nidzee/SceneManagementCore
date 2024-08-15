using System.Collections.Generic;
using System.Collections;
using UnityEngine;




public abstract class BasicAliveUnit : MonoBehaviour
{
    [HideInInspector] public int HealthMax;


    public abstract void AcceptDamage(AspectType damageAspect, int damageIncomeValue);
    public abstract void Die();


    
    // Pause/resume logic
    public abstract void PauseUnit();
    public abstract void ResumeUnit();
}