using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private DialogueManager _myDM = null;
    [SerializeField] private Button _continueButton;

    public void OnPanelShow()
    {
        _continueButton.enabled = false;
    }

    public void PanelCompletedAnimation()
    {
        _myDM.StartNewDialogue();
    }
}
