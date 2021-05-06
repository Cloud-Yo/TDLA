using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent[] _events;

    public void FireEvent(int index)
    {
        _events[index]?.Invoke();
    }

}
