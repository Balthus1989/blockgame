using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    TextMeshProUGUI highestScoreText;
    TextMeshProUGUI actualScoreText;
    private void Start()
    {
        highestScoreText = GameObject.FindGameObjectWithTag("HighestScore").GetComponentsInChildren<TextMeshProUGUI>()[1];
        highestScoreText.text = StatsManager.Instance.highestScore.ToString();

        actualScoreText = GameObject.FindGameObjectWithTag("ActualScore").GetComponentsInChildren<TextMeshProUGUI>()[1];
        actualScoreText.text = StatsManager.Instance.actualScore.ToString();
    }

    public void LoadHome()
    {
        Debug.Log("Home");
        SceneManager.LoadScene(0);
    }

    public void LoadNewGame()
    {
        Debug.Log("New Game");
        SceneManager.LoadScene(1);
    }

}
