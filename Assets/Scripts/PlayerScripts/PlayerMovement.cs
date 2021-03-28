using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Movement Variables
    public delegate void MoveDelegate(Vector2 mov);
    public static MoveDelegate MyMoveDelegate;
    private Vector2 _mov;
    [SerializeField] private float _spd = 5f;
    #endregion

    
    void Start()
    {
       MyMoveDelegate = StandardMovement;
    }

    private void Update()
    {
        MovementInput();
 
    }
    private void LateUpdate()
    {
        RestrictMovement();
    }
   
    private void MovementInput()
    {
        _mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        MyMoveDelegate?.Invoke(_mov);
    }
    private void RestrictMovement()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -12.25f, 12.25f), Mathf.Clamp(transform.position.y, -8f, 1f), 0);
       
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
