using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    #region Movement Variables
    public delegate void MoveDelegate(Vector2 mov);
    public static MoveDelegate MyMoveDelegate;
    public static Action<float> OnAcceleration;
    private bool _canAccelerate = true;
    [SerializeField] private float _maxAccelerationTime = 3f;
    [SerializeField] private float _accelerationTimeLeft;
    private Vector2 _mov;
    [SerializeField] private static bool _accelerate = false;
    [SerializeField] private float _spd = 3f;
    [SerializeField] private float _accelSpd = 1.5f;
    [SerializeField] private Animator _myAN = null;
    [SerializeField] private AudioSource _motorAS = null;
    #endregion

    
    void Start()
    {
        _accelerationTimeLeft = _maxAccelerationTime;
       MyMoveDelegate = StandardMovement;
    }

    private void Update()
    {
        MovementInput();
        SetMoveMode();
        HandleAccelerationTime();
    }

    private void LateUpdate()
    {
        RestrictMovement();
    }

    private void SetMoveMode()
    {
        if (Accelerate() && MyMoveDelegate != TurboMovement)
        {
            MyMoveDelegate = TurboMovement;

        }
        else if (!Accelerate() && MyMoveDelegate != StandardMovement)
        {
            MyMoveDelegate = StandardMovement;

        }
    }

   
    private void MovementInput()
    {
        _mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _myAN.SetFloat("TurnFloat", _mov.x);
        MyMoveDelegate?.Invoke(_mov);
    }
    private void RestrictMovement()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.75f, 5.75f), Mathf.Clamp(transform.position.y, -3.65f, 0.5f));
       
    }

    private bool Accelerate()
    {
        if (!_canAccelerate)
        {
            return false;
        }
       return Input.GetKey(KeyCode.LeftShift);
    }

    #region MovementTypes
    private void StandardMovement(Vector2 move)
    {
        transform.Translate(move * _spd * Time.deltaTime);
        if (_motorAS.pitch > 1f)
        {
            _motorAS.pitch -= Time.deltaTime;
            
        }
    }

    private void TurboMovement(Vector2 move)
    {
        
        transform.Translate(move * (_spd * _accelSpd) * Time.deltaTime);
        if (_motorAS.pitch < 1.75f)
        {
            _motorAS.pitch += Time.deltaTime;
        }
    }
    #endregion

    private void HandleAccelerationTime()
    {
        if (Accelerate())
        {
            if(_accelerationTimeLeft > 0)
            {
                _accelerationTimeLeft -= Time.deltaTime;
                float fill = _accelerationTimeLeft / _maxAccelerationTime;
                OnAcceleration?.Invoke(fill);
            }
            else
            {
                OnAcceleration?.Invoke(0);
                _canAccelerate = false;
                _accelerationTimeLeft = 0;
            }


        }
        else if(!Accelerate())
        {
            if (_accelerationTimeLeft < _maxAccelerationTime)
            {
                _accelerationTimeLeft += Time.deltaTime;
                float fill = _accelerationTimeLeft / _maxAccelerationTime;
                OnAcceleration?.Invoke(fill);
            }
            else if (!_canAccelerate)
            {

                _accelerationTimeLeft = _maxAccelerationTime;
                _canAccelerate = true;
            }

        }
    }

}
