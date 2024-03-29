using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

public class PlayerDataManager : GeneralManager<PlayerDataManager>
{
    public PlayerSaveData PlayerData;

    [HideInInspector] public UnityEvent OnDataChanged = new UnityEvent();

    const string LOGGER_KEY = "[PlayerDataManager]";



    

    public override async UniTask Initialize()
    {
        Debug.Log(LOGGER_KEY + " Initialization-started");
        await Init();
        Debug.Log(LOGGER_KEY + " Initialization-finished");
    }





    async UniTask Init()
    {

        // Read actual data from cloud
        await CoinsManager.Instance.Initialize();
        await CrystalsManager.Instance.Initialize();


        // Save copy of data of each instance
        PlayerData = new PlayerSaveData
        {
            CoinsData = CoinsManager.Instance.GetCopy(),
            CrystalsData = CrystalsManager.Instance.GetCopy(),
        };
    }





    public void AddCoins(int amount)
    {
        // Increase coins amount
        PlayerData.CoinsData.CoinsAmount += amount;


        CoinsManager.Instance.SavePlayerData(PlayerData.CoinsData);


        OnDataChanged.Invoke();
    }

    
    public void AddCrystals(int amount)
    {
        // Increase coins amount
        PlayerData.CrystalsData.CrystalsAmount += amount;


        CrystalsManager.Instance.SavePlayerData(PlayerData.CrystalsData);


        OnDataChanged.Invoke();
    }
}