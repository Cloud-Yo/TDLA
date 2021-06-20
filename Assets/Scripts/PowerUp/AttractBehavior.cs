using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractBehavior : MonoBehaviour
{
    [SerializeField] private float _spd = 2f;
    private bool _attraction = false;

    public bool Attraction
    {
        get { return _attraction; }
        set { if(_attraction != value) _attraction = value; }
    }

    private Transform _playerT = null;
    public Transform PlayerT { set{ _playerT = value; } }
   

    void Update()
    {
        if (Attraction && _playerT != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerT.position, _spd * Time.deltaTime);
        }
    }

}
