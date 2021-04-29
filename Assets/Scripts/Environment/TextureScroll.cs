using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    [SerializeField] private Material _mat = null;
    [SerializeField] private float _spd = 2f;
    [SerializeField] private float _currentSpd = 2f;
    private void OnEnable()
    {
        GameManager.OnSetGlobalSpeed += SetGlobalSpeed;
    }
    private void OnDisable()
    {
        GameManager.OnSetGlobalSpeed -= SetGlobalSpeed;
    }

    private void SetGlobalSpeed(float s)
    {
        _spd = s;
        _currentSpd = _spd;
    }

    // Update is called once per frame
    void Update()
    {
        _mat.mainTextureOffset += Vector2.up * (Time.deltaTime * _spd); 
        if(GameManager.Instance.GetWorldSpeed() != _currentSpd)
        {
            SetGlobalSpeed(GameManager.Instance.GetWorldSpeed());
        }
    }
}
