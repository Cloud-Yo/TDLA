using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _lifeLights;
    void Start()
    {
        foreach (var l in _lifeLights)
        {
            l.SetActive(true);
        }
    }

    public void UpdateLifeLights(int h)
    {
        _lifeLights[h].SetActive(false);
    }
}
