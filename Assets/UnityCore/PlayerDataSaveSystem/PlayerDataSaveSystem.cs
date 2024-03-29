// using System.Collections.Generic;
// using Unity.Services.CloudSave;
// using Cysharp.Threading.Tasks;
// using UnityEngine;

// public class PlayerDataSaveSystem : GeneralManager<PlayerDataSaveSystem>
// {
//     public static PlayerSaveData_Test PlayerSaveData_Test = new PlayerSaveData_Test() {Coins = 100, Crystals = 20};
//     const string LOGGER_KEY = "[PlayerData-SaveSystem]";







//     public override async UniTask Initialize()
//     {
//         Debug.Log(LOGGER_KEY + " Initialization-started");
//         await LoadPlayerData();
//         Debug.Log(LOGGER_KEY + " Initialization-finished");
//     }


//     // Load on initialization only
//     async UniTask LoadPlayerData()
//     {
//         var loadResult = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {"Coins", "Crystals"});
        



//         if (loadResult.TryGetValue("Crystals", out var crystals)) {
//             int cloudCrystalsAmount = crystals.Value.GetAs<int>();
//             PlayerSaveData_Test.Crystals = cloudCrystalsAmount;
//             Debug.Log("[###] GET FROM CLOUD: CRYSTALS: " + cloudCrystalsAmount);
//         }
        



//         if (loadResult.TryGetValue("Coins", out var coins)) {
//             int cloudCoinsAmount = coins.Value.GetAs<int>();
//             PlayerSaveData_Test.Coins = cloudCoinsAmount;
//             Debug.Log("[###] GET FROM CLOUD: COINS: " + cloudCoinsAmount);
//         }
//     }







//     // Custom action for testing-purposes
//     public void RandomizePlayerSaveData()
//     {
//         PlayerSaveData_Test.Coins = Random.Range(0, 100);
//         PlayerSaveData_Test.Crystals = Random.Range(0, 100);
//     }



//     // Save after some values changed
//     public void SavePlayerData()
//     {
//         var saveData = new Dictionary<string, object> 
//         { 
//             {"Coins", PlayerSaveData_Test.Coins},
//             {"Crystals", PlayerSaveData_Test.Crystals},
//         };

//         CloudSaveService.Instance.Data.Player.SaveAsync(saveData).AsUniTask().Forget();
//     }
// }

// public class PlayerSaveData_Test
// {
//     public int Coins;
//     public int Crystals;
// }