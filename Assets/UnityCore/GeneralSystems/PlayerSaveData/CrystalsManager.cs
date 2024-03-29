using System.Collections.Generic;
using Unity.Services.CloudSave;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CrystalsManager : GeneralManager<CrystalsManager>
{

    // Copy of saved data
    CrystalsSaveData _crystalsSaveData;


    // Ecent to notify all listeners to update visuals
    public UnityEvent OnDataChanged_Crystals = new UnityEvent();









    public override async UniTask Initialize()
    {
        await LoadPlayerCrystalsData();
        PlayerDataManager.Instance.OnDataChanged.AddListener(OnPlayerGameDataChanged);
    }
    




    // Load on initialization only
    async UniTask LoadPlayerCrystalsData()
    {
        _crystalsSaveData = new CrystalsSaveData();


        var loadResult = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {CrystalsSaveData.SAVE_KEY});
        if (loadResult.TryGetValue(CrystalsSaveData.SAVE_KEY, out var coins)) {
            int cloudCrystalsAmount = coins.Value.GetAs<int>();
            _crystalsSaveData.CrystalsAmount = cloudCrystalsAmount;
        }
    }
    




    
    
    void OnPlayerGameDataChanged()
    {
        var runTimeData = PlayerDataManager.Instance.PlayerData.CrystalsData;
        bool isDataEqual = runTimeData.IsEqual(_crystalsSaveData);


        if (!isDataEqual)
        {
            // Refresh actual data copy
            _crystalsSaveData = runTimeData.GetCopy();

            // Fire event to update all related widgets
            OnDataChanged_Crystals.Invoke();
        }
    }






    public void SavePlayerData(CrystalsSaveData dataToSave)
    {
        var saveData = new Dictionary<string, object> 
        { 
            {CrystalsSaveData.SAVE_KEY, dataToSave.CrystalsAmount},
        };

        CloudSaveService.Instance.Data.Player.SaveAsync(saveData).AsUniTask().Forget();
    }







    public CrystalsSaveData GetCopy()
    {
        return _crystalsSaveData.GetCopy();
    }
}









public class CrystalsSaveData
{
    public static string SAVE_KEY = "Crystals";
    public int CrystalsAmount;

    public CrystalsSaveData GetCopy()
    {
        CrystalsSaveData result = new CrystalsSaveData();
        result.CrystalsAmount = this.CrystalsAmount;
        return result;
    }

    public bool IsEqual(CrystalsSaveData compareData)
    {
        if (compareData.CrystalsAmount != this.CrystalsAmount)
        {
            return false;
        }

        return true;
    }
}