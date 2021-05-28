using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    private AudioSource _myAS = null;
    [SerializeField] private AudioClip _txtAudioClip = null;

    void Start()
    {
        _myAS = gameObject.GetComponent<AudioSource>();
        
    }
   public void AnimateText(TMP_Text txt)
    {
        
        
        StartCoroutine(TextAnimation(txt));

    }

    IEnumerator TextAnimation(TMP_Text txt)
    {
        int charCount = txt.textInfo.characterCount;
        int visibleChar = 0;

        while (visibleChar < charCount)
        {
            visibleChar++;
            txt.maxVisibleCharacters = visibleChar;
            if (!_myAS.isPlaying)
            {
                _myAS.PlayOneShot(_txtAudioClip);
            }
            
            yield return null;

        }
    }
}
