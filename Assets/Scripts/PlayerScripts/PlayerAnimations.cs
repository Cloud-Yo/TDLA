using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] public  Animator _turretAN = null;
    [SerializeField] private Animator _sideCannonAN = null;
    [SerializeField] private Animator _shieldAN = null;
    [SerializeField] private Animator _playerAN = null;
    [SerializeField] private Animator _tracksAN = null;

    private void Update()
    {
        _tracksAN.speed = GameManager.Instance.GetWorldSpeed() == 0 ? 0 : 1;
    }


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

    public void FireAnimation(int i)
    {
        switch(i)
        {
            case 0:
                _turretAN.SetTrigger("Shoot");
                break;
            case 1:
                _turretAN.SetTrigger("Shoot");
                _sideCannonAN.SetTrigger("SC_Shoot");
                break;
            default:
                break;
        }
    }
}
