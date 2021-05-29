using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private int _index = 0;
    private ConversationManager _myCM = null;
    [SerializeField] private DialogueSO[] _dialogues;
    [SerializeField] private MoveToLocation _playerMTL = null;

    private void Start()
    {
        _myCM = GetComponent<ConversationManager>();
        _index = 0;
    }

    public void StartNewDialogue()
    {
        if(_index < _dialogues.Length)
        {
            _myCM.MyDSO = _dialogues[_index];


            if(GameManager.Instance.GameStarted)
            {
                GameManager.Instance.ResumeControls(false);
                _playerMTL.TravelToCenter(StartDialogue);
            }
            else
            {
                StartDialogue();   
            }
            _index++;
        }
        else
        {
            return;
        }
        
    }

    public void StartDialogue()
    {
        _myCM.StartConversation();
    }
   
}
