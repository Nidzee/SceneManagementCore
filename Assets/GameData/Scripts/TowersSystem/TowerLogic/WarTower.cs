using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;




public class WarTower : MonoBehaviour
{

    [SerializeField] FlyingProjectile _projectile;
    [SerializeField] TowerSightDetector _detector;
    [SerializeField] float _projectileFlySpeed = 60;



    List<BasicEnemy> _enemiesInSight;
    WarTowerConfig _myConfig;
    AspectType _damegAspectType;
    int _damagePoints;

    bool _isPaused = false;
    bool _isBlocked = false;
    float _currentShootCooldown;
    float _shootCooldown;
    BasicEnemy ClosestTarget;






    public void Initialize(UniversalTowerConfig config)
    {
        if (config.TowerType != TowerType.WarTower)
        {
            CustomLogger.LogError("Wrong config passed");
            return;
        }


        _myConfig = config.WarTowerConfig;
        _enemiesInSight = new List<BasicEnemy>();
        _damegAspectType = config.WarTowerConfig.DamageAspect;
        _damagePoints = config.WarTowerConfig.DamagePoints;
        _shootCooldown = _myConfig.ReloadTime_Seconds;
        _currentShootCooldown = 0;
        _isPaused = false;
        _isBlocked = true;



        _detector.Initialize(this, config.DetecorRange);


        PauseController.PauseControllerRef.OnPauseEmited.AddListener(Pause);
        PauseController.PauseControllerRef.OnResumeEmited.AddListener(Resume);

        LaunchTowerLogic();
    }

    void LaunchTowerLogic()
    {
        _isBlocked = false;
    }

    void Pause()
    {
        _isPaused = true;
    }
    void Resume()
    {
        _isPaused = false;
    }











    public void Update()
    {
        SortEnemiesByTarget();

        if (_isPaused)
            return;

        if (_isBlocked)
            return;


        UpdateShootCooldown();
        TryToTriggerShotStart();
    }

    void SortEnemiesByTarget()
    {

        // [0] Remove empty references frim list
        List<BasicEnemy> enemiesInSightCLeared = new List<BasicEnemy>();
        foreach (var enemy in _enemiesInSight)
        {
            if (enemy == null)
                continue;
            enemiesInSightCLeared.Add(enemy);
        }
        _enemiesInSight = enemiesInSightCLeared;



        // [1] If no enemies in sight -> remove closest-reference
        if (_enemiesInSight == null || _enemiesInSight?.Count <= 0)
        {
            ClosestTarget = null;
            return;
        }


        // [2] Sort enemies that are in sight
        _enemiesInSight
            .Sort((a, b) => Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position)));


        // [3] Set closest enemy reference
        ClosestTarget = _enemiesInSight.FirstOrDefault();
    }

    void UpdateShootCooldown()
    {
        // Skip if cool down already passed
        if (_currentShootCooldown <= 0)
            return;

        // Reduce timer
        _currentShootCooldown -= Time.deltaTime;

        // Clamp timer if need
        if (_currentShootCooldown <= 0)
            _currentShootCooldown = 0;
    }

    void TryToTriggerShotStart()
    {
        if (_currentShootCooldown > 0)
            return;

        if (_enemiesInSight == null || _enemiesInSight.Count <= 0)
            return;


        TryToShootProjectile();
    }

    void TryToShootProjectile()
    {
        var target = _enemiesInSight.FirstOrDefault();
        if (target == null)
            return;


        _currentShootCooldown = _shootCooldown;

        // [1] Create arrow object and initialize it
        var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);

        // [2] Rottate arrow to target
        projectile.transform.LookAt(target.transform.position);


        // [3] Launch arrow to target
        float distance = Vector3.Distance(projectile.transform.position, target.transform.position);
        float duration = distance / _projectileFlySpeed;



        projectile.LaunchProjectileToTarget(
            duration,
            target,
            () => { if (target != null) LaunchImpactLogic(target); },
            () => {  }
            );
    }

    void LaunchImpactLogic(BasicEnemy target)
    {
        target.AcceptDamage(_damegAspectType, _damagePoints);
    }

    private void OnDrawGizmos()
    {
        // Check if the enemies list is not null and has elements
        if (_enemiesInSight == null || _enemiesInSight.Count <= 0)
            return;
        


        // Set the Gizmo color for the rays
        Gizmos.color = Color.red;

        // Iterate through each enemy in the list
        foreach (var enemy in _enemiesInSight)
        {
            if (enemy != null)
            {
                Gizmos.DrawLine(transform.position, enemy.transform.position);
            }
        }
    }







































    // Enemy detection logic
    public void TryToRegisterEnemy(BasicEnemy enemyDetected)
    {
        if (_enemiesInSight.Contains(enemyDetected))
        {
            CustomLogger.LogMessage("Already detected enemy");
            return;
        }


        _enemiesInSight.Add(enemyDetected);
        CustomLogger.LogMessage("DETECT ENEMY");
    }
    
    public void TryToRemoveEnemy(BasicEnemy enemyLeft)
    {
        _enemiesInSight.Remove(enemyLeft);
        CustomLogger.LogMessage("ENEMY LEFT");
    }
}