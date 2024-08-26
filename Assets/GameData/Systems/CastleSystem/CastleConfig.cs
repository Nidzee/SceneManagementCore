using System.Collections.Generic;
using System.Collections;
using UnityEngine;




[CreateAssetMenu(fileName = "CastleConfig", menuName = "SoskaGames/Castle/CastleConfig")]
public class CastleConfig : ScriptableObject
{
    public int HealthAmount;
    public Color Color;
    public Sprite Icon;
    public string Name;
}