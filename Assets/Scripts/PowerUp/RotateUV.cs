using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUV : MonoBehaviour
{
    private SpriteRenderer _mySR = null;
    private Material _myMat = null;
    private float _time = 0f;
    [SerializeField] private float _rotSpd = 5f;
    [SerializeField] private bool _rotating;
    void Start()
    {
        _time = 0f;
        _mySR = GetComponent<SpriteRenderer>();
        _myMat = _mySR.material;

    }

    // Update is called once per frame
    void Update()
    {
        UVRotation();
    }

    private void UVRotation()
    {
        if (_rotating)
        {
            _time += Time.deltaTime * _rotSpd;
            _myMat.SetFloat("_Rotation", _time);
        }
        
    }
}
