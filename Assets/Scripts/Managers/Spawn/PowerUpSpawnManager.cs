using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _powerUp = null;
    [SerializeField] private WaitForSeconds _powerUpDelay = new WaitForSeconds(7.5f);
    [SerializeField] private Transform _container = null;
    [SerializeField] private bool _gameOver = false;
    [SerializeField] private SpawnData[] _spawnOptions;


    private void OnEnable()
    {
        GameManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= GameOver;
    }


    void Start()
    {
        StartCoroutine(SpawnPowerUpRoutine());    
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (!_gameOver)
        {
            yield return _powerUpDelay;
            Vector2 pos = new Vector2(Random.Range(-6f, 6f), 5f);
            GameObject p = Instantiate(_powerUp, pos, Quaternion.identity);
            int rand = WeightedSpawnUtility.ReturnRandomChoice(_spawnOptions);
            int choice = Random.Range(0, _spawnOptions[rand].Items.Length);
            p.GetComponent<PowerUp>().SetType(_spawnOptions[rand].Items[choice]);
            p.transform.SetParent(_container);
        }
        
    }

    private void GameOver()
    {
        GameManager.OnGameOver -= GameOver;
        _gameOver = true;
    }
}
