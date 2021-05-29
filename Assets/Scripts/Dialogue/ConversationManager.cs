using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ConversationManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _dialogueText = null;
    [SerializeField] private TMP_Text _npcName = null;
    [SerializeField] private Image _npcImg = null;
    [SerializeField] private Image _playerImg = null;
    [SerializeField] private Animator _panelAN = null;
    [SerializeField] private Color _shadowColor;
    [SerializeField] private AnimatePortraitShader[] _portraitShaders;
    private TextAnimator _myTA = null;
    private int _index = 0;
    [SerializeField] private DialogueSO _myDSO = null;
    
    public DialogueSO MyDSO
    {
        get { return _myDSO; }
        set { _myDSO = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _myTA = GetComponent<TextAnimator>();
        _dialogueText.maxVisibleCharacters = 0;

    }
    
    public void StartConversation()
    {
        _index = 0;
        _npcImg.sprite = _myDSO.LeftSprite;
        _playerImg.sprite = _myDSO.RightSprite;
        if (_index == 0)
        {
            StartCoroutine(AnimatePortraits());  
        }



    }
    public void ContinueToNextSentence()
    {
        if(_myTA.VisibleChar < _myTA.CharCount)
        {
            _myTA.ShowFullSentence();
            return;
        }
        _index++;
        if (_index < _myDSO.Sentences.Length)
        {
            
            StartSentence();

        }
        else
        {
            //conversation over, close panel.
            _panelAN.SetBool("Appear", false);
            if(!GameManager.Instance.GameStarted)
            {
                GameManager.Instance.StartMoving();
            }
            else
            {
                GameManager.Instance.ResumeControls(true);
            }
        }
    }

    private void StartSentence()
    {
        if(_myDSO.Sentences[_index].SpeakerSide == Sentence.Side.left)
        {
            _npcName.color = _myDSO.Sentences[_index].SentenceColor;
            _npcName.SetText(_myDSO.Sentences[_index].Name);
        }

        _dialogueText.SetText(_myDSO.Sentences[_index].DialogueSentence);
        _dialogueText.color = _myDSO.Sentences[_index].SentenceColor;
        switch (_myDSO.Sentences[_index].SpeakerSide)
        {
            case Sentence.Side.left:
                //Light Up Left side and obscure right side
                _npcImg.color = Color.white;
                _playerImg.color = _shadowColor;
                break;
            case Sentence.Side.right:
                //Light Up right side and obscure left side
                _playerImg.color = Color.white;
                _npcImg.color = _shadowColor;
                break;
            default:
                break;
        }
        _myTA.AnimateText(_dialogueText);
    }
    
    IEnumerator AnimatePortraits()
    {
        foreach (var p in _portraitShaders)
        {
            p.StartImageAnimation();
            yield return null;
        }
        yield return new WaitForSeconds(1.25f);
        StartSentence();
    }
}
