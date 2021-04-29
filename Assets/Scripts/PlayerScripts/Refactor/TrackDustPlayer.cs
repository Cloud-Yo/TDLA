using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDustPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _partSys;
    [SerializeField] private float _spd;

    
    void Start()
    {
        _spd = GameManager.Instance.GetWorldSpeed();
        foreach (var p in _partSys)
        {
            p.Stop();
        }    
    }

    // Update is called once per frame
    void Update()
    {
        PlayStopPS();
    }

    private void PlayStopPS()
    {
        if (GameManager.Instance.GetWorldSpeed()!= 0 && _spd != GameManager.Instance.GetWorldSpeed())
        {
            _spd = GameManager.Instance.GetWorldSpeed();
            foreach (var p in _partSys)
            {
                var main = p.main;
                main.startSpeed = -GameManager.Instance.GetWorldSpeed();
                if (!p.isPlaying)
                {
                    p.Play();
                }
            }
        }
        else if(GameManager.Instance.GetWorldSpeed() == 0 && _spd != 0)
        {
            _spd = 0;
            foreach (var p in _partSys)
            {
                p.Stop();
            }
        }
    }
}
