using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform[] scoreContainer;
    private Transform[] timeContainer;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timeText;
    void Start()
    {
        scoreContainer = GameObject.FindGameObjectWithTag("ScoreUI").GetComponentsInChildren<Transform>();
        timeContainer = GameObject.FindGameObjectWithTag("TimeUI").GetComponentsInChildren<Transform>();
        scoreText = scoreContainer[2].gameObject.GetComponent<TextMeshProUGUI>();
        timeText = timeContainer[2].gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = StatsManager.Instance.actualScore.ToString();
        DisplayTime();
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(StatsManager.Instance.time / 60);
        float seconds = Mathf.FloorToInt(StatsManager.Instance.time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
