using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    [SerializeField] private ParticleSystem _myPS = null;
    void Start()
    {
        _myPS = GetComponent<ParticleSystem>();
        //_myPS.Stop();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            _myPS.Play();
            Debug.Log("Splish Splash!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            _myPS.Stop();
        }
    }
}
