using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModularShooting : MonoBehaviour
{

    private PlayerAnimations _myAN = null;
    private AudioSource _myAS = null;
    //Weapons: 0 = Main, 1 = TripleShot
    [SerializeField] private WeaponSO[] _weapons;
    [SerializeField] private int _weaponIndex = 0;
   
    public int WeaponIndex { get { return _weaponIndex; } private set { _weaponIndex = value; } }
    [SerializeField] private ParticleSystem[] _shotSmokePS;
    
    [SerializeField] private float _sfxVolume = 0.8f;
    [SerializeField] private Transform _bulletContainer = null;
    [SerializeField] private float _shotReady = -1f;
    [SerializeField] private float _fireRate = 0.5f;
    private WaitForSeconds _shellDelay = new WaitForSeconds(0.15f);
    [SerializeField] private UnityEvent OnShellEject;
    [SerializeField] private AmmoCounter _myAC = null;
    private ActionHandler _myAH = null;
    private SetParticleSystemColor _mySPSC = null;
    private void Start()
    {
        _mySPSC = GetComponent<SetParticleSystemColor>();
        _myAH = GetComponent<ActionHandler>();
        _myAN = GetComponent<PlayerAnimations>();
        foreach (var w in _weapons)
        {
           w.ResetAmmo();
        }
        _weapons[_weaponIndex].UpdateAmmo(_weapons[_weaponIndex].AmmoReload);
        _myAC.UpdateAmmoCounter(_weapons[_weaponIndex].Ammo, _weapons[_weaponIndex].MaxAmmo);
        _myAS = GetComponent<AudioSource>();

    }

    public void FireWeapon()
    {
        if (_weapons[_weaponIndex].Ammo > 0 && CanFireCheck())
        {
            _weapons[_weaponIndex].UpdateAmmo(-1);
            _myAC.UpdateAmmoCounter(_weapons[_weaponIndex].Ammo, _weapons[_weaponIndex].MaxAmmo);
            _shotReady = Time.time + _fireRate;
             GameObject bullet = Instantiate(_weapons[_weaponIndex].WeaponPrefab, transform.position, Quaternion.identity);
            bullet.transform.SetParent(_bulletContainer);
            bullet.transform.tag = "PlayerBullet";
            _myAS.PlayOneShot(_weapons[_weaponIndex].SFX, _sfxVolume);
            _myAN.FireAnimation(_weaponIndex);
            
            //Eject shells if the weapon uses them
            if (_weapons[_weaponIndex].Shells > 0)
            {
                StartCoroutine(EjectShells(_weapons[_weaponIndex].Shells));
            }
            
            //Emit Smoke Particles from barrels
            _shotSmokePS[0].Emit(1);
            if (_weaponIndex == 1)
            {
                for (int i = 1; i < _shotSmokePS.Length; i++)
                {
                    _shotSmokePS[i].Emit(1);
                }
            }
        }
        //if weapon is out of ammo and it's not main weapon
        else if(_weapons[_weaponIndex].Ammo <= 0 && _weaponIndex != 0 && CanFireCheck())
        {
            if(_weapons[_weaponIndex].WSOT == WeaponSO.WeaponSOType.tripleShot)
            {
                _myAN.ActivateSideCannons(false);
            }
            _myAH?.FireEvent(1);
            _weaponIndex = 0;
            if(_weapons[_weaponIndex].Ammo == 0)
            {
                _weapons[_weaponIndex].UpdateAmmo(5);
            }
            _myAC.UpdateAmmoCounter(_weapons[_weaponIndex].Ammo, _weapons[_weaponIndex].MaxAmmo);
            _myAC.ChangeAmmoColor(_weaponIndex);
            FireWeapon();
        }

    }

    private bool CanFireCheck()
    {

        if (Time.time > _shotReady)
        {
            return true;
        }
        return false;
    }

    public void SwitchWeapon(int i)
    {
        if (_weaponIndex == i)
        {
            if (_weapons[i].Ammo < _weapons[i].MaxAmmo)
            {
                _weapons[i].UpdateAmmo(_weapons[i].AmmoReload);
                _mySPSC.ChangePSColor(i);
                _myAH?.FireEvent(3);

            }
        }
        else
        {
            if (_weapons[_weaponIndex].WSOT == WeaponSO.WeaponSOType.tripleShot)
            {
                _myAN.ActivateSideCannons(false);
            }
            _weaponIndex = i;
            _weapons[_weaponIndex].UpdateAmmo(_weapons[_weaponIndex].AmmoReload);
            
            
        }
        _myAC.UpdateAmmoCounter(_weapons[_weaponIndex].Ammo, _weapons[_weaponIndex].MaxAmmo);
        _myAC.ChangeAmmoColor(_weaponIndex);
    }

    public void ReloadMainAmmo()
    {
        _weapons[0].UpdateAmmo(_weapons[0].AmmoReload);
        if (_weaponIndex == 0)
        {
            _myAC.UpdateAmmoCounter(_weapons[_weaponIndex].Ammo, _weapons[_weaponIndex].MaxAmmo);

        }
        _mySPSC.ChangePSColor(0);
        _myAH?.FireEvent(3);
    }
    IEnumerator EjectShells(int shells)
    {
        for (int i = 0; i < shells; i++)
        {
            OnShellEject?.Invoke();
            yield return _shellDelay;
        }
    }


}
