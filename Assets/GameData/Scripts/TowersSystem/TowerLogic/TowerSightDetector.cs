using System.Collections.Generic;
using System.Collections;
using UnityEngine;




public class TowerSightDetector : MonoBehaviour
{
    [SerializeField] Transform _sightAreaObject;
    WarTower _thisTower;



    public void Initialize(WarTower tower, int sightRadius)
    {
        _thisTower = tower;
        _sightAreaObject.transform.localScale = new Vector3(sightRadius, sightRadius, sightRadius);
    }



    public void OnTriggerEnter(Collider other)
    {
        // Skip if not enemy detected
        BasicEnemy enemyComponent = other.gameObject.GetComponent<BasicEnemy>();
        if (enemyComponent == null)
            return;


        _thisTower.TryToRegisterEnemy(enemyComponent);
    }
    public void OnTriggerExit(Collider other)
    {
        // Skip if not enemy detected
        BasicEnemy enemyComponent = other.gameObject.GetComponent<BasicEnemy>();
        if (enemyComponent == null)
            return;


        _thisTower.TryToRemoveEnemy(enemyComponent);
    }
}