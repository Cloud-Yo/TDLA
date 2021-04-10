using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerAnimations : MonoBehaviour
{
    [SerializeField] public  Animator _turretAN = null;
    [SerializeField] private Animator _sideCannonAN = null;
    [SerializeField] private Animator _shieldAN = null;
    [SerializeField] private Animator _playerAN = null;


   
    
    public void CannonShoot()
    {
        _turretAN.SetTrigger("Shoot");
    }



    #region TripleShot
    public void ActivateSideCannons(bool x)
    {
        _sideCannonAN.SetBool("SC_Active", x);
    }

    public void SideCannonShoot()
    {
        _sideCannonAN.SetTrigger("SC_Shoot");
    }

    public void ActivateShield(bool x)
    {
        _shieldAN.SetBool("Shield", x);
    }
    #endregion
}
