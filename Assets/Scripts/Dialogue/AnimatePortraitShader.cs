using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AnimatePortraitShader : MonoBehaviour
{
    [SerializeField] private Material _imgMat = null;
    [SerializeField] private float _spd = 2f;
    [SerializeField] private bool _isVisible = false;
    
 

    void Start()
    {
        _imgMat = GetComponent<Image>().material;
        if (!_isVisible)
        {
            _imgMat.SetVector("_RectangleSize", new Vector4(1, -0.1f, 0, 0));
        }
        else
        {
            _imgMat.SetVector("_RectangleSize", new Vector4(1, 1f, 0, 0));
        }
        
    }

    public void StartImageAnimation()
    {
        StartCoroutine(ImageMaterialAnimation());
    }

    IEnumerator ImageMaterialAnimation()
    {
        float y =_imgMat.GetVector("_RectangleSize").y == 1f ? 1f : -0.1f;

        if (y == -0.1f)
        {
            while (y < 1)
            {
                y += Time.deltaTime * _spd;
                _imgMat.SetVector("_RectangleSize", new Vector4(1, y, 0, 0));
                yield return null;
            }
            _imgMat.SetVector("_RectangleSize", new Vector4(1, 1, 0, 0));
            yield break;
        }
        else if (y == 1)
        {
            while (y > -0.1f)
            {
                y -= Time.deltaTime * _spd;
                _imgMat.SetVector("_RectangleSize", new Vector4(1, y, 0, 0));
                yield return null;
            }
            _imgMat.SetVector("_RectangleSize", new Vector4(1, -0.1f, 0, 0));
            yield break;
        }

    }
}
