using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    [SerializeField] private Collider _myColl = null;
    [SerializeField] private Rigidbody _myRB = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerLaser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCore>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
