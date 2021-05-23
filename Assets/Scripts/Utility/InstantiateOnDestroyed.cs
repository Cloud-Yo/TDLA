using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnDestroyed : MonoBehaviour
{
    [SerializeField] private GameObject _go = null;

    private void OnDestroy()
    {
        Instantiate(_go, this.gameObject.transform.position, Quaternion.identity);
    }
}
