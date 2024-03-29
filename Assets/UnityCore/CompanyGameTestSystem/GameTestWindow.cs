using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class GameTestWindow : EditorWindow
{

    const string GENERAL_NAME = "Base Settings";
    string myString;




    [MenuItem ("IceShardCompany/GameTest")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(GameTestWindow));
    }


    void OnGUI()
    {

        // Skip if game is not playing
        if (!Application.isPlaying)
        {
            GUILayout.Label("Start the game to use game-test-panel", EditorStyles.boldLabel);
            return;
        }
        





        // General header
        GUILayout.Space(10);
        GUILayout.Label(GENERAL_NAME, EditorStyles.boldLabel);



        // Button test
        if (GUILayout.Button("Add 100 Coins"))
        {
            PlayerDataManager.Instance.AddCoins(100);
        }

        // Button test
        if (GUILayout.Button("Add 100 Crystals"))
        {
            PlayerDataManager.Instance.AddCrystals(100);
        }


        // Label test
        if (!string.IsNullOrEmpty(AuthenticationManager.Instance.PlayerId))
        EditorGUILayout.LabelField("PLAYER ID: ", AuthenticationManager.Instance.PlayerId);
        

        // Text field
        // myString = EditorGUILayout.TextField ("Text Field", myString);



        // EditorGUILayout.LabelField("Time since start: ", EditorApplication.timeSinceStartup.ToString());
        // Repaint();
    }
}