using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialogue", menuName ="TDLA/RPG/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [TextArea(2, 5)]
    [SerializeField] private string[] _sentences;
    [SerializeField] private string _charName;
    [SerializeField] private Sprite _charPortrait;

}
