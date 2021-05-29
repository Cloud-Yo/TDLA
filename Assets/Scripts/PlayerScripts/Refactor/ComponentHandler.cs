using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentHandler : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _components;

    [SerializeField]
    private List<GameObject> _goList = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.OnEnableControls += HandleComponents;
    }

    private void OnDisable()
    {
        GameManager.OnEnableControls -= HandleComponents;
    }
    void Start()
    {
        foreach (var c in _components)
        {
            c.enabled = false;
        }
    }

    public void HandleComponents(bool on)
    {
        
        foreach (var c in _components)
        {
            c.enabled = on;
        }

    }



}
