using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlocksManager : MonoBehaviour
{
    public int xSize = 3;
    public int ySize = 4;
    public float offset = 1.2f;
    public GameObject gameOverPanel;
    GridMap map;
    void Start()
    {
        map = gameObject.AddComponent<GridMap>();
        map.InitMap(xSize, ySize, offset);
        StatsManager.Instance.actualScore = 0.0f;
        StatsManager.Instance.time = 120.0f;
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOverCondition();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform != null)
            {
                for (int x = 0; x < xSize; x++)
                {
                    for (int y = 0; y < ySize; y++)
                    {
                        if (map.grid[x, y] != null && map.grid[x, y] == hit.transform.gameObject)
                        {
                            hit.transform.gameObject.GetComponent<Block>().DestroyNeighbors(map, map.grid[x, y].GetComponent<Block>().rend.color, x, y);
                            UpdateStats();
                            map.MapMethod();
                        }
                    }
                }
            }
        }
    }

    void UpdateStats()
    {
        StatsManager.Instance.UpdateScore();
        StatsManager.Instance.UpdateTime();
        StatsManager.Instance.ResetBlocksNumber();
    }

    void CheckGameOverCondition()
    {
        if (!map.checkStatus || StatsManager.Instance.time <= 0)
        {
            StartCoroutine(InvokeGameOver());
        }

    }

    IEnumerator InvokeGameOver()
    {
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        Debug.Log("GAME OVER");
        SceneManager.LoadScene(2);
    }

}
