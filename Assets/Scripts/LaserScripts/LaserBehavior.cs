using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField] private float _spd = 10f;
    private Vector3 _dir;
    private Vector3 _startPos;
    private bool _playerLaser;

    private void Start()
    {
        _startPos = new Vector3(transform.position.x, 0, 0);
        if(this.transform.CompareTag("PlayerLaser"))
        {
            _dir = Vector3.up;
            _playerLaser = true;
        }
        else if(this.transform.CompareTag("EnemyLaser"))
        {
            _dir = Vector3.down;
            _playerLaser = false;
        }
    }

    void Update()
    {
        transform.Translate(_dir * _spd * Time.deltaTime);
        
    }
    private void LateUpdate()
    {
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        if(Vector2.Distance(_startPos, transform.position) > 12f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && _playerLaser)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("Player") && !_playerLaser)
        {
            other.GetComponent<PlayerCore>()?.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
