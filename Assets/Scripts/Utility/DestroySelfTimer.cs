using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfTimer : MonoBehaviour
{
    [SerializeField] private float _time = 1f;
    private void Start()
    {
        DestroySelf();
    }
    private void DestroySelf()
    {
        Destroy(this.gameObject, _time);
    }
}
