using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _baseEnemyPrefab = null;

    [Header("Variables")]
    [SerializeField] private float _enemySpawnIntervalMin = 2f;
    [SerializeField] private float _enemySpawnIntervalMax = 5f;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(_enemySpawnIntervalMin, _enemySpawnIntervalMax));
            Instantiate(_baseEnemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
