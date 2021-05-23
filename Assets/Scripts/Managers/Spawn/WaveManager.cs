using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public struct WaveData
{
    public string WaveName;
    public int EnemyCount;
    public float EnemySpawnDelay;
    public GameObject[] Enemies;
    public float[] Weights;
}
public class WaveManager : MonoBehaviour
{

    [SerializeField] private int _waveID = 0;
    [Header("Waves")]
    [SerializeField] private WaveData[] _waveData;
    [Space]
    [SerializeField] private UnityEvent _onWaveInterval;
    private WaveSpawner _myWS = null;
    private static int _enemyCount;

    public static int EnemyCount
    {
        get { return _enemyCount; }
        set { _enemyCount = value; if (_enemyCount < 0){ _enemyCount = 0;}}
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += StartWaves;
        _myWS = GetComponent<WaveSpawner>();
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= StartWaves;
    }

    public void StartWaves()
    {
        GameManager.OnGameStarted -= StartWaves;
        _myWS.StartNewWave(_waveData[_waveID], NextWave);
    }

    private void NextWave()
    {
        //start wave delay and restart coroutine
        //_waveID++;
        //_onWaveInterval?.Invoke();
        if (!GameManager.Instance.GameIsOver)
        {
            if(EnemyCount > 0)
            {
                StartCoroutine(WaitForWaveComplete());
            }
            else
            {
                _waveID++;
                if (_waveID < _waveData.Length)
                {
                    StartCoroutine(WaveIntervalRoutine());
                }
            }

        }
    }

    IEnumerator WaveIntervalRoutine()
    {
        
        //display UI message
        yield return new WaitForSeconds(5f);
        _myWS.StartNewWave(_waveData[_waveID], NextWave);

    }

    IEnumerator WaitForWaveComplete()
    {
        while (!GameManager.Instance.GameIsOver && EnemyCount > 0)
        {
            yield return null;
        }

        NextWave();
    }
}
