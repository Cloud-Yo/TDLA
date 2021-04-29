using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _spd = 10f;
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

}
