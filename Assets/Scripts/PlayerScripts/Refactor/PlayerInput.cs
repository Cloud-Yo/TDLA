using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent OnFireInput;
    public static Action<bool> OnAttracting;
    [SerializeField] private KeyCode _shootKey = KeyCode.Space;
    [SerializeField] private KeyCode _attractKey = KeyCode.C;
 
    private void Update()
    {
        if (Input.GetKeyDown(_shootKey))
        {
            OnFireInput?.Invoke();
        }
        if (Input.GetKey(_attractKey))
        {

            OnAttracting?.Invoke(true);

        }
        else if (!Input.GetKeyUp(_attractKey))
        {
            OnAttracting?.Invoke(false);

        }
    }
}
