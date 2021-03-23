using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private float _spd;
    [SerializeField] private Rigidbody _myRB = null;
    
}
