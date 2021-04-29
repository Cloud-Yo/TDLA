using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent OnFireInput;
    [SerializeField] private KeyCode _shootKey = KeyCode.Space;
    private void Update()
    {
        if (Input.GetKeyDown(_shootKey))
        {
            OnFireInput?.Invoke();
        }
    }
}
