using System.Collections.Generic;
using Unity.Services.CloudSave;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

public class CoinsManager : GeneralManager<CoinsManager>
{

    CoinsSaveData _coinsSaveData;
    public UnityEvent OnDataChanged_Coins = new UnityEvent();













    public override async UniTask Initialize()
    {
        await LoadPlayerCoinsData();
        PlayerDataManager.Instance.OnDataChanged.AddListener(OnPlayerGameDataChanged);
    }





    
    // Load on initialization only
    async UniTask LoadPlayerCoinsData()
    {
        // Create data of coins
        _coinsSaveData = new CoinsSaveData();


        // Try to load data about coins
        var loadResult = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {CoinsSaveData.SAVE_KEY});
        if (loadResult.TryGetValue(CoinsSaveData.SAVE_KEY, out var coins)) {
            int cloudCoinsAmount = coins.Value.GetAs<int>();
            _coinsSaveData.CoinsAmount = cloudCoinsAmount;
        }
    }






    
    void OnPlayerGameDataChanged()
    {
        var runTimeData = PlayerDataManager.Instance.PlayerData.CoinsData;
        bool isDataEqual = runTimeData.IsEqual(_coinsSaveData);


        if (!isDataEqual)
        {
            // Refresh actual data copy
            _coinsSaveData = runTimeData.GetCopy();

            // Fire event to update all related widgets
            OnDataChanged_Coins.Invoke();
        }
    }





    
    public void SavePlayerData(CoinsSaveData dataToSave)
    {
        var saveData = new Dictionary<string, object> 
        { 
            {CoinsSaveData.SAVE_KEY, dataToSave.CoinsAmount},
        };

        CloudSaveService.Instance.Data.Player.SaveAsync(saveData).AsUniTask().Forget();
    }








    public CoinsSaveData GetCopy()
    {
        return _coinsSaveData.GetCopy();
    }
}






public class CoinsSaveData
{
    public static string SAVE_KEY = "Coins";
    public int CoinsAmount;

    public CoinsSaveData GetCopy()
    {
        CoinsSaveData result = new CoinsSaveData();
        result.CoinsAmount = this.CoinsAmount;
        return result;
    }

    public bool IsEqual(CoinsSaveData compareData)
    {
        if (compareData.CoinsAmount != this.CoinsAmount)
        {
            return false;
        }

        return true;
    }
}