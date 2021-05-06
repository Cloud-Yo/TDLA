using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class ThrusterBarManager : MonoBehaviour
{
    [SerializeField] private Image _thruster = null;
    [SerializeField] private Light2D _redLight = null;
    [SerializeField] private Light2D _greenLight = null;
    [SerializeField] private float _lightIntensity = 3f;

    private void OnEnable()
    {
        PlayerMovement.OnAcceleration += ResizeThrusterBar;
    }

    private void OnDisable()
    {
        PlayerMovement.OnAcceleration -= ResizeThrusterBar;
    }

    void Start()
    {
        _thruster.fillAmount = 1;
        _redLight.enabled = false;
    }


    private void ResizeThrusterBar(float t)
    {

        int i = (int)(t*10);
        t = i / 10f;
        
        _thruster.fillAmount = t;
        if (t <= 0)
        {
            _greenLight.enabled = false;
            _redLight.enabled = true;
            StartCoroutine(BlinkRedLight());
        }
        
    }

    IEnumerator BlinkRedLight()
    {

        float rate = 1;
        while (_thruster.fillAmount < 1)
        {
            rate += Time.deltaTime;
            _redLight.intensity = Mathf.PingPong(rate * 9f, _lightIntensity);
            yield return null;
        }
        _redLight.intensity = _lightIntensity;
        _redLight.enabled = false;
        _greenLight.enabled = true;
        yield break;
    }
}
