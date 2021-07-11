using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HomingMissile : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotSpeed = 225f;
    [SerializeField] private Transform _target = null;
    [SerializeField] private GameObject _explosion = null;
    [SerializeField] private ParticleSystem[] _exhaust;
    private bool _lockedOn = false;
    private EnemyCore[] _targets;
    private AudioSource _myAS = null;
    private Rigidbody2D _myRB = null;
    private void OnEnable()
    {
        _myAS = GetComponent<AudioSource>();
        _myRB = GetComponent<Rigidbody2D>();

        foreach (var x in _exhaust)
        {
            x.Stop();
        }
        _myAS.Stop();
    }
    void Start()
    {

        StartCoroutine(ActivateHoming());
       
    }

    void Update()
    {
        if (!_lockedOn && _target == null)
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }
        else if(_lockedOn && _target != null)
        {

            MoveToTarget();
        }
        else if(_lockedOn && _target == null)
        {
            _target = FindTarget();
        }

    }
    
    IEnumerator ActivateHoming()
    {
        yield return new WaitForSeconds(0.25f);
        _target = FindTarget();
        _lockedOn = true;
        foreach (var x in _exhaust)
        {
            x.Play();
        }
        _myAS.Play();
    }

    private Transform FindTarget()
    {
        _targets = FindObjectsOfType<EnemyCore>();
        float[] distances = new float[_targets.Length];
        if (distances.Length == 0)
        {
            StartCoroutine(SelfDestruct());
            return null;
        }
        for (int i = 0; i < _targets.Length; i++)
        {
            distances[i] = Vector2.Distance(this.transform.position, _targets[i].transform.position);
        }

        return _targets[Array.IndexOf(distances, Mathf.Min(distances))].transform;
    }

    IEnumerator SelfDestruct()
    {
        _lockedOn = false;
        yield return new WaitForSeconds(1f);
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void FlyToTarget()
    {
        Vector2 direction =  (_target.transform.position - this.transform.position).normalized;
        float rotDirection = Vector3.Cross(transform.up, direction).z;
        _myRB.angularVelocity = rotDirection * _rotSpeed;
        transform.Translate(Vector2.up * _speed * Time.deltaTime, Space.Self);
    }

    private void MoveToTarget()
    {
        Vector2 direction = (transform.position - _target.position).normalized;
        float angle = Vector2.Dot(direction, transform.right);
        transform.Rotate(new Vector3(0,0,angle *_rotSpeed * Time.deltaTime));
        transform.Translate(Vector2.up * _speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            EnemyCore ec = collision.GetComponent<EnemyCore>();
            if (ec != null)
            {
                ec.TakeDamage();
            }
            Destroy(this.gameObject);
        }
    }
}
