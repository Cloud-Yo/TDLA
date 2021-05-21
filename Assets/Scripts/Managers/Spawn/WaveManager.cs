using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{

    private SpawnWaveBehavior _mySWB = null;
    [SerializeField] private int _waveID = 0;
    [SerializeField] private UnityEvent _onWaveInterval;
    private void OnEnable()
    {
        GameManager.OnGameStarted += StartWaves;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= StartWaves;
    }
    void Start()
    {
        _mySWB = GetComponent<SpawnWaveBehavior>();    
    }

    public void StartWaves()
    {

        GameManager.OnGameStarted -= StartWaves;
        _mySWB.StartSpawningEnemyWaves(_waveID, NextWave);
    }

    private void NextWave()
    {
        //start wave delay and restart coroutine
        //_waveID++;
        //_onWaveInterval?.Invoke();
        
        StartCoroutine(WaveIntervalRoutine());

    }

    IEnumerator WaveIntervalRoutine()
    {
        _waveID++;
        //display UI message
        yield return new WaitForSeconds(5f);
        _mySWB.StartSpawningEnemyWaves(_waveID, NextWave);

    }
}
