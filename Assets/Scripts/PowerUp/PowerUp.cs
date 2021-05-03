using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _puType = null;
    [SerializeField] private Sprite[] _types;
    [SerializeField] float _spd = 2f;
    [SerializeField] float _currentSpd = 2f;
    [SerializeField] private int _type = 0;
    [SerializeField] private GameManager _myGM = null;

    void Start()
    {
        _myGM = FindObjectOfType<GameManager>();
        _puType.sprite = _types[_type];
        SetPowerupSpeed(_myGM.GetWorldSpeed());
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.down * _spd * Time.deltaTime);
        if (_myGM.GetWorldSpeed() != _currentSpd)
        {
            SetPowerupSpeed(_myGM.GetWorldSpeed());
        }
       
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void SetPowerupSpeed(float s)
    {
        _spd =  s* 2f;
        _currentSpd = s;
    }

    public void SetType(int i)
    {
        _puType.sprite = _types[i];
        _type = i;
    }
    public void RandType()
    {
        int i = Random.Range(0, _types.Length);
        _puType.sprite = _types[i];
        _type = i;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            switch(_type)
            {
                case 0: //TripleShot PowerUp
                    collision.GetComponent<PlayerModularShooting>()?.SwitchWeapon(1);
                    collision.GetComponent<PlayerAnimations>()?.ActivateSideCannons(true);
                    break;
                case 1://TO DO: Ammo PowerUp
                    //collision.GetComponent<PlayerMovement>()?.StartSpeedBoost();
                    break;
                case 2:
                    collision.GetComponent<PlayerShields>()?.ActivateShields();
                    break;
            }
            
            Destroy(this.gameObject);
        }
    }
}
