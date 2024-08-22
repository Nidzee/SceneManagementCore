using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Zenject;





public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] List<EnemySpawnPoint> _possibleSpawnPoints;
    [SerializeField] Transform _allEnemiesParent;


    
    // Main controllers
    bool _isPaused = true;
    bool _isTimerNeedToWork = false;

    
    
    // Temers data
    float _wavePlayTime = 0;
    float _thisStepTimePassed = 0;
    float _thisStepTimePassed_ForNextWaveChange = 0;





    // Data about all waves
    int _thisStepIndex_Max = 0;
    int _thisStepIndex_Current = 0;

    int _thisStepIndex_SubstepIndex_Max = 0;
    int _thisStepIndex_SubstepIndex_Current = 0;

    int _enemiesInWavesCounter = 0;
    List<StepTEST> _allStepsDataForThisWave;




    StepTEST _thisWaveStepOperating;
    EnemySpawnData _thisSubstep;
    bool _waitingForWaveToChange = false;





    // Events config
    [HideInInspector] public UnityEvent<int> OnEnemyDiedTriggered = new UnityEvent<int>();










    [Inject] EnemyConfigContainer _enemyConfigContainer;











    // General initialization
    public void Initialize()
    {
        _isPaused = false;
        _isTimerNeedToWork = false;
    }








    public void SetBatchForEnemiesSpawner(EnemyWaveConfig currentWave)
    {

        // [0] Reset all data
        ResetWaveData();

        CustomLogger.LogEnemySpawner("#############################");
        CustomLogger.LogEnemySpawner("Start enemy-wave data forming");



        // Loop trough all wave steps
        var steps = currentWave.WaveConfig.StepsList;
        foreach (var configStepData in steps)
        {
            

            // Create step info
            StepTEST thisStepData = new StepTEST();
            thisStepData.SubStepsData = new List<EnemySpawnData>();
            thisStepData.TimeToWaitForNextWave = configStepData.TimeToNextStep;


            

            // Try to fill substeps for wave-step
            if (configStepData.EnemyStepSpawnType == EnemyStepSpawnType.Units)
            {
                float thisSubStepSpawnTime = 0;
                float thisSubstepBetweenUnitsStep = 0.1f;

                // Fill amounts of enemies from batch
                for (int i = 0; i < configStepData.AmountToSpawn; i++)
                {

                    // Create enemy data
                    EnemySpawnData enemyToSpawnData = new EnemySpawnData();
                    enemyToSpawnData.Type = configStepData.EnemyToSpawn;
                    enemyToSpawnData.SpawnType = configStepData.RespawnPosition;
                    enemyToSpawnData.TimeWhenToSpawn = thisSubStepSpawnTime;

                    // Add to list
                    thisStepData.SubStepsData.Add(enemyToSpawnData);

                    CustomLogger.LogEnemySpawner("Add wave-SUB-step. spawn-time: " + thisSubStepSpawnTime);

                    _enemiesInWavesCounter += 1;

                    // Increase step between enemies from one batch
                    thisSubStepSpawnTime += thisSubstepBetweenUnitsStep;
                }
            }
                
            


            thisStepData.SubstepMaxIndex = thisStepData.SubStepsData.Count - 1;
            CustomLogger.LogEnemySpawner("Add wave-step");
            CustomLogger.LogEnemySpawner("-------------------");


            // Add wave step to collection
            _allStepsDataForThisWave.Add(thisStepData);
        }



        _thisStepIndex_Current = 0;
        _thisStepIndex_Max = _allStepsDataForThisWave.Count - 1;



        CustomLogger.LogEnemySpawner("Enemy-wave data forming finished");
        CustomLogger.LogEnemySpawner("#############################");
        CustomLogger.LogEnemySpawner("Steps total (index): " + _thisStepIndex_Max);
        CustomLogger.LogEnemySpawner("Enemies total: " + _enemiesInWavesCounter);
        CustomLogger.LogEnemySpawner("#############################");
    }

    void ResetWaveData()
    {
        _isTimerNeedToWork = false;

        _wavePlayTime = 0;
        _thisStepTimePassed = 0;
        _thisStepTimePassed_ForNextWaveChange = 0;
        _enemiesInWavesCounter = 0;

        _thisStepIndex_Max = 0;
        _thisStepIndex_Current = 0;
        _thisStepIndex_SubstepIndex_Max = 0;
        _thisStepIndex_SubstepIndex_Current = 0;

        _allStepsDataForThisWave = new List<StepTEST>();
    }

    public int CountEnemiesForWaves()
    {
        return _enemiesInWavesCounter;
    }









































    // Timer controlling
    float _beforeWaveSleepDuration = 3;
    float _beforeWaveTimer = 0;
    public void StartEnemyWaveSpawnLogic()
    {

        // Timers rest
        _wavePlayTime = 0;
        _thisStepTimePassed = 0;
        _thisStepTimePassed_ForNextWaveChange = 0;


        // Indexes reset
        _thisStepIndex_Current = 0;
        _thisStepIndex_SubstepIndex_Current = 0;


        // Set current wave-step data
        InitializeCurrentWaveStepData();


        // Unblock timer
        _isTimerNeedToWork = true;
        _waitingForWaveToChange = false;
    }

    void InitializeCurrentWaveStepData()
    {

        // [0] Check if this step-index if inside bounds
        if (_thisStepIndex_Current > _thisStepIndex_Max || _thisStepIndex_Current < 0)
        {
            CustomLogger.LogError("STEPS OUT OF BOUNDS");
            return;
        }



        // Set reference to current wave-step
        _thisWaveStepOperating = _allStepsDataForThisWave[_thisStepIndex_Current];

        // Count wave-SUB-steps
        _thisStepIndex_SubstepIndex_Current = 0;
        _thisStepIndex_SubstepIndex_Max = _thisWaveStepOperating.SubstepMaxIndex;
        
        // Set reference to current sub-step
        _thisSubstep = _thisWaveStepOperating.SubStepsData[_thisStepIndex_SubstepIndex_Current];





        CustomLogger.LogEnemySpawner("[RUN] ------------");
        CustomLogger.LogEnemySpawner("[RUN] Initialize current wave-step");
        CustomLogger.LogEnemySpawner("[RUN] Current sub-step index: " + _thisStepIndex_SubstepIndex_Current);
        CustomLogger.LogEnemySpawner("[RUN] Total sub-step index: " + _thisStepIndex_SubstepIndex_Max);
        CustomLogger.LogEnemySpawner("[RUN] ------------");
    }

    void TryToMoveToNextSubStep()
    {


        CustomLogger.LogEnemySpawner("[RUN] Try to move to next wave-SUB-step");



        // [0] Increase SUB-step index -> because we spawned enemy (enemy is sub-step)
        _thisStepIndex_SubstepIndex_Current++;



        // [1] If we have sub-steps -> go
        if (_thisStepIndex_SubstepIndex_Current <= _thisStepIndex_SubstepIndex_Max)
        {
            CustomLogger.LogEnemySpawner("[RUN] Move to next substep(index): " + _thisStepIndex_SubstepIndex_Current);
            _thisSubstep = _thisWaveStepOperating.SubStepsData[_thisStepIndex_SubstepIndex_Current];
            return;
        }


        // [2] If no more sub-steps -> launch timer for wave-step change
        CustomLogger.LogEnemySpawner("[RUN] No more substeps wait for time to change wave (s): " + _thisWaveStepOperating.TimeToWaitForNextWave);
        _waitingForWaveToChange = true;
        _thisSubstep = null;
        return;
    }

    void TryToMoveToNextStep()
    {

        CustomLogger.LogEnemySpawner("[RUN] Try to move to next wave-step");


        // [0] Increase wave-step index
        _thisStepIndex_Current++;



        // [1] If we have next wave-step -> init
        if (_thisStepIndex_Current <= _thisStepIndex_Max)
        {

            CustomLogger.LogEnemySpawner("[RUN] Move to next wave-step: " + _thisStepIndex_Current);

            _waitingForWaveToChange = false;
            InitializeCurrentWaveStepData();
            _thisStepTimePassed = 0;
            _thisStepTimePassed_ForNextWaveChange = 0;
            return;
        }



        // [2] If no more wave-steps stop and wait for wave to be finished
        _isTimerNeedToWork = false;
        _thisSubstep = null;
        _thisWaveStepOperating = null;
        CustomLogger.LogEnemySpawner("[RUN] No more wave-steps. Stop working...");
        CustomLogger.LogEnemySpawner("[RUN] +++++++++++++++++++++++++++++++++++");
    }

    public void Update() => RunEnemiesSpawner();

    void RunEnemiesSpawner()
    {
        // Skip if timer is blocked
        if (_isPaused)
            return;

        // Skip if no need for logic
        if (!_isTimerNeedToWork)
            return;

        // Skip if before-wave timer is not elapsed
        if (!IsBeforeWaveTimerElapsed())
            return;








        // Enemy spawn logic
        _wavePlayTime += Time.deltaTime; // ONLY FOR ANALYTICS
        _thisStepTimePassed += Time.deltaTime;



        if (_thisSubstep != null)
        {
            if (_thisStepTimePassed >= _thisSubstep.TimeWhenToSpawn)
            {
                CustomLogger.LogEnemySpawner("[RUN] +enemy");
                TryToSpawnEnemy(_thisSubstep);
                TryToMoveToNextSubStep();
            }
        }
        

        if (_waitingForWaveToChange)
        {
            _thisStepTimePassed_ForNextWaveChange += Time.deltaTime;
            if (_thisStepTimePassed_ForNextWaveChange >= _thisWaveStepOperating.TimeToWaitForNextWave)
            {
                CustomLogger.LogEnemySpawner("[RUN] Wave-step time elapsed.");
                TryToMoveToNextStep();
            }
        }
    }
    


















    void TryToSpawnEnemy(EnemySpawnData enemyToSpawn)
    {

        // [0] Check if enemy config is correct
        var targetEnemyConfig = _enemyConfigContainer.TryToGetEnemyConfig(enemyToSpawn.Type);
        if (targetEnemyConfig == null)
        {
            CustomLogger.LogError("Unknown enemy config passed");
            return;
        }


        // [1] Specify enemy move data
        var enemySpawner = GetSpawnBySpawnType(enemyToSpawn.SpawnType);
        Transform spawnPos = enemySpawner.transform;
        var targetSpawnPoint = spawnPos;



        // [2] Create enemy-object
        var spawnedEnemy = Instantiate(targetEnemyConfig.EnemyPrefab, targetSpawnPoint.transform.position, targetSpawnPoint.transform.rotation);
        spawnedEnemy.transform.SetParent(_allEnemiesParent);
        var enemyComponent = spawnedEnemy.gameObject.GetComponent<BasicEnemy>();
        if (enemyComponent == null)
        {
            CustomLogger.LogError("Enemy component is missing");
            return;
        }





        enemyComponent.Initialize(
            targetEnemyConfig.EnemyStats,
            AddXPAfterEnemyDeath);
    }

    void AddXPAfterEnemyDeath(int xpAmount)
    {
        OnEnemyDiedTriggered.Invoke(xpAmount);
    }


















































    // Pase-resume logic
    public void PauseWavesTimer() => _isPaused = true;
    public void ResumeWavesTimer() => _isPaused = false;








    // Helper methods
    bool IsBeforeWaveTimerElapsed()
    {
        // Add custom delay before wave
        if (_beforeWaveTimer < _beforeWaveSleepDuration)
        {
            _beforeWaveTimer += Time.deltaTime;
            if (_beforeWaveTimer >= _beforeWaveSleepDuration)
            {
                _beforeWaveTimer = _beforeWaveSleepDuration;
            }
            return false;
        }

        return true;
    }

    EnemySpawner GetSpawnBySpawnType(EnemySpawenPoint spawnPoint)
    {
        foreach (var point in _possibleSpawnPoints)
        {
            if (point.SpawnPointType == spawnPoint)
            {
                return point.EnemySpawner;
            }
        }

        return null;
    }
}








public class StepTEST
{
    public List<EnemySpawnData> SubStepsData;
    public float TimeToWaitForNextWave;

    public int SubstepMaxIndex;
}

public class EnemySpawnData
{
    public float TimeWhenToSpawn;
    public EnemyType Type;
    public EnemySpawenPoint SpawnType;
}



[System.Serializable]
public enum EnemySpawenPoint
{
    Random = 0,
    Position1 = 1,
    Position2 = 2,
    Position3 = 3,
}



[System.Serializable]
public class EnemySpawnPoint
{
    public EnemySpawenPoint SpawnPointType;
    public EnemySpawner EnemySpawner;
}