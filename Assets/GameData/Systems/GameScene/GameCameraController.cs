using System.Collections.Generic;
using System.Collections;
using UnityEngine;






public class GameCameraController : MonoBehaviour
{
    [SerializeField] float _sensitivity = 1;
    GameInputHandler _gameInputHandler;


    float _horizontal;
    float _vertical;



    public void Initialize(GameInputHandler gameInputHandler)
    {
        _gameInputHandler = gameInputHandler;


        _horizontal = 0;
        _vertical = 0;


        _gameInputHandler.OnCameraMoveRecieved.AddListener(DetectTouchDelta);
    }

    void DetectTouchDelta(Vector2 delta)
    {
        float dt = Time.deltaTime;

        _horizontal -= delta.x * dt * _sensitivity;
        _vertical -= delta.y * dt * _sensitivity;

        transform.position = new Vector3(_horizontal, 0, _vertical);
    }
}