using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBG : MonoBehaviour
{
    [SerializeField] private float _spd;
    [SerializeField] private float _currentSpd;
    [SerializeField] private float _spdMult = 2;

    void Start()
    {
        _currentSpd = GameManager.Instance.GetWorldSpeed();
        _spd = _currentSpd *_spdMult;
    }

    void Update()
    {
        MoveWithBackGround();
        ChangeSpeed();
    }

    private void MoveWithBackGround()
    {
        transform.Translate(Vector2.down * _spd * Time.deltaTime);
    }


    private void ChangeSpeed()
    {
        if(GameManager.Instance.GetWorldSpeed() != _currentSpd)
        {
            _currentSpd = GameManager.Instance.GetWorldSpeed();
            _spd = _currentSpd * _spdMult;
        }
    }
}
