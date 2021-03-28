using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static Action<float> OnSetGlobalSpeed;
    [SerializeField] private bool _gameOver = false;
    [SerializeField] private float _gmSpeed = 0.5f;
    void Start()
    {
        _gameOver = false;
        OnSetGlobalSpeed?.Invoke(_gmSpeed);
    }

    public void GameIsOver()
    {
        _gameOver = true;
        Debug.Log("Game is over!");
    }

    public void ChangeGameSpeed(float s)
    {
        _gmSpeed = s;
        OnSetGlobalSpeed?.Invoke(_gmSpeed);
    }

    public float GetWorldSpeed()
    {
        return _gmSpeed;
    }
}
