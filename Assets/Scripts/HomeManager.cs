using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    TextMeshProUGUI highestScoreText;
    private void Start()
    {
        highestScoreText = GameObject.FindGameObjectWithTag("HighestScore").GetComponentsInChildren<TextMeshProUGUI>()[1];
        highestScoreText.text = StatsManager.Instance.highestScore.ToString();
    }

    public void LoadGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene(1);
        
    }
}
