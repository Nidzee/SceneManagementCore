using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "GameSoundsConfig", menuName = "SoskaGames/Audio/GameSoundsConfig", order = 1)]
public class GameSoundsConfig : ScriptableObject
{
    [SerializeField] List<MainMenuMusicToEnvironmentType> _mainMenmuMusic;
    [SerializeField] AudioClip _gameSceneMusic;
    [SerializeField] List<GameSoundTypeToResource> _gameSoundsConfig;




    public AudioClip GetMainMenuMusic(EnvironmentType type)
    {
        List<AudioClip> musicToPlaye = new List<AudioClip>();
        foreach (var musicCollection in _mainMenmuMusic)
        {
            if (musicCollection.Type != type)
                continue;

            musicToPlaye = musicCollection.Music;
        }




        var randomMainMenuAmbient = RandomElementFromList.GetRandomElement(musicToPlaye);
        return randomMainMenuAmbient;
    }

    public AudioClip GetgameSceneMusic()
    {
        return _gameSceneMusic;
    }
    public AudioClip GetSoundByType(GameSoundType type)
    {
        foreach (var config in _gameSoundsConfig)
        {
            if (config.Type == type)
            {
                return config.Sound;
            }
        }


        return null;
    }
}




[System.Serializable]
public enum EnvironmentType
{
    Forest = 0,
    Winter = 1,
}


[System.Serializable]
public class MainMenuMusicToEnvironmentType
{
    public EnvironmentType Type;
    public List<AudioClip> Music;
}


[System.Serializable]
public class GameSoundTypeToResource
{
    public GameSoundType Type;
    public AudioClip Sound;
}



public enum GameSoundType
{
    None = 0,
    
    WaveStart = 1,
    LevelLose = 2,
    LevelWin = 3,
}