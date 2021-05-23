using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    private UIManager _myUIM = null;
    private GameManager _myGM = null;
    [SerializeField] private GameObject _explosionFX = null;
    [SerializeField] private int _points = 35;
    private Rigidbody2D _myRB2D = null;
    private ScoreManager _mySM = null;
    [SerializeField] private bool _mimic = false;

    private void OnEnable()
    {
        _myRB2D = GetComponent<Rigidbody2D>();
        _mySM = FindObjectOfType<ScoreManager>();
        _myGM = FindObjectOfType<GameManager>();
        _myUIM = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

         if(other.CompareTag("PlayerBullet"))
         {
            Destroy(_myRB2D);
            Destroy(other.gameObject);
            Instantiate(_explosionFX, transform.position, Quaternion.identity);
            _mySM.UpdateScore(_points);
            Destroy(this.gameObject);
         }
         else if(other.CompareTag("Player"))
         {
            Destroy(_myRB2D);
            other.GetComponent<PlayerCore>()?.TakeDamage();
            Instantiate(_explosionFX, transform.position, Quaternion.identity);
            _mySM.UpdateScore(_points);
            Destroy(this.gameObject);

         }

    }

    private void OnDestroy()
    {
        if (!_mimic)
        {
            WaveManager.EnemyCount--;
        }
        _myUIM.SetInfoText(true);
    }
}
