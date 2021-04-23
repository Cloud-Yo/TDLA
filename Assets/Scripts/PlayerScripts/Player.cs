using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private GameManager _myGM = null;
    void Start()
    {
        transform.position = Vector3.zero;
        _myGM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        _health--;
        if(_health < 1)
        {
            _myGM.GameOver();
            Destroy(this.gameObject);
        }
    }
}
