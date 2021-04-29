using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPlayerWeapon
{

    AudioClip SFX { get ; set; }

    UnityEvent OnShootEvent { get; set; }

    int Shells { get; set; }

    GameObject WeaponPrefab { get; set; }

    int Ammo { get; set; }

    int MaxAmmo { get; set; }

    int AmmoReload { get; set; }
    IPlayerWeapon Shoot();

}
