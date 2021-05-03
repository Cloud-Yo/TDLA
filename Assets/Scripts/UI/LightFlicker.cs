using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Light2D _light;
    [SerializeField] private float _rand;
    [SerializeField] private WaitForSeconds _fDelay = new WaitForSeconds(0.1f);
    [SerializeField] private bool _gameOver = false;
    [SerializeField] private Vector2 _intensityMinMax;
    void Start()
    {
        _light = GetComponent<Light2D>();
        StartCoroutine(FlickerLights());
    }


    IEnumerator FlickerLights()
    {
       while(_light.enabled)
        {
            yield return _fDelay;
            _light.intensity = Random.Range(_intensityMinMax.x, _intensityMinMax.y);
            
        }

    }
}
