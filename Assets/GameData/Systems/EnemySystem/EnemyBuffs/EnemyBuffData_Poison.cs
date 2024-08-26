using System.Collections.Generic;
using System.Collections;
using UnityEngine;




[System.Serializable]

public class EnemyBuffData_Poison
{
    [Header("Damage config")]
    public float Probability;
    public float Duration_Seconds;
    public int DamagePointsPerSecond;



    [Header("Visuals config")]
    public ParticleSystem AuraParticles;
    public ParticleSystem ImpactParticles;



    [Header("Sounds config")]
    public List<AudioClip> ApplyPoisonSound;
    public List<AudioClip> ApplyDamageSound;




    [Header("Health custom slider color")]
    public bool UseCustomHealthBarColor;
    public Color HealthColor;


    [Header("Custom model material color")]
    public bool UseCustomModelBarColor;
    public Color ModelColor;
}
