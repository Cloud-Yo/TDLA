using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    private Action OnMoving;
    [SerializeField] private float _spd = 2f;
    [SerializeField] private float _currentSpd = 2f;
    [SerializeField] private float _spdDelta = 0.5f;
    [SerializeField] private Rigidbody2D _myRB = null;
    [SerializeField] private GameManager _gm = null;
    [SerializeField] private EnemyCore _myEC = null;
    [SerializeField] private UIManager _myUIM = null;

   
    private bool lowPos => transform.position.y < -7.5f;
    

    private void OnEnable()
    {
        _myUIM = FindObjectOfType<UIManager>();
        _gm = FindObjectOfType<GameManager>();
        GameManager.OnSetGlobalSpeed += SetEnemySpeed;
    }
    private void OnDisable()
    {
        GameManager.OnSetGlobalSpeed -= SetEnemySpeed;
    }

    void Start()
    {
       _spd = _gm.GetWorldSpeed() *2;
       OnMoving = BasicEnemyMovement;
       ResetPosition();
    }
    private void SetEnemySpeed(float s)
    {
        if(s >= 0.5f)
        {
            _spd = (s * 2) + (_spdDelta);
        }
        else
        {
            _spd = _spdDelta;
        }
        
        _currentSpd = s;
        _spdDelta = Mathf.Clamp(_spdDelta, 1f, 100f);
    }
    // Update is called once per frame
    void Update()
    {
        OnMoving?.Invoke();
    }
    

    private void LateUpdate()
    {
        if(lowPos)
        {
            _myUIM.SetInfoText(false);
            ResetPosition();
        }
        LimitXPosition();
    }
    private void LimitXPosition()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.5f, 5.5f), transform.position.y);
    }
    private void ResetPosition()
    {
        transform.position = new Vector2(UnityEngine.Random.Range(-5.5f, 5.5f), 8f);

    }

    void BasicEnemyMovement()
    {
        transform.Translate(Vector2.down * _spd * Time.deltaTime);
        if (_gm.GetWorldSpeed() != _currentSpd)
        {
            SetEnemySpeed(_gm.GetWorldSpeed());
        }
    }
}
