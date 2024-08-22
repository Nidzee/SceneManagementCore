using Cysharp.Threading.Tasks;
using UnityEngine;

public class BottomMenuPanel : MonoBehaviour
{
    [SerializeField] BottomMenuButton _mainMenuSceneButton;   
    [SerializeField] BottomMenuButton _gameStoreButton;   
    [SerializeField] BottomMenuButton _comingSoonButton;




    public void Initialize()
    {
        var activeScene = SceneLoader.GetActiveScene();


        
        _mainMenuSceneButton.Initialize(activeScene is MainMenuScene);
        _mainMenuSceneButton.OnButtonClick.AddListener(OnMainMenuSceneButtonClick);


        _gameStoreButton.Initialize(activeScene is GameStoreScene);
        _gameStoreButton.OnButtonClick.AddListener(OnGameStoreSceneButtonClick);


        _comingSoonButton.Initialize(false);
        _comingSoonButton.OnButtonClick.AddListener(OnComingSoonButtonClick);
    }







    // Buttons click handlers
    void OnMainMenuSceneButtonClick()
    {
        SceneLoader.LoadScene<MainMenuScene>("MainMenuScene", SceneLoader.LoadingAnimationType.WithAnimation).Forget();
    }
    
    void OnGameStoreSceneButtonClick()
    {
        SceneLoader.LoadScene<GameStoreScene>("GameStoreScene").Forget();
    }
    
    void OnComingSoonButtonClick()
    {
        Debug.Log("Comming soon");
    }
}