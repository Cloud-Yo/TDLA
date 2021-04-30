using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action<float> OnSetGlobalSpeed;
    public static Action OnGameOver;
    public static Action OnGameStarted;
    [SerializeField] private bool _gameOver = false;
    [SerializeField] private bool _gameStarted = false;
    [SerializeField] private float _gmSpeed = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _gameOver = false;
        OnSetGlobalSpeed?.Invoke(_gmSpeed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        Debug.Log("Game is over!");
        OnGameOver?.Invoke();
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

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
