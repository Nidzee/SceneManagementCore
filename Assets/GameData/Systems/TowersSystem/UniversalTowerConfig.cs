using System.Collections.Generic;
using System.Collections;
using UnityEngine;



[CreateAssetMenu(fileName = "UniversalTowerConfig", menuName = "SoskaGames/TowersSystem/UniversalTowerConfig")]
public class UniversalTowerConfig : ScriptableObject
{
    public TowerType TowerType;
    public int PriceCost;
    public int DetecorRange;
    public string Name;
    public Sprite Icon;
    public GameObject TowerPrefab;


    public WarTowerConfig WarTowerConfig;
}




[System.Serializable]
public class WarTowerConfig
{
    public AspectType DamageAspect;
    public int DamagePoints;
    public float ReloadTime_Seconds;
}




[System.Serializable]
public enum TowerType
{
    WarTower = 0,
    EffectTower = 1,
}