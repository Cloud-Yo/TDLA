using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleShot : MonoBehaviour, IPlayerWeapon
{
    [SerializeField] private AudioClip _sfx = null;
    public AudioClip SFX { get => _sfx; set => value = _sfx; }
    public UnityEvent OnShootEvent { get; set; }

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



    public IPlayerWeapon Shoot()
    {
        return this;
    }

}
