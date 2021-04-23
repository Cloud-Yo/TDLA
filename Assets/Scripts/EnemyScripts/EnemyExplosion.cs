using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] private float _spd;
    [SerializeField] private SpriteRenderer _mySR = null;
    [SerializeField] private ParticleSystem _myPS = null;
    [SerializeField] private GameManager _myGM = null;


    void Start()
    {
        Debug.Log("Enemy Boom!");
        _myGM = FindObjectOfType<GameManager>();
        _mySR.flipX = FlipSprite();
        _spd = _myGM.GetWorldSpeed() * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * _spd * Time.deltaTime);
        if (transform.position.y < -8.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private bool FlipSprite()
    {
        int x = Random.Range(0,2);
        return x != 0;
    }
}
