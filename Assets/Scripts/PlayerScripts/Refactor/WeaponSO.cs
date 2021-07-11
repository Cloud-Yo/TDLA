using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Weapon", menuName = "TDLA/Player/Weapons")]
public class WeaponSO : ScriptableObject
{
    public enum WeaponSOType
    {
        singleShot,
        tripleShot,
        shotGunShot,
        homingMissle,
        numberOfWeaponSOTypes
    }
    [SerializeField] private WeaponSOType _wSOT = WeaponSOType.singleShot;
    public WeaponSOType WSOT { get { return _wSOT; } private set { _wSOT = value; } }

    [SerializeField] private AudioClip _sfx = null;
    public AudioClip SFX { get => _sfx; set => value = _sfx; }
  
    [SerializeField] private int _shells;
    public int Shells { get => _shells; set => value = _shells; }

    [SerializeField] private GameObject _weaponPrefab = null;
    public GameObject WeaponPrefab { get => _weaponPrefab; set => value = _weaponPrefab; }

    [SerializeField] private int _ammo;
    public int Ammo { get => _ammo; set => value = _ammo; }

    [SerializeField] private int _maxAmmo;
    public int MaxAmmo { get => _maxAmmo; set => value = _maxAmmo; }

    [SerializeField] private int _ammoReload;
    public int AmmoReload { get => _ammoReload; set => value = _ammoReload; }

    [SerializeField] private Vector2 _firePos;

    public Vector2 FirePos
    {
        get { return _firePos; }
        set { _firePos = value; }
    }



    //Ammo can be added or removed
    public void UpdateAmmo(int ammo)
    {
        _ammo += ammo;
        if (_ammo < 0)
        {
            _ammo = 0;
        }
        else if (_ammo > MaxAmmo)
        {
            _ammo = MaxAmmo;
        }
    }

    public void ResetAmmo()
    {
        _ammo = 0;
    }
}
