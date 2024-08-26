using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;






public class GameCameraController : MonoBehaviour
{
    [SerializeField] float _sensitivity = 1;
    [SerializeField] float _moveToTargetSpeed = 75f;
    GameInputHandler _gameInputHandler;


    float _horizontal;
    float _vertical;



    public void Initialize(GameInputHandler gameInputHandler)
    {
        _gameInputHandler = gameInputHandler;


        _horizontal = transform.position.x;
        _vertical = transform.position.z;


        _gameInputHandler.OnCameraMoveRecieved.AddListener(DetectTouchDelta);
    }

    void DetectTouchDelta(Vector2 delta)
    {
        _moveTween?.Kill();

        float dt = Time.deltaTime;

        _horizontal = transform.position.x;
        _vertical = transform.position.z;

        _horizontal -= delta.x * dt * _sensitivity;
        _vertical -= delta.y * dt * _sensitivity;

        transform.position = new Vector3(_horizontal, 0, _vertical);
    }







    Tween _moveTween;
    public void MoveCameraToPosition(Vector3 targetPosition)
    {

        Vector3 finalPos = new Vector3(targetPosition.x, 0, targetPosition.z);


        float distance = Vector3.Distance(transform.position, finalPos);
        float duration = distance / _moveToTargetSpeed;

        _moveTween?.Kill();
        _moveTween = transform.DOMove(finalPos, duration).SetEase(Ease.OutExpo);
    }
}