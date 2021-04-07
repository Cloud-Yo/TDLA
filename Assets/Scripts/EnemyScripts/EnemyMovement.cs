using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    private Action OnMoving;
    [SerializeField] private float _spd = 2f;
    [SerializeField] private float _spdMult = 2f;
    [SerializeField] private Rigidbody2D _myRB = null;
    [SerializeField] private GameManager _gm = null;
    [SerializeField] private EnemyCore _myEC = null;

    private void OnEnable()
    {
        _gm = FindObjectOfType<GameManager>();
        GameManager.OnSetGlobalSpeed += SetEnemySpeed;
    }
    private void OnDisable()
    {
        GameManager.OnSetGlobalSpeed -= SetEnemySpeed;
    }

    private void SetEnemySpeed(float s)
    {
        _spd = s * _spdMult;
    }

    void Start()
    {
       _spd = _gm.GetWorldSpeed() * _spdMult;
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
        transform.position = new Vector2(UnityEngine.Random.Range(-5.5f, 5.5f), 8f);

    }

    void BasicEnemyMovement()
    {
        transform.Translate(Vector2.down * _spd * Time.deltaTime);
    }
}
