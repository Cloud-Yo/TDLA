using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement _myPMove = null;
    [SerializeField] private PlayerModularShooting _myPShoot = null;
    [SerializeField] private Rigidbody2D _myRB = null;
    [SerializeField] private Collider _myColl = null;
    [SerializeField] private PlayerAnimations _myAN = null;
    [SerializeField] private GameObject[] _damageFX;

    [Header("Player Variables")]
    [SerializeField] private int _lives = 3;
    [SerializeField] private bool _shield = false;

    [Header("Manager Components")]
    [SerializeField] private EnemySpawnManager _mySM = null;
    [SerializeField] private UIManager _myUIM = null;
    [SerializeField] private GameManager _myGM = null;
    [SerializeField] private HealthUIManager _myHUIM = null;
    

    void Start()
    {

        foreach (GameObject fx in _damageFX)
        {
            fx.SetActive(false);
        }
        //transform.position = new Vector3(0,0,transform.position.z);    
    }

    public void TakeDamage()
    {
        
        if(_shield)
        {
            _shield = false;
            _myAN.ActivateShield(false);
            return;
        }
        else
        {
            _lives--;

            switch (_lives)
            {
                case 2:
                    _damageFX[0].SetActive(true);
                    
                    break;
                case 1:
                    _damageFX[1].SetActive(true);
                    
                    break;
                case 0:
                    
                    break;
                default:
                    break;
            }
            _myHUIM.UpdateLifeLights(_lives);
            if (_lives < 1)
            {
                _mySM.GameOver();
                _myUIM.GameOver();
                _myGM.GameOver();
                Destroy(this.gameObject);
            }
        }
  
       
    }


    public void StartShield()
    {
        _myAN.ActivateShield(true);
        _shield = true;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("UI"))
        {
            _myUIM.SetUIOpacity(true);
            Debug.Log("Entered UI");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("UI"))
        {
            _myUIM.SetUIOpacity(false);
            Debug.Log("Left UI");
        }
    }
}
