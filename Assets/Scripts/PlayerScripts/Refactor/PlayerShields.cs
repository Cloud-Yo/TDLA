using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerShields : MonoBehaviour
{
    [SerializeField] private int _shieldHP;
    public int ShieldHP { get { return _shieldHP; } private set { _shieldHP = value; } }

    [SerializeField] private Light2D[] _shieldLights;
    public Light2D[] ShieldLights { get { return _shieldLights; } private set { _shieldLights = value; } }

    [SerializeField] private int _maxShields;
    [SerializeField] private PlayerAnimations _myPAN = null;
    [SerializeField] private bool _shieldsActive = false;
    public bool ShieldsActive { get { return _shieldsActive; } private set { _shieldsActive = value; } }


    void Start()
    {
        _shieldHP = _maxShields;
        _shieldsActive = false;
        for (int i = 0; i < _shieldLights.Length; i++)
        {
            _shieldLights[i].enabled = false;
        }
    }


    public void ActivateShields()
    {
        _shieldHP = _maxShields;

        if (!_shieldsActive)
        {
            _shieldsActive = true;
            _myPAN.ActivateShield(_shieldsActive);
            
        }
        StartCoroutine(ActivateShieldUIDisplay());
    }


    IEnumerator ActivateShieldUIDisplay()
    {
        for (int i = 0; i < _shieldLights.Length; i++)
        {
            _shieldLights[i].enabled = true;
            yield return new WaitForSeconds(0.15f);
        }
    }

    public bool AreShieldsActive()
    {
        return _shieldsActive;
    }

    public void DamageShield(int dmg)
    {
        if (_shieldHP > 1)
        {
            _shieldLights[_shieldHP - 1].enabled = false;
            _shieldHP--;
        }
        else if(_shieldHP == 1)
        {
            _shieldLights[_shieldHP - 1].enabled = false;
            _shieldHP--;
            _shieldsActive = false;
            _myPAN.ActivateShield(_shieldsActive);
        }
    }

}
