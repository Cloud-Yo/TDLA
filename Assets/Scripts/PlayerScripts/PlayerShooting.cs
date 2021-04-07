using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    [Header("Components")]
    [SerializeField] private PlayerAnimations _myAN = null;
    [SerializeField] private ParticleSystem[] _sideSmoke;
    [SerializeField] private ParticleSystem _turretSmoke = null;

    #region Ammo Variables
    [Header("Ammo")]
    [SerializeField] private GameObject _laserPrefab = null;
    [SerializeField] private GameObject _tripleShot = null;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0.475f, 0f);
    #endregion

    void Start()
    {
      MyShootingDelegate = NormalShot;
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
        //_myPartSys.Emit(1);
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
