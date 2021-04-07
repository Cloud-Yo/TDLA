using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Movement Variables
    public delegate void MoveDelegate(Vector2 mov);
    public static MoveDelegate MyMoveDelegate;
    private Vector2 _mov;
    [SerializeField] private bool _accelerate = false;
    [SerializeField] private float _spd = 3f;
    [SerializeField] private Animator _myAN = null;
    #endregion

    
    void Start()
    {
       MyMoveDelegate = StandardMovement;
    }

    private void Update()
    {
        MovementInput();
        if(Accelerate() && MyMoveDelegate != TurboMovement)
        {
            MyMoveDelegate = TurboMovement;
        }
        else if(MyMoveDelegate != StandardMovement)
        {
            MyMoveDelegate = StandardMovement;
        }
    }
    private void LateUpdate()
    {
        RestrictMovement();
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
        return Input.GetKey(KeyCode.LeftShift) ? true : false;
    }

    #region MovementTypes
    private void StandardMovement(Vector2 move)
    {
        transform.Translate(move * _spd * Time.deltaTime);
    }

    private void TurboMovement(Vector2 move)
    {
        transform.Translate(move * (_spd * 2f) * Time.deltaTime);
    }
    #endregion
}
