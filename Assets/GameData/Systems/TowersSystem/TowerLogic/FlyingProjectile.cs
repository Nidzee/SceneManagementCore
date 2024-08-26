using UnityEngine.Events;
using UnityEngine;




public class FlyingProjectile : MonoBehaviour
{
    BasicEnemy _thisTarget;
    float _timeFlySpend;
    float _flyDuration;
    bool _isFlying;
    UnityAction _reachCallback;
    UnityAction _dieCallback;
    Vector3 _startPoint;







    public void LaunchProjectileToTarget(float duration, BasicEnemy enemy, UnityAction reachTargetAction, UnityAction dieCallback)
    {
        _flyDuration = duration;
        _thisTarget = enemy;
        _reachCallback = reachTargetAction;
        _dieCallback = dieCallback;
        _timeFlySpend = 0;

        _startPoint = transform.position;


        PauseController.PauseControllerRef.OnPauseEmited.AddListener(Pause);
        PauseController.PauseControllerRef.OnResumeEmited.AddListener(Resume);


        _isFlying = true;
    }






    public void Update() => MoveArrowToTarget();

    void MoveArrowToTarget()
    {
        if (!_isFlying)
            return;


        if (_thisTarget == null)
        {
            _dieCallback.Invoke();
            Destroy(gameObject);
            return;
        }


        // Increase fly time spent
        _timeFlySpend += Time.deltaTime;
        float percentage = _timeFlySpend / _flyDuration;
        percentage = Mathf.Clamp01(percentage);
        transform.position = Vector3.Lerp(_startPoint, _thisTarget.ArrowDestinationPoint.transform.position, percentage);


        if (percentage >= 1)
        {
            _isFlying = false;
            _reachCallback?.Invoke();
            Destroy(gameObject);
        }
    }






    public void Pause() => _isFlying = false;
    public void Resume() => _isFlying = true;
}