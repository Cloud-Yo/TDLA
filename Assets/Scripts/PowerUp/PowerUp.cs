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
    [SerializeField] private MimicBehavior _myMB = null;
    [SerializeField] private GameObject _explosionPrefab = null;
    public int Type
    {
        get { return _type; }
        private set { _type = value; }
    }

    [SerializeField] private GameManager _myGM = null;

    void Start()
    {
        _myGM = FindObjectOfType<GameManager>();
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
        
        _type = i;
        if (i == 5)
        {
            i = Random.Range(0, _types.Length - 1);
            _myMB.enabled = true;
        }
        else
        {
            _myMB.enabled = false;
        }
        _puType.sprite = _types[i];
    }
    public void RandType()
    {
        int i = Random.Range(0, _types.Length);
        _type = i;
        if (i == 5)
        {
            i = Random.Range(0, _types.Length-1);
            _myMB.enabled = true;
        }
        else
        {
            _myMB.enabled = false;
        }
        _puType.sprite = _types[i];
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //FireEvents: 0 = MainAmmo, 1 = TripleShot, 2 = GrapeShot, 3 = Shields, 4 = Health, 5 = Mimic
            switch(_type)
            {

                case 0: //Ammo
                    collision.GetComponent<PlayerModularShooting>()?.ReloadMainAmmo();
                    break;
                case 1://TripleShot PowerUp
                  
                    if(collision.GetComponent<PlayerModularShooting>().WeaponIndex != _type)
                    {
                        collision.GetComponent<UnityEventHandler>()?.FireEvent(0);
                    }
                    collision.GetComponent<PlayerModularShooting>()?.SwitchWeapon(_type);
                    collision.GetComponent<PlayerAnimations>()?.ActivateSideCannons(true);

                    break;
                case 2://GrapeShot PowerUp
                  
                    if(collision.GetComponent<PlayerModularShooting>().WeaponIndex != _type)
                    {
                        collision.GetComponent<UnityEventHandler>()?.FireEvent(0);
                    }
                    collision.GetComponent<PlayerModularShooting>()?.SwitchWeapon(_type);

                    break;
                case 3://shields
                    collision.GetComponent<PlayerShields>()?.ActivateShields();
                    collision.GetComponent<UnityEventHandler>()?.FireEvent(0);
                    break;
                case 4://Health
                    collision.GetComponent<PlayerCore>()?.RestoreHealth();
                    collision.GetComponent<UnityEventHandler>()?.FireEvent(2);
                    break;
                case 5://Mimic
                    //instantiate explosion
                    collision.GetComponent<PlayerCore>()?.TakeDamage();
                    Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                    break;
            }
            
            Destroy(this.gameObject);
        }
    }
}
