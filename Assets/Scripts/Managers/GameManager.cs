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
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _gmSpeed = 0.5f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
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

    public void StartMoving()
    {
        StartCoroutine(GetMoving());
    }

    public void BeginGame()
    {
        _gameStarted = true;
        OnGameStarted?.Invoke();
    }

    IEnumerator GetMoving()
    {
        while (_gmSpeed < _speed)
        {
            _gmSpeed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
