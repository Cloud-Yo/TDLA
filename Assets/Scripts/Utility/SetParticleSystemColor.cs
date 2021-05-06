using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PSColor
{
    public ParticleSystem MYPS;
    public Color MYColor;

}

public class SetParticleSystemColor : MonoBehaviour
{
    [SerializeField] private PSColor[] _colorSets;
    public PSColor[] ColorSets { get { return _colorSets; } private set { _colorSets = value; } }

    public void ChangePSColor(int i)
    {
        var main = _colorSets[i].MYPS.main;
        main.startColor = _colorSets[i].MYColor;
    }

}
