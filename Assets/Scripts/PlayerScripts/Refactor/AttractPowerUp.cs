using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractPowerUp : MonoBehaviour
{
    private bool _attracting = false;
    private AttractBehavior[] _attractable;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private ParticleSystem _attractPS = null;
    [SerializeField] private AudioSource _myAS = null;
    private void OnEnable()
    {
        _attractPS.Stop();
        PlayerInput.OnAttracting += SetAttractionState;
    }
    private void OnDisable()
    {
        PlayerInput.OnAttracting -= SetAttractionState;

    }
    private void Update()
    {
        AttractPowerUps();
    }

    private void SetAttractionState(bool attracting)
    {
        if (_attracting != attracting)
        {
            _attracting = attracting;
        }
    }

    private void AttractPowerUps()
    {
        if (_attracting)
        {
            if (!_attractPS.isPlaying)
            {
                _attractPS.Play();
                _myAS.Play();
            }
            _attractable = FindObjectsOfType<AttractBehavior>();
            foreach (var a in _attractable)
            {
                if (Vector2.Distance(a.transform.position, this.transform.position) <= _radius && a.Attraction == false)
                {
                    a.PlayerT = this.transform;
                    a.Attraction = true;
                }

            }
        }
        else if (!_attracting)
        {
            _attractPS.Stop();
            _myAS.Stop();
            _attractable = FindObjectsOfType<AttractBehavior>();
            foreach (var a in _attractable)
            {
                a.Attraction = false;
            }

        }
    }
}
