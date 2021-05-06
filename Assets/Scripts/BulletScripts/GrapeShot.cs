using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeShot : MonoBehaviour
{
    [SerializeField] private float _angleMax = 40f;
    [SerializeField] private GameObject[] _bullets;
    [SerializeField] private float _angleOffset = 5;
    private void Start()
    {
        SetBulletSpread();
    }


    private void SetBulletSpread()
    {
        float x = 0;
        float y = _angleMax;

        for(int i = 0; i < _bullets.Length; i++)
        {
            _bullets[i].transform.tag = "PlayerBullet";
            x = Random.Range(-_angleMax, _angleMax);
            while (i != 0 && Mathf.Abs(x - y) < _angleOffset)
            {
                x = Random.Range(-_angleMax, _angleMax);
            }
            _bullets[i].transform.rotation = Quaternion.Euler(0,0,x);
            y = x;
        }
    }
}
