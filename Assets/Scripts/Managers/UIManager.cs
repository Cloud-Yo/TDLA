using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text _gameOverTxt;
    [SerializeField] private Text _restartTxt;
    [SerializeField] private bool _gameIsOver = false;
    [SerializeField] private float _blinkRate = 0.5f;
    [SerializeField] private TMP_Text _enKillTxt = null;
    private int _enKill = 0;
    private int _enMiss = 0;
    [SerializeField] private TMP_Text _enMissTxt = null;


    // Start is called before the first frame update
    void Start()
    {
        _enKill = 0;
        _enKillTxt.SetText("000");
        _enMiss = 0;
        _enMissTxt.SetText("000");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        _gameIsOver = true;
        _restartTxt.enabled = true;
        StartCoroutine(GameOverBlink());
    }

    IEnumerator GameOverBlink()
    {
        while(_gameIsOver)
        {
            _gameOverTxt.enabled = true;
            yield return new WaitForSeconds(_blinkRate);
            _gameOverTxt.enabled = false;
            yield return new WaitForSeconds(_blinkRate);
        }
    }

    public void SetInfoText(bool kill)
    {

        if (kill)
        {
            _enKill++;
            _enKillTxt.SetText(_enKill.ToString());
        }
        else if (!kill)
        {
            _enMiss++;
            _enMissTxt.SetText(_enMiss.ToString());
        }
    }

}
