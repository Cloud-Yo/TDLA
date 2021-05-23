using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private Sprite[] _digiNumsArray;
    [SerializeField] private Sprite[] _digiMaxNumsArray;
    [SerializeField] private Image[] _digiNums;
    [SerializeField] private Image _digiNums01;
    [SerializeField] private Image _digiNums10;
    [SerializeField] private Image _digiMaxNums01;
    [SerializeField] private Image _digiMaxNums10;
    [SerializeField] private Color _mainCol, _TripShotCol, _grapeShotCol, _homingMissleCol;

    private int _currentMaxAmmo;


    private void Start()
    {
        SetColor(_mainCol);
        UpdateAmmoCounter(15, 99);
    }

    public void UpdateAmmoCounter(int ammo, int max)
    {
        if (ammo > max)
        {
            ammo = max;

        }

        int tens = ammo / 10;
        int ones = ammo % 10;
        _digiNums10.sprite = _digiNumsArray[tens];
        _digiNums01.sprite = _digiNumsArray[ones];

        tens = max / 10;
        ones = max % 10;
        _digiMaxNums10.sprite = _digiMaxNumsArray[tens];
        _digiMaxNums01.sprite = _digiMaxNumsArray[ones];
    }


    public void ChangeAmmoColor(int type)
    {
    
        switch (type)
        {
            case 0: //main ammo color
                SetColor(_mainCol);
                break;
            case 1: //triple shot color
                SetColor(_TripShotCol);
                break;
            case 2: //grapeShot color
                SetColor(_grapeShotCol);
                break;
            case 3: //homing Missle color
                SetColor(_homingMissleCol);
                break;
            default:
                SetColor(_mainCol);
                break;
        }
    }

    private void SetColor(Color col)
    {
        _digiNums01.color = col;
        _digiNums10.color = col;
        _digiMaxNums01.color = col;
        _digiMaxNums10.color = col;
    }

}
