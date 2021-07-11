using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBackwards : MonoBehaviour
{
    private Transform _player = null;
    private AudioSource _myAS = null;
    [SerializeField] private GameObject _alienBullet = null;
    [SerializeField] private int _bullets = 3;
    
    [Range(-1, 1)]
    [SerializeField] private float _angle = 0.85f;
    [SerializeField] private AudioClip _shootSFX = null;
    private Transform _bulletContainer = null;
    private bool _isShooting = false;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerCore>().transform;
        _myAS = GetComponent<AudioSource>();
        _bulletContainer = GameObject.Find("BulletContainer").transform;
    }

    void Update()
    {
        if(!GameManager.Instance.GameIsOver && PlayerIsBehind() && !_isShooting)
        {
           if(transform.position.y > -3.85f)
            {
                _isShooting = true;
                StartCoroutine(ShootBackAtPlayer(_bullets));
            }

        }

        if(transform.position.y > 3.5f && _isShooting)
        {
            _isShooting = false;
        }
    }

    private IEnumerator ShootBackAtPlayer(int bullets)
    {
        while (bullets > 0)
        {
           
            Vector2 shootAngle;
            if (_player != null)
            {
                shootAngle = _player.transform.position - this.transform.position;
            }
            else
            {
                shootAngle = Vector2.up;
            }

            GameObject bullet = Instantiate(_alienBullet, transform.position, Quaternion.FromToRotation(Vector2.down, shootAngle));
            bullet.transform.SetParent(_bulletContainer);
            _myAS.PlayOneShot(_shootSFX, 0.5f);
            bullets--;
            yield return new WaitForSeconds(0.5f);

        }
    }

    private bool PlayerIsBehind()
    {
        if(_player != null)
        {
            return Vector2.Dot(Vector2.down, ((Vector2)transform.position - (Vector2)_player.position).normalized) > _angle;
        }
        return false;
    }
}
