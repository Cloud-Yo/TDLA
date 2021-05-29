using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private DialogueManager _myDM = null;
    public void OnPanelShow()
    {
        _myDM.StartNewDialogue();
    }
}
