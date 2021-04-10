using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _powerUp = null;
    [SerializeField] private WaitForSeconds _powerUpDelay = new WaitForSeconds(7.5f);
    [SerializeField] private Transform _container = null;
    
    void Start()
    {
        StartCoroutine(SpawnPowerUpRoutine());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return _powerUpDelay;
            Vector2 pos = new Vector2(Random.Range(-6f, 6f), 5f);
            GameObject p = Instantiate(_powerUp, pos, Quaternion.identity);
            p.GetComponent<PowerUp>().RandType();
            p.transform.SetParent(_container);
        }
        
    }
}
