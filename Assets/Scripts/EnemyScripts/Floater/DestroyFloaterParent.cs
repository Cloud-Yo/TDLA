using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloaterParent : MonoBehaviour
{
    [SerializeField] private Transform _parent = null;
    void Start()
    {
        _parent = transform.parent.transform;
    }

    private void OnDestroy()
    {
        Destroy(_parent.gameObject);
    }
}
