using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    private AudioSource _myAS = null;
    private LineRenderer _laserLineR = null;
    [SerializeField] private float _fireRate = 3f;
    [SerializeField] private float _spd = 2f;
    [SerializeField] private float _laserRange = 8f;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private AudioClip _laserFireClip = null;
    [SerializeField] private AudioClip _laserHitClip = null;
    [SerializeField] private GameObject _laserHitPS = null;

    private ParticleSystem _laserPS = null;
    private float _canFire = -1;
    private RaycastHit2D _laserHit; 
    private bool _shotReady => (Time.time > _canFire) && !_fireLaser;
    private bool _fireLaser = false;

    
  
    void Start()
    {
        _laserPS = GetComponent<ParticleSystem>();
        _myAS = transform.parent.GetComponent<AudioSource>();
        _laserLineR = GetComponent<LineRenderer>();
        _laserLineR.enabled = false;
    }


    void Update()
    {
        if (_shotReady)
        {
            ShootLaser();
        }
        _laserLineR.SetPosition(0, this.transform.position);

    }

    private void ShootLaser()
    {
        _laserHit = Physics2D.Raycast(this.transform.position, Vector2.down, _laserRange, _playerLayerMask);
        Vector2 _laserHitPoint;

        if (_laserHit.collider != null)
        {
            _laserLineR.enabled = true;
            _fireLaser = true;
            _laserHitPoint = _laserHit.point;
            if (!_laserPS.isEmitting)
            {
                _laserPS.Emit(3);
            }
            StartCoroutine(LaserShotRoutine(_laserHitPoint));

        }

    }

    private IEnumerator LaserShotRoutine(Vector2 hitPoint)
    {
        _laserLineR.SetPosition(1, _laserLineR.GetPosition(0));
        _myAS.PlayOneShot(_laserFireClip, 0.5f);
        while ((Vector2)_laserLineR.GetPosition(1) != hitPoint)
        {
            _laserLineR.SetPosition(1, Vector3.MoveTowards(_laserLineR.GetPosition(1), hitPoint, 8 * _spd * Time.deltaTime));
            yield return null;
        }
        _laserHit = Physics2D.Raycast(this.transform.position, Vector2.down, _laserRange, _playerLayerMask);
        if (_laserHit.collider != null)
        {

            Debug.Log("Player hit by laser");
            Instantiate(_laserHitPS, _laserHit.point, Quaternion.identity);
            _myAS.PlayOneShot(_laserHitClip, 0.5f);
            _laserHit.collider.GetComponent<PlayerCore>().TakeDamage();
            _fireLaser = false;
            _canFire = Time.time + _fireRate;
            _laserLineR.enabled = false;
        }
        else
        {
            Debug.Log("Player missed!");
            _fireLaser = false;
            _canFire = Time.time + _fireRate;
            _laserLineR.enabled = false;
        }
    }

}
