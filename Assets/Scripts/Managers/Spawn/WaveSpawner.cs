using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public delegate void OnWaveComplete();
    private OnWaveComplete _onWaveComplete = null;
    private WaveData _myWD;
    private WaitForSeconds _spawnDelay;
    [SerializeField] private Transform _container = null;
    public void StartNewWave(WaveData wd, OnWaveComplete owc)
    {
        _onWaveComplete = owc;
        _myWD = wd;
        _spawnDelay = new WaitForSeconds(wd.EnemySpawnDelay);
        StartCoroutine(WaveSpawnerRoutine());
    }

    private IEnumerator WaveSpawnerRoutine()
    {

        for (int i = 0; i < _myWD.EnemyCount; i++)
        {

            if (!GameManager.Instance.GameIsOver)
            {
                int rand = WeightedSpawnUtility.ReturnWaveRandomIndex(_myWD);
                yield return _spawnDelay;
                GameObject alien = Instantiate(_myWD.Enemies[rand], transform.position, Quaternion.identity);
                alien.transform.SetParent(_container);
                WaveManager.EnemyCount++;
            }
            else
            {
                StopAllCoroutines();
                yield break;
            }
        }
        _onWaveComplete?.Invoke();
    }
}
