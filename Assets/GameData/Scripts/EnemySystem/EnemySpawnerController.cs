using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using UnityEngine;





public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    [SerializeField] BasicEnemy _prefab;
    [SerializeField] EnemyConfig _config;


    public void Initialize()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        Debug.Log("Create enemy");
        var enemy = Instantiate(_prefab, _spawnPoint.transform.position, Quaternion.identity);
        enemy.Initialize(_config, (t) => {Debug.Log("Enemy died. Add coins: " + t);});
        enemy.gameObject.name = "1";


        Debug.Log("Create finished: " + enemy);
        Debug.Log("Create finished: " + enemy.transform.position);
    }
}