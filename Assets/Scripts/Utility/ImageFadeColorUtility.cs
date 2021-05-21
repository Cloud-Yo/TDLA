using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeColorUtility : MonoBehaviour
{



    [SerializeField]
    private Image _myImg = null;

    [SerializeField]
    private Color _targetCol;

    [SerializeField]
    private float _spd = 2f;

    void Awake()
    {
        _myImg = GetComponent<Image>();
    }

   public void FadeColor()
    {

       StartCoroutine(ColorFade());
    }

    IEnumerator ColorFade()
    {
        while (_myImg.color != _targetCol)
        {
            _myImg.color = Color.Lerp(_myImg.color, _targetCol, _spd * Time.deltaTime);
            yield return null;
        }
    }
}
