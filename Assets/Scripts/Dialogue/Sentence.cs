using UnityEngine;

[System.Serializable]
public class Sentence 
{

    public enum Side
    {
        left,
        right
    }

    public string Name;
    public Side SpeakerSide;
    [TextArea(2, 5)]
    public string DialogueSentence;
    public Color SentenceColor;

}
