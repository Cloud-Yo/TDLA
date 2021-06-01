using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRamming : MonoBehaviour
{
    [SerializeField] private float _spd = 1.5f;
    [SerializeField] private float _range = 3f;
    private GameObject _player = null;
    private AudioSource _myAS = null;
    [SerializeField] private AudioClip _roarSFX = null;

    private bool _canRam => Vector2.Distance(_player.transform.position, transform.position) <= _range && transform.position.y > _player.transform.position.y;
    private bool _isRamming = false;
    private void OnEnable()
    {
        _player = GameObject.FindObjectOfType<PlayerCore>().gameObject;
        _myAS = GetComponent<AudioSource>();
    }

    void Update()
    {
        Ramming();
    }

    private void Ramming()
    {
        if (_canRam)
        {
            if (!_isRamming)
            {
                _myAS.PlayOneShot(_roarSFX, 0.65f);
                _isRamming = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_player.transform.position.x, transform.position.y), _spd * Time.deltaTime);
        }
        else
        {
            if (_isRamming)
            {
                _isRamming = false;
            }
        }
    }

}
