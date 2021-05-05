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
    [SerializeField] private PlayerShields _myPSH = null;

    [Header("Player Variables")]
    private int _lives = 3;
    [SerializeField] private int _maxLives = 3;
    private bool _shield = false;

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
 
    }

    public void TakeDamage()
    {
        
        if(_myPSH.AreShieldsActive())
        {
            _myPSH.DamageShield(1);
            return;
        }
        else
        {
            _lives--;
            DisplayDamage();

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

    void DisplayDamage()
    {
        switch (_lives)
        {
            case 2:
                if (!_damageFX[0].activeInHierarchy)
                {
                    _damageFX[0].SetActive(true);
                }
                else 
                {
                    _damageFX[1].SetActive(false);
                }

                break;
            case 1:
                if (!_damageFX[1].activeInHierarchy)
                {
                    _damageFX[1].SetActive(true);
                }
                break;
            case 3:
                if (_damageFX[0].activeInHierarchy)
                {
                    _damageFX[0].SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    public void RestoreHealth()
    {
        _lives++;
        if (_lives > _maxLives)
        {
            _lives = _maxLives;
        }
        DisplayDamage();
        _myHUIM.RestoreLifeLights(_lives-1);
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
