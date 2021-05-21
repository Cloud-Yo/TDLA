using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct WaveData
{
    public GameObject[] Enemies;
    public int WaveID;
    public int EnemyCount;
    public float EnemySpawnDelay;
}
public class SpawnWaveBehavior : MonoBehaviour
{

    public enum WaveState
    {
        Spawning,
        WaitingForNewWave,
        WaveComplete,
        StopSpawning
    }


    [SerializeField] private WaveState _myWS = WaveState.WaitingForNewWave;
    public WaveState MyWS
    {
        get { return _myWS; }
        set { _myWS = value; }
    }

    public delegate void OnNextWave();
    public OnNextWave MyNextWaveDelegate;


    [SerializeField]private WaveData[] _waves;

    [SerializeField]
    private Transform _conatainer = null;

    public WaveData[] Waves
    {
        get { return _waves; }
        private set { _waves = value; }
    }

    private WaitForSeconds _spawnDelay;
    [SerializeField]private bool _gameOver = false;

    private static int _enemyCount = 0;

    public static int EnemyCount
    {
        get { return _enemyCount; }
        set { _enemyCount = value; }
    }

    private bool _readyToSpawn => _myWS == WaveState.WaitingForNewWave || _myWS == WaveState.WaveComplete;
    private bool _startNewWave => EnemyCount == 0 && _myWS == WaveState.WaveComplete;

    private void OnEnable()
    {
        GameManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= GameOver;
    }

    private void GameOver()
    {
        GameManager.OnGameOver -= GameOver;
        _gameOver = true;
        _myWS = WaveState.StopSpawning;
        StopAllCoroutines();
    }
    public void StartSpawningEnemyWaves(int id, OnNextWave onw)
    {
        if (_readyToSpawn && id < Waves.Length)
        {
            StartCoroutine(SpawnWaveX(id, onw));
        }
        //else begin bossfight
    }

    void Update()
    {
        if (_startNewWave)
        {
            MyWS = WaveState.WaitingForNewWave;
            MyNextWaveDelegate?.Invoke();
        }
    }

    IEnumerator SpawnWaveX(int id, OnNextWave onw)
    {
        _myWS = WaveState.Spawning;
        _spawnDelay = new WaitForSeconds(Waves[id].EnemySpawnDelay);
        MyNextWaveDelegate = onw;

            for (int i = 0; i < Waves[id].EnemyCount; i++)
            {
                
                if (!_gameOver)
                {
                    int rand = Random.Range(0, Waves[id].Enemies.Length);
                    yield return _spawnDelay;
                    GameObject alien = Instantiate(Waves[id].Enemies[rand], transform.position, Quaternion.identity);
                    alien.transform.SetParent(_conatainer);
                    EnemyCount++;
                }
                else
                {
                    yield break;
                }
            }
           
        _myWS = _gameOver? _myWS = WaveState.StopSpawning : WaveState.WaveComplete;
       
    }
}
