using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerShooting : MonoBehaviour
{
    public enum WeaponTypes
    {
        singleShot,
        tripleShot,
        weaponTypes
    }
    #region Shooting Variables
    public delegate void OnShootingDelegate();
    public static OnShootingDelegate MyShootingDelegate;
    public static Action<GameObject> OnFireAction;
    public static Action<int> OnLoaDReload;
    [Header("Shooting")]
    [SerializeField] private float _shotReady = -1f;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private ParticleSystem _myPartSys = null;
    [SerializeField] private UnityEvent OnShoot = new UnityEvent();
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] private PlayerAnimations _myAN = null;
    [SerializeField] private ParticleSystem[] _sideSmoke;
    [SerializeField] private ParticleSystem _turretSmoke = null;
    [SerializeField] private AudioSource _myAS = null;
    #endregion

    #region Ammo Variables
    [Header("Ammo")]
    [SerializeField] private WeaponTypes _weaponTypes;
    [SerializeField] private GameObject _singleShot = null;
    [SerializeField] private GameObject _tripleShot = null;
    [SerializeField] private GameObject _myAmmoType = null;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0.475f, 0f);
    private WaitForSeconds _tripDelay = new WaitForSeconds(0.15f);
    #endregion

    #region Audio
    [Header("Audio Files")]
    [SerializeField] private AudioClip _singleShotFX = null;
    [SerializeField] private AudioClip _tripleShotFX = null;
    #endregion

    void Start()
    {
        MyShootingDelegate = NormalShot;
        _myAmmoType = _singleShot;
        _myAS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ShootingInput();
    }

    private void ShootingInput()
    {
        if (CanFireCheck() && Input.GetKeyDown(KeyCode.Space))
        {
            //Shoot based on delegate
            MyShootingDelegate?.Invoke();
            //instantiates prefab
            OnFireAction?.Invoke(_myAmmoType);
            _shotReady = Time.time + _fireRate;
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

    #region Ammo Types
    private void NormalShot()
    {
        var shot = Instantiate(_singleShot, transform.position + _offset , Quaternion.identity);
        shot.transform.tag = "PlayerBullet";
        _myAN.CannonShoot();
        _turretSmoke.Emit(1);
        OnShoot?.Invoke();
        _myAS.PlayOneShot(_singleShotFX, 0.8f);

    }

    private void FireTripleShot()
    {
        var shot = Instantiate(_tripleShot, transform.position, Quaternion.identity);
        shot.transform.tag = "PlayerBullet";
        _myAN.CannonShoot();
        _myAN.SideCannonShoot();
        _turretSmoke.Emit(1);
        foreach (var item in _sideSmoke)
        {
            item.Emit(1);
        }
        StartCoroutine(TripleShotEject());
        _myAS.PlayOneShot(_tripleShotFX, 0.8f);
    }
    IEnumerator TripleShotEject()
    {
        for (int i = 0; i < 3; i++)
        {
            OnShoot?.Invoke();
            yield return _tripDelay;
        }

    }
    #endregion

    public void SetWeaponType(int weap, int ammo)
    {
        switch (weap)
        {
            case 0:
                MyShootingDelegate = NormalShot;
                _myAmmoType = _singleShot;
                OnLoaDReload?.Invoke(ammo);
                break;
            case 1:
                MyShootingDelegate = FireTripleShot;
                _myAmmoType = _tripleShot;
                OnLoaDReload?.Invoke(ammo);
                StartCoroutine(TSCooldown());
                break;
            default:
                MyShootingDelegate = NormalShot;
                break;
        }
    }

    IEnumerator TSCooldown()
    {
        yield return new WaitForSeconds(5f);
        _myAN.ActivateSideCannons(false);
        SetWeaponType(0, 15);
    }
}
