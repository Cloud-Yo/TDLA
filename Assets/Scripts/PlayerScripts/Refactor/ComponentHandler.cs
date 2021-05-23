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
        GameManager.OnGameStarted += StartGame;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= StartGame;
    }
    void Start()
    {
        foreach (var c in _components)
        {
            c.enabled = false;
        }
    }

    private void StartGame()
    {
        GameManager.OnGameStarted -= StartGame;
        HandleComponents(true);
    }
    public void HandleComponents(bool on)
    {
        
        foreach (var c in _components)
        {
            c.enabled = on;
        }

    }



}
