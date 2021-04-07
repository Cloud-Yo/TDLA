using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _cog = null;
    [SerializeField] private SpriteRenderer _puType = null;
    [SerializeField] private Sprite[] _types;
    [SerializeField] float _spd = 2f;
    [SerializeField] float _rotSpd = 2f;
    [SerializeField] private int _index = 0;
    [SerializeField] private GameManager _myGM = null;

    void Start()
    {
        _myGM = FindObjectOfType<GameManager>();
        _puType.sprite = _types[0];
        _spd = _myGM.GetWorldSpeed() * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        _cog.transform.Rotate(new Vector3(0,0,1), _spd * _rotSpd);
        transform.Translate(Vector2.down * _spd * Time.deltaTime);
        if(transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }

    }

    public void SetType(int i)
    {
        _puType.sprite = _types[i];
        _index = i;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCore>().StartTrippleShot();
            Destroy(this.gameObject);
        }
    }
}
