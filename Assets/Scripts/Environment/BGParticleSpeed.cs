using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParticleSpeed : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _bgParticles;
    [SerializeField] private float _spd;
    private float _currentspd;
    void Start()
    {
        SetBGSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetWorldSpeed() != _currentspd)
        {
            SetBGSpeed();
        }
    }

    void SetBGSpeed()
    {
        _currentspd = GameManager.Instance.GetWorldSpeed();
        _spd = _currentspd*2f;
        foreach (var item in _bgParticles)
        {
            var main = item.main;
            main.simulationSpeed = _spd;
        }
    }
}
