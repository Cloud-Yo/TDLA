using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    #region Shooting Variables
    public delegate void OnShootingDelegate();
    public static OnShootingDelegate MyShootingDelegate;
    [Header("Shooting")]
    [SerializeField] private float _shotReady = -1f;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private bool _canFire = true;
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
    [SerializeField] private GameObject _laserPrefab = null;
    [SerializeField] private GameObject _tripleShot = null;
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
      _myAS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ShootingInput();
    }

    private void ShootingInput()
    {
        CanFireCheck();
        if (_canFire && Input.GetKeyDown(KeyCode.Space))
        {

            //Shoot
            MyShootingDelegate?.Invoke();
            _shotReady = Time.time + _fireRate;
            
            _canFire = false;
        }
    }

    private void CanFireCheck()
    {

        if (Time.time > _shotReady)
        {
            _canFire = true;
        }
    }

    #region Ammo Types
    private void NormalShot()
    {
        var laser = Instantiate(_laserPrefab, transform.position + _offset , Quaternion.identity);
        laser.transform.tag = "PlayerLaser";
        _myAN.CannonShoot();
        _turretSmoke.Emit(1);
        OnShoot?.Invoke();
        _myAS.PlayOneShot(_singleShotFX, 0.8f);
    
    }

    private void FireTripleShot()
    {
        var laser = Instantiate(_tripleShot, transform.position, Quaternion.identity);
        laser.transform.tag = "PlayerLaser";
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

    public void SetTripleShot()
    {
        MyShootingDelegate = FireTripleShot;
    }

    public void SetNormalShot()
    {
        MyShootingDelegate = NormalShot;
    }
}
