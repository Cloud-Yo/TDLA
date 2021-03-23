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

    #region Ammo Variables
    [Header("Ammo")]
    [SerializeField] private GameObject _laserPrefab = null;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0.8f, 0f);
    #endregion

    void Start()
    {
      MyShootingDelegate = FireLaser;
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
    private void FireLaser()
    {
        var laser = Instantiate(_laserPrefab, transform.position + _offset, Quaternion.identity);
        laser.transform.tag = "PlayerLaser";
        _myPartSys.Emit(1);
    }

    private void FireTripleShot()
    {

    }
    #endregion
}
