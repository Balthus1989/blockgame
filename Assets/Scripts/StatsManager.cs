using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StatsManager: MonoBehaviour
{
    public int destroyedBlocks;
    public float actualScore;
    public float highestScore;
    public float time;
    public bool countdown;

    public static StatsManager Instance;
    private readonly Utils utils = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        time -= Time.deltaTime;
    }

    public void UpdateScore()
    {
        actualScore += utils.computeScore(destroyedBlocks);
        
        if (actualScore > highestScore)
            highestScore = actualScore;

        Debug.Log("Actual score: " + actualScore);
        Debug.Log("Highest score: " + highestScore);
    }

    public void UpdateTime()
    {
        time += utils.computeTime(destroyedBlocks);
    }

    public void ResetBlocksNumber()
    {
        destroyedBlocks = 0;
    }
}
