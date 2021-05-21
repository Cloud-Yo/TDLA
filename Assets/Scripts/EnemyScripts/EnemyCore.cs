using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    private UIManager _myUIM = null;
    private GameManager _myGM = null;
    [SerializeField] private GameObject _explosionFX = null;
    [SerializeField] private int _points = 35;
    private ScoreManager _mySM = null;

    private void OnEnable()
    {

        _mySM = FindObjectOfType<ScoreManager>();
        _myGM = FindObjectOfType<GameManager>();
        _myUIM = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

         if(other.CompareTag("PlayerBullet"))
         {
            Destroy(other.gameObject);
            Instantiate(_explosionFX, transform.position, Quaternion.identity);
            _mySM.UpdateScore(_points);
            _myUIM.SetInfoText(true);
            SpawnWaveBehavior.EnemyCount -= 1;
            Destroy(this.gameObject);
         }
         else if(other.CompareTag("Player"))
         {
             
            other.GetComponent<PlayerCore>()?.TakeDamage();
            GameObject splat = Instantiate(_explosionFX, transform.position, Quaternion.identity);
            //splat.transform.SetParent(this.transform.parent);
            _mySM.UpdateScore(_points);
            _myUIM.SetInfoText(true);
            SpawnWaveBehavior.EnemyCount -= 1;
            Destroy(this.gameObject);

         }

    }
}
