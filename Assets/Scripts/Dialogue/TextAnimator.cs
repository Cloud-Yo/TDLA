using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    private AudioSource _myAS = null;
    [SerializeField] private AudioClip _txtAudioClip = null;
    //disable during animation and enable at end.
    [SerializeField] private Button _continueButton = null;
    private TMP_Text _text = null;
    private int _visibleChar = 0;
    public int VisibleChar { get { return _visibleChar; } }
    private int _charCount;
    public int CharCount { get { return _charCount; } }


    void Start()
    {
        _myAS = gameObject.GetComponent<AudioSource>();
        //Disable Continue Button

    }
    public void AnimateText(TMP_Text txt)
    {
        _text = txt;
        _charCount = _text.GetTextInfo(_text.text).characterCount;
        _visibleChar = 0;
        //disable Continue Button
        StartCoroutine(TextAnimation());

    }

    public void ShowFullSentence()
    {
        if (_visibleChar < _charCount)
        {
            StopAllCoroutines();
            _text.maxVisibleCharacters = _charCount;
            _visibleChar = _charCount;
            //enable Continue Button
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
        //enable Continue Button
    }
}
