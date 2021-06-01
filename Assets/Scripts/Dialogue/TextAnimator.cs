using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    private AudioSource _myAS = null;
    [SerializeField] private AudioClip _txtAudioClip = null;

    [SerializeField] private Button _continueButton = null;
    private TMP_Text _text = null;
    private int _visibleChar = 0;
    public int VisibleChar { get { return _visibleChar; } }
    private int _charCount;
    public int CharCount { get { return _charCount; } }


    void Start()
    {
        _myAS = gameObject.GetComponent<AudioSource>();
       // _continueButton.enabled = false;

    }
    public void AnimateText(TMP_Text txt)
    {
        _text = txt;
        _charCount = _text.GetTextInfo(_text.text).characterCount;
        _visibleChar = 0;
        StartCoroutine(TextAnimation());
        _continueButton.enabled = true;

    }

    public void ShowFullSentence()
    {
        if (_visibleChar < _charCount)
        {
            StopAllCoroutines();
            _text.maxVisibleCharacters = _charCount;
            _visibleChar = _charCount;
  
        }
    }

    IEnumerator TextAnimation()
    {

        while (_visibleChar < _charCount)
        {
            _visibleChar++;
            _text.maxVisibleCharacters = _visibleChar;
            if (!_myAS.isPlaying)
            {
                _myAS.PlayOneShot(_txtAudioClip);
            }
            
            yield return null;

        }

    }
}
