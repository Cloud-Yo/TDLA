using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement _myPMove = null;
    [SerializeField] private PlayerShooting _myPShoot = null;
    [SerializeField] private Rigidbody2D _myRB = null;
    [SerializeField] private Collider _myColl = null;
    [SerializeField] private PlayerAnimations _myAN = null;

    [Header("Player Variables")]
    [SerializeField] private int _lives = 3;

    [Header("Manager Components")]
    [SerializeField] private SpawnManager _mySM = null;
    

    void Start()
    {
        transform.position = new Vector3(0,0,transform.position.z);    
    }

    public void TakeDamage()
    {
        _lives--;

        if (_lives < 1)
        {
            _mySM.GameOver();
            Destroy(this.gameObject);
        }
    }

    public void StartTrippleShot()
    {
        _myAN.ActivateSideCannons(true);
        _myPShoot.SetTripleShot();
        //StartCoroutine(TSCooldown());
    }

    IEnumerator TSCooldown()
    {
        yield return new WaitForSeconds(5f);
        _myAN.ActivateSideCannons(false);
        _myPShoot.SetNormalShot();
    }
}
