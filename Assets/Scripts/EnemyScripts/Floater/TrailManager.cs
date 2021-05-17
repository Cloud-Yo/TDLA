using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    private TrailRenderer _myTR = null;
    [SerializeField] private float _yMin = -6f;
    void Start()
    {
        _myTR = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _yMin)
        {
            _myTR.enabled = false;
        }
        else if (!_myTR.enabled && transform.position.y > _yMin)
        {
            _myTR.enabled = true;
        }
    }
}
