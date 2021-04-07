using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    [SerializeField] private UIManager _myUIM = null;
    [SerializeField] private GameManager _myGM = null;
    private void OnEnable()
    {
        _myGM = FindObjectOfType<GameManager>();
        _myUIM = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

         if(other.CompareTag("PlayerLaser"))
         {
             Destroy(other.gameObject);
             Destroy(this.gameObject);
         }
         else if(other.CompareTag("Player"))
         {
             
             other.GetComponent<Player>()?.TakeDamage();
             other.GetComponent<PlayerCore>()?.TakeDamage();
             Destroy(this.gameObject);
         }

    }
}
