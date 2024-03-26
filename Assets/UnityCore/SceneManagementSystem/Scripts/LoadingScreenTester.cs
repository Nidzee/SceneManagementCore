using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static SceneLoader;

public class LoadingScreenTester : MonoBehaviour
{




    void Start()
    {
        DontDestroyOnLoad(this);
    }
    
    
    
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {

        // Skip if no keys pressed
        if (!Input.anyKeyDown)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneLoader.LoadScene<MainMenuScene>(
                "MainMenuScene", 
                LoadingAnimationType.WithAnimation,
                (scene) => 
                {
                    scene.SetPassingData("Main-Menu-Hello!");
                }).Forget();

            return;
        }
        


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneLoader.LoadScene<GameStoreScene>(
                "GameStoreScene", 
                LoadingAnimationType.WithAnimation,
                (scene) => 
                {
                    scene.SetPassingData("Store-Hello!");
                }).Forget();

            return;
        }






        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("[###] RANDOMIZE DATA");
            randomizePlayerSaveData();
            logPlayerSaveData();
            return;
        }

        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("[###] SAVE DATA");
            savePlayerSaveData();
            logPlayerSaveData();
            return;
        }
    }

    void logPlayerSaveData()
    {
        Debug.Log("[###] COINS: " + PlayerDataSaveSystem.PlayerSaveData_Test.Coins);
        Debug.Log("[###] CRYSTALS: " + PlayerDataSaveSystem.PlayerSaveData_Test.Crystals);
    }

    void randomizePlayerSaveData()
    {
        PlayerDataSaveSystem.Instance.RandomizePlayerSaveData();
    }
    
    void savePlayerSaveData()
    {
        PlayerDataSaveSystem.Instance.SavePlayerData();
    }
}
