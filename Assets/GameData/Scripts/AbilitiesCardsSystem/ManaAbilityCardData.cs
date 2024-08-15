using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ManaAbilityCardData", menuName = "SoskaGames/ManaAbilities/ManaAbilityCardData")]
public class ManaAbilityCardData : ScriptableObject
{
    public ManaAbilityCardType ManaAbilityCardType;
    public Sprite Icon;
    public int ManaAmountNeed;
}



[System.Serializable]
public enum ManaAbilityCardType
{
    None = 0,
    SunStrike = 1,
    MeteorRain = 2,
    ArrowShower = 3,
}