using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab = null;
    [SerializeField] private Transform _container = null;
    [SerializeField] private float _fireRate = 2.5f;
    [SerializeField] private float _shotReady = -1f;
    [SerializeField] private float _fireDistance = 5f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private AudioSource _myAS = null;
    [SerializeField] private AudioClip _fireSFX = null;

    private bool _canShoot => Physics2D.Raycast(transform.position, Vector2.down, _fireDistance, _targetLayer) && Time.time > _shotReady;


    private void Update()
    {
        if (_canShoot)
        {
            _shotReady = Time.time + _fireRate;
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.tag = "EnemyBullet";
            bullet.transform.SetParent(_container);
            _myAS.PlayOneShot(_fireSFX, 0.6f);
 
        }
    }
  

}
