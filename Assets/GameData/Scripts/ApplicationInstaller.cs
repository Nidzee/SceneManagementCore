using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;







public class ApplicationInstaller : MonoInstaller
{
    [SerializeField] EnemyConfigContainer _enemiesConfig;
    [SerializeField] GameSoundsConfig _gameSoundsConfig;



    public override void InstallBindings()
    {
        Container.BindInstance(_enemiesConfig);
        Container.BindInstance(_gameSoundsConfig);


        Container.BindInterfacesAndSelfTo<GameSceneTransferingData>().AsSingle();

        InstallService();
    }

    private void InstallService()
    {

    }
}