using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModularShooting : MonoBehaviour
{

    private PlayerAnimations _myAN = null;
    private AudioSource _myAS = null;
    [SerializeField] private WeaponSO[] _weapons;
    [SerializeField] private int _weaponIndex = 0;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private float _sfxVolume = 0.8f;
    [SerializeField] private Transform _bulletContainer = null;
    [SerializeField] private float _shotReady = -1f;
    [SerializeField] private float _fireRate = 0.5f;
    private WaitForSeconds _shellDelay = new WaitForSeconds(0.15f);
    [SerializeField] private UnityEvent OnShellEject;
    private AmmoCounter _myAC = null;




    private void Start()
    {
        _myAN = GetComponent<PlayerAnimations>();
        _currentAmmo = _weapons[_weaponIndex].Ammo;
        _myAS = GetComponent<AudioSource>();
        _myAC = GetComponent<AmmoCounter>();

    }

    public void FireWeapon()
    {
        if (_currentAmmo > 0 && CanFireCheck())
        {
            _shotReady = Time.time + _fireRate;
            _currentAmmo--;
            _myAC.UpdateAmmoCounter(_currentAmmo, _weapons[_weaponIndex].MaxAmmo);
            GameObject bullet = Instantiate(_weapons[_weaponIndex].WeaponPrefab, transform.position, Quaternion.identity);
            bullet.transform.SetParent(_bulletContainer);
            bullet.transform.tag = "PlayerBullet";
            _myAS.PlayOneShot(_weapons[_weaponIndex].SFX, _sfxVolume);
            _myAN.FireAnimation(_weaponIndex);
            if (_weapons[_weaponIndex].Shells > 0)
            {
                StartCoroutine(EjectShells(_weapons[_weaponIndex].Shells));
            }
        }
        else if(_currentAmmo <= 0 && _weaponIndex != 0 && CanFireCheck())
        {
            if(_weapons[_weaponIndex].WSOT == WeaponSO.WeaponSOType.tripleShot)
            {
                _myAN.ActivateSideCannons(false);
            }
            SwitchWeapon(0);
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
            if (_currentAmmo < _weapons[i].MaxAmmo)
            {
                _currentAmmo += _weapons[i].AmmoReload;
                if (_currentAmmo > _weapons[i].MaxAmmo)
                {
                    _currentAmmo = _weapons[i].MaxAmmo;
                }
                
            }
        }
        else
        {
            if (_weapons[_weaponIndex].WSOT == WeaponSO.WeaponSOType.tripleShot)
            {
                _myAN.ActivateSideCannons(false);
            }
            _weaponIndex = i;
            _currentAmmo = _weapons[i].AmmoReload;
        }
        _myAC.UpdateAmmoCounter(_currentAmmo, _weapons[_weaponIndex].MaxAmmo);
        _myAC.UpdateMaxAmmo(_weapons[_weaponIndex].MaxAmmo);
        _myAC.ChangeAmmoColor(_weaponIndex);
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
