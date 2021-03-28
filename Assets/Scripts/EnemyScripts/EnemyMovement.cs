using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    private Action OnMoving;
    [SerializeField] private float _spd = 2f;
    [SerializeField] private Rigidbody _myRB = null;
    [SerializeField] private GameManager _gm = null;

    private void OnEnable()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.OnSetGlobalSpeed += SetEnemySpeed;
    }
    private void OnDisable()
    {
        GameManager.OnSetGlobalSpeed -= SetEnemySpeed;
    }

    private void SetEnemySpeed(float s)
    {
        _spd = s * 8f;
    }

    void Start()
    {
       _spd = _gm.GetWorldSpeed() * 8f;
       OnMoving = BasicEnemyMovement;
       ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        OnMoving?.Invoke();
    }

    private void LateUpdate()
    {
        if(transform.position.y < -7.5f)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-9f, 9f), 8f, 0f);

    }

    void BasicEnemyMovement()
    {
        transform.Translate(Vector3.down * _spd * Time.deltaTime);
    }
}
