using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public enum Speaker
    {
        Player,
        NPC
    }
    [SerializeField] private Speaker _currentSpeaker;
    [SerializeField] private TMP_Text _dialogueText = null;
    [SerializeField] private TMP_Text _npcName = null;
    [SerializeField] private Image _npcImg = null;
    [SerializeField] private Image _playerImg = null;
    [SerializeField] private Animator _npcImgAN = null;
    private int _index = 0;
    private DialogueSO _myDSO = null;
    public DialogueSO MyDSO
    {
        get { return _myDSO; }
        set { _myDSO = value; }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        //set starting dialogueSO
        //start first dialogue Coroutine
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
