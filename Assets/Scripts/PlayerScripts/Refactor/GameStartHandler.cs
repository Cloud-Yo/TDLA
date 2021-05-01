using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartHandler : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _components;

    private void OnEnable()
    {
        GameManager.OnGameStarted += StartGame;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= StartGame;
    }
    void Start()
    {
        foreach (var c in _components)
        {
            c.enabled = false;
        }
    }

    private void StartGame()
    {
        GameManager.OnGameStarted -= StartGame;
        foreach (var c in _components)
        {
            c.enabled = true;
        }

    }



}
