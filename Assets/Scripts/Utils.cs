using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public float computeScore(int numberOfBlocks)
    {
        return (numberOfBlocks - 1) * 80 + Mathf.Pow((numberOfBlocks - 2)/5, 2);
    }

    public int computeTime(int numberOfBlocks)
    {
        return Mathf.FloorToInt(10 + Mathf.Pow((numberOfBlocks - 2) / 3, 2) * 20);
    }
}
