using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _go = null;

    public void InstantiateGameObject()
    {
        Instantiate(_go, this.transform.position, Quaternion.identity);
    }
}
