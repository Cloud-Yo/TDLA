using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _startText;
    [SerializeField] private ImageFadeColorUtility _myIFCU = null;
    void Start()
    {
        _myIFCU?.FadeColor();
        StartCoroutine(BlinkText());
    }

    public void LoadScene(int index)
    {
        // 0 = Main Menu 1 = Main Game
        SceneManager.LoadScene(index);
    }


    public void QuitApplication()
    {
        Application.Quit();
    }

    IEnumerator BlinkText()
    {
        Color col = Color.white;
        while (SceneManager.GetActiveScene().name == "MainMenu")
        {
            col.a = Mathf.PingPong(Time.time, 1f);
            _startText.color = col;
            yield return null;
        }
    }
}
