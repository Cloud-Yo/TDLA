using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _spd = 10f;
    [SerializeField] private GameObject _hitFX = null;
    private Vector3 _dir;
    private Vector3 _startPos;
    private bool _playerBullet;

    private void Start()
    {
        _startPos = new Vector3(transform.position.x, 0, 0);
        if(this.transform.CompareTag("PlayerBullet"))
        {
            _dir = Vector3.up;
            _playerBullet = true;
            this.gameObject.layer = 0;
        }
        else if(this.transform.CompareTag("EnemyBullet"))
        {
            _dir = Vector3.down;
            _playerBullet = false;
        }
    }

    void Update()
    {
        transform.Translate(_dir * _spd * Time.deltaTime);
        
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1.5f);
    }

    public void ShowHitFX()
    {
        if (_hitFX != null)
        {
            Instantiate(_hitFX, transform.position, Quaternion.identity);
        }
    }

}
