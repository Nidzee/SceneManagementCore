using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    private CoinsSaveData _coinsData;
    private CrystalsSaveData _crystalsData;


    public CoinsSaveData CoinsData { get => _coinsData; set => _coinsData = value;}
    public CrystalsSaveData CrystalsData { get => _crystalsData; set => _crystalsData = value;}
}