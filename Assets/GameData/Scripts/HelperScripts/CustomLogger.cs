using UnityEngine;




public static class CustomLogger
{
    static bool LOG_ERRORS = true;
    static string ERROR_KEY = "[[[ERROR]]]";

    static bool LOG_ANALYTICS = true;
    static string ANALYTICS_KEY = "-[ANALYTICS]-";

    static bool LOG_VALIDATOR = true;
    static string VALIDATOR_KEY = "+[DATA-VALIDATOR]+";

    static bool LOG_PAUSE_CONTROLLER = true;
    static string PAUSE_CONTROLLER_KEY = "[PAUSE-CONTROLLER]";

    static bool LOG_GAME_XP_CONTROLLER = true;
    static string GAME_XP_CONTROLLER_KEY = "[GAME-XP-CONTROLLER]";

    static bool LOG_PLAYER_CONTROLLER = true;
    static string PLAYER_CONTROLLER_KEY = "[PLAYER-CONTROLLER]";

    static bool LOG_INPUT_HANDLER = true;
    static string INPUT_HANDLER_KEY = "[INPUT-HANDLER]";

    static bool LOG_ENEMY_SPAWNER = true;
    static string ENEMY_SPAWNER_KEY = "-[ENEMY-SPAWNER]-";




    public static void LogMessage(string message)
    {
        Debug.Log("[###] " + message);
    }

    public static void LogError(string errorMessage)
    {
        if (!LOG_ERRORS)
            return;


        Debug.Log("<color=red>" + ERROR_KEY + " " + errorMessage + "</color>");
    }

    public static void LogAnalytics(string message)
    {
        if (!LOG_ANALYTICS)
            return;
            

        Debug.Log("<color=green>" + ANALYTICS_KEY + " " + message + "</color>");
    }

    public static void LogValidator(string message)
    {
        if (!LOG_VALIDATOR)
            return;


        Debug.Log("<color=orange>" + VALIDATOR_KEY + " " + message + "</color>");
    }

    public static void LogGameXpController(string message)
    {
        if (!LOG_GAME_XP_CONTROLLER)
            return;


        Debug.Log("<color=yellow>" + GAME_XP_CONTROLLER_KEY + " " + message + "</color>");
    }

    public static void LogPauseController(string message)
    {
        if (!LOG_PAUSE_CONTROLLER)
            return;


        Debug.Log("<color=blue>" + PAUSE_CONTROLLER_KEY + " " + message + "</color>");
    }

    public static void LogPlayerController(string message)
    {
        if (!LOG_PLAYER_CONTROLLER)
            return;


        Debug.Log("<color=pink>" + PLAYER_CONTROLLER_KEY + " " + message + "</color>");
    }

    public static void LogInputHandler(string message)
    {
        if (!LOG_INPUT_HANDLER)
            return;


        Debug.Log("<color=gray>" + INPUT_HANDLER_KEY + " " + message + "</color>");
    }

    public static void LogEnemySpawner(string message)
    {
        if (!LOG_ENEMY_SPAWNER)
            return;


        Debug.Log("<color=cyan>" + ENEMY_SPAWNER_KEY + " " + message + "</color>");
    }
}