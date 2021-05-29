using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "TDLA/RPG/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] private Sentence[] _sentences;

    public Sentence[] Sentences
    {
        get { return _sentences; }
        set { _sentences = value; }
    }

    [SerializeField] private Sprite _leftSprite;
    public Sprite LeftSprite { get { return _leftSprite; } set { _leftSprite = value; } }
    [SerializeField] private Sprite _rightSprite;
    public Sprite RightSprite { get { return _rightSprite; } set { _rightSprite = value; } }
}
