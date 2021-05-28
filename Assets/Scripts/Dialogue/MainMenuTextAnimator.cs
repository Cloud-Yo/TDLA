using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuTextAnimator : MonoBehaviour
{
    [SerializeField] private TMP_Text _text = null;
    [SerializeField] private float _delay = 0.025f;
    [SerializeField] private WaitForSeconds _waitDelay;
void Start()
    {
        _waitDelay = new WaitForSeconds(_delay);

        _text.maxVisibleCharacters = 0;
    }

    public void AnimateText(TMP_Text txt = null)
    {
        _waitDelay = new WaitForSeconds(_delay);
        if (txt != null)
        {
            StartCoroutine(TextAnimation(txt));
        }
        else
        {
            StartCoroutine(TextAnimation(_text));
        }
    }

    IEnumerator TextAnimation(TMP_Text txt)
    {
        int charCount = txt.textInfo.characterCount;
        int visibleChar = 0;

        while (visibleChar < charCount)
        {
            visibleChar++;
            txt.maxVisibleCharacters = visibleChar;
            if(_waitDelay != null)
            {
                yield return _waitDelay;
            }
            else
            {
                yield return null;
            }

        }
    }
}
