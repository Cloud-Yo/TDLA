using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicBehavior : MonoBehaviour
{
    private PowerUp _myPU = null;
    [SerializeField] private GameObject _mimicGO = null;
    [SerializeField] private float _radius = 1.25f;
    [SerializeField] private LayerMask _playerMask;
    private bool _isMimicInRange => Physics2D.OverlapCircle(transform.position, _radius, _playerMask) && !_mimicGO.activeInHierarchy;

    private void Awake()
    {

    }
    void Start()
    {
        _mimicGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isMimicInRange)
        {
            _mimicGO.SetActive(true);
        }
    }
}
