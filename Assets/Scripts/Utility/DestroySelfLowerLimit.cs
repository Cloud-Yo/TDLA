using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfLowerLimit : MonoBehaviour
{
    [SerializeField] private float _destroyPoint = 6.5f;
    void Update()
    {
        DestroySelf();
    }
    private void DestroySelf()
    {
        if (transform.position.y < -_destroyPoint)
        {
            Destroy(this.gameObject);
        }
    }
}
