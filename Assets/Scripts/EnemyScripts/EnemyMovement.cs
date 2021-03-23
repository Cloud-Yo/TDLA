using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    private Action OnMoving;
    [SerializeField] private float _spd = 2f;
    [SerializeField] private Rigidbody _myRB = null;

    void Start()
    {
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
