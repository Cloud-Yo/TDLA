using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _spd = 10f;
    [SerializeField] private GameObject _hitFX = null;
    private Vector3 _dir;

    private void Start()
    {
        if(this.transform.CompareTag("PlayerBullet"))
        {
            _dir = Vector3.up;

            this.gameObject.layer = 0;
        }
        else if(this.transform.CompareTag("EnemyBullet"))
        {
            _dir = Vector3.down;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (this.transform.tag)
        {
            case "PlayerBullet":
                if(other.transform.CompareTag("Enemy"))
                {
                    EnemyCore ec = other.GetComponent<EnemyCore>();
                    if (ec != null)
                    {
                        ec.TakeDamage();
                    }
                    ShowHitFX();
                    Destroy(this.gameObject);
                }
            break;
            case "EnemyBullet":
                if (other.transform.CompareTag("Player"))
                {
                    PlayerCore pc = other.GetComponent<PlayerCore>();
                    if (pc != null)
                    {
                        pc.TakeDamage();
                    }
                    ShowHitFX();
                    Destroy(this.gameObject);
                }
                else if (other.transform.CompareTag("PowerUp"))
                {
                    Destroy(other.gameObject);
                    ShowHitFX();
                    Destroy(this.gameObject);
                }
            break;
            default:
                
                break;
        }
    }

}
