using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    private UIManager _myUIM = null;
    private GameManager _myGM = null;
     private Animator _myAN = null;
    private AudioSource _myAS = null;
    [SerializeField] private GameObject _explosionFX = null;
    [SerializeField] private int _points = 35;
    private Rigidbody2D _myRB2D = null;
    private ScoreManager _mySM = null;
    private bool _isHit = false;
    [Header("Is Mimic")]
    [SerializeField] private bool _mimic = false;
    [Header("Has Shield")]
    [SerializeField] private bool _shield = false;
    [SerializeField] private AudioClip _shieldBreakAudioClip = null;
    private void OnEnable()
    {
        _myAS = GetComponent<AudioSource>();
        _myAN = GetComponent<Animator>();
        _myRB2D = GetComponent<Rigidbody2D>();
        _mySM = FindObjectOfType<ScoreManager>();
        _myGM = FindObjectOfType<GameManager>();
        _myUIM = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCore>()?.TakeDamage();
            DirectHit();
        }

    }


    public void TakeDamage()
    {
        if (!_shield)
        {
            DirectHit();
        }
        else
        {
            ShieldHit();
        }
    }


    private void DirectHit()
    {
        if (!_isHit)
        {
            _isHit = true;
            Destroy(_myRB2D);
            Instantiate(_explosionFX, transform.position, Quaternion.identity);
            _mySM.UpdateScore(_points);
            UpdateCount();
            Destroy(this.gameObject);
        }

    }

    private void ShieldHit()
    {
        _myAN.SetTrigger("BreakShield");
        _myAS.PlayOneShot(_shieldBreakAudioClip, 0.5f);
        _shield = false;
    }


    private void UpdateCount()
    {
        if (!_mimic)
        {
            WaveManager.EnemyCount--;
        }
        _myUIM.SetInfoText(true);
    }

}
