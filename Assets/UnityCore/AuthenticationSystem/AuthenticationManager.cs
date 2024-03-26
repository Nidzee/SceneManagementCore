using Unity.Services.Authentication;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AuthenticationManager : GeneralManager<AuthenticationManager>
{
    const string LOGGER_KEY = "[Authentication-Manager]";

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
            Debug.Log(LOGGER_KEY + " anonimously authetication completed");
            Debug.Log(LOGGER_KEY + " ID: " + AuthenticationService.Instance.PlayerId);
        }
        catch (AuthenticationException exception)
        {
            Debug.Log(LOGGER_KEY + " Error! Sign in failed");
            Debug.Log(LOGGER_KEY + " Error: " + exception);
        }
    }
}