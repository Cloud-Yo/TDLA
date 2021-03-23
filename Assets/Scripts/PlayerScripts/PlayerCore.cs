using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement _myPMove = null;
    [SerializeField] private PlayerShooting _myPShoot = null;
    [SerializeField] private Rigidbody _myRB = null;
    [SerializeField] private Collider _myColl = null;

    [Header("Player Variables")]
    [SerializeField] private int _lives = 3;
    

    void Start()
    {
        transform.position = new Vector3(0,0,transform.position.z);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyLaser"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        _lives--;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

}
