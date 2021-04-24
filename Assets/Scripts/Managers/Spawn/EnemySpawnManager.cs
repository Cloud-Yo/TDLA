using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _baseEnemyPrefab = null;
    [SerializeField] private GameObject _enemyContainer = null;

    [Header("Variables")]
    [SerializeField] private float _enemySpawnIntervalMin = 2f;
    [SerializeField] private float _enemySpawnIntervalMax = 5f;
    [SerializeField] private bool _gameOver = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while(!_gameOver)
        {
            yield return new WaitForSeconds(Random.Range(_enemySpawnIntervalMin, _enemySpawnIntervalMax));
            GameObject e = Instantiate(_baseEnemyPrefab, transform.position, Quaternion.identity);
            e.transform.SetParent(_enemyContainer.transform);
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}
