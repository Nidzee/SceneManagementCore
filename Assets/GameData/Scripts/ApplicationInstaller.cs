using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;







public class ApplicationInstaller : MonoInstaller
{
    [SerializeField] EnemyConfigContainer _enemiesConfig;



    public override void InstallBindings()
    {
        Container.BindInstance(_enemiesConfig);


        Container.BindInterfacesAndSelfTo<GameSceneTransferingData>().AsSingle();

        InstallService();
    }

    private void InstallService()
    {

    }
}