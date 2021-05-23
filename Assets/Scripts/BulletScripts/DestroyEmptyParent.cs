using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEmptyParent : MonoBehaviour
{
    private bool _allBulletsDestroyed => this.transform.childCount == 0;

    // Update is called once per frame
    void Update()
    {
        if (_allBulletsDestroyed)
        {
            Destroy(this.gameObject);
        }
    }
}
