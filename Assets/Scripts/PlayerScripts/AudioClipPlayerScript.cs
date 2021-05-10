using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinAttribute = UnityEngine.MinAttribute;

[RequireComponent(typeof(AudioSource))]
public class AudioClipPlayerScript : MonoBehaviour
{
    [SerializeField] private AudioSource _myAS = null;
    public AudioSource MyAS { get { return _myAS; } private set { _myAS = value; } }

    void Start()
    {
        MyAS = GetComponent<AudioSource>();

    }

    public void PlayOneShotClip(AudioClip c)
    {
        _myAS.PlayOneShot(c);
    }

    public void PlayLoopingClip(AudioClip c)
    {
        _myAS.clip = c;
        _myAS.loop = true;
        _myAS.Play();
    }
}
