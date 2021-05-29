using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private ImageFadeColorUtility _myIFCU = null;
    [SerializeField] private Animator _dialoguePanelAN = null;
   
    void Start()
    {

        StartCoroutine(BeginGame());
    }


    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(1.5f);
        _myIFCU.FadeColor();
        yield return new WaitForSeconds(1.5f);
        _dialoguePanelAN.SetBool("Appear", true);
    }
}
