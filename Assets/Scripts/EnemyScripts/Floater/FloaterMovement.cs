using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterMovement : MonoBehaviour
{
    [SerializeField] private Transform _parent = null;
    [SerializeField] private float _sinFreq = 1f;
    [SerializeField] private float _sinAmplitude = 1f;
    [SerializeField] private bool _isDead = false;
    void Start()
    {
        _sinFreq = Random.Range(1f, 5f);
        _sinAmplitude = Random.Range(-0.75f, -1.25f);
        _parent = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        FloaterMove();
    }

    private void FloaterMove()
    {
        if (!_isDead)
        {
            float x = (_parent.position.x + Mathf.Sin(Time.time * _sinFreq) * _sinAmplitude);
            float y = _parent.position.y;

            transform.position = new Vector2(x, y);
        }

    }
}
