using Unity.Services.Authentication;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AuthenticationManager : GeneralManager<AuthenticationManager>
{
    const string LOGGER_KEY = "[Authentication-Manager]";

    public string PlayerId;



    public override async UniTask Initialize()
    {
        Debug.Log(LOGGER_KEY + " Initialization-started");
        await SignInAnonimously();
        Debug.Log(LOGGER_KEY + " Initialization-finished");
    }


    public async UniTask SignInAnonimously()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            PlayerId = AuthenticationService.Instance.PlayerId;
            Debug.Log(LOGGER_KEY + " anonimously authetication completed");
            Debug.Log(LOGGER_KEY + " ID: " + PlayerId);
        }
        catch (AuthenticationException exception)
        {
            Debug.Log(LOGGER_KEY + " Error! Sign in failed");
            Debug.Log(LOGGER_KEY + " Error: " + exception);
        }
    }
}