using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class GridMap : MonoBehaviour
{
    public int xSize;
    public int ySize;
    float offset;

    public GameObject[,] grid;
    public GameObject mapHolder;
    public Vector2 gridSize;
    public bool checkStatus;
    public float width;

    private int SECURITY_VALUE = 100;
    public void InitMap(int x, int y, float off)
    {
        xSize = x;
        ySize = y;
        offset = off;
        mapHolder = GameObject.FindGameObjectWithTag("Map");

        grid = new GameObject[xSize, ySize];
        width = (float)((float) Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height);
        checkStatus = false;

        MapMethod();

    }

    public void MapMethod()
    {
        int checkCounter = 0;
        do
        {
            GenerateMap();
            CheckMap();
            checkCounter += 1;
            Debug.Log("Checking map coherence");
        } while (!checkStatus && checkCounter < SECURITY_VALUE);

    }

    public void GenerateMap()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                if (grid[i, j] == null)
                {
                    SpawnBlocks(i, j);
                }
            }
        }
    }

    public void ResetVisits()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                if (grid[i, j] != null)
                {
                    if (grid[i, j].GetComponent<Block>().visitedBlock)
                        grid[i, j].GetComponent<Block>().visitedBlock = false;
                }

            }
        }

    }

    public void SpawnBlocks(int x, int y)
    {
        grid[x, y] = new GameObject();
        grid[x, y].transform.parent = mapHolder.transform;
        grid[x, y].transform.position = new Vector2((x * offset) - xSize / 2f, (y * offset) - ySize / 2f);

        int prob = Random.Range(0, 100);
        switch (prob)
        {
            case <= 95:
                grid[x, y].AddComponent<ColorBlock>();
                break;
            case > 95:
                int special = Random.Range(0, 2);
                switch(special)
                {
                    case 0:
                        grid[x, y].AddComponent<BombBlock>();
                        break;
                    case 1:
                        grid[x, y].AddComponent<StripeBlock>();
                        break;
                    default:
                        grid[x, y].AddComponent<ColorBlock>();
                        break;
                }
                break;
            default:
        }

        Block blockComponent = grid[x, y].GetComponent<Block>();
        blockComponent.InitBlock(x, y);
        blockComponent.LoadTexture();
        
    }

    public void CheckMap()
    {
        bool colorsContiguity = false;
        bool specialBlockPresence = false;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].GetComponent<ColorBlock>())
                    {
                        ColorBlock colorBlock = grid[x, y].GetComponent<ColorBlock>();

                        if (x + 1 < xSize)
                        {
                            colorsContiguity = colorsContiguity || CheckContiguousBlock(colorBlock, x + 1, y);
                        }

                        if (x - 1 >= 0)
                        {
                            colorsContiguity = colorsContiguity || CheckContiguousBlock(colorBlock, x - 1, y);
                        }

                        if (y + 1 < ySize)
                        {
                            colorsContiguity = colorsContiguity || CheckContiguousBlock(colorBlock, x, y + 1);
                        }

                        if (y - 1 >= 0)
                        {
                            colorsContiguity = colorsContiguity || CheckContiguousBlock(colorBlock, x, y - 1);
                        }
                    }

                    if (grid[x, y].GetComponent<BombBlock>() || grid[x, y].GetComponent<StripeBlock>())
                    {
                        specialBlockPresence = specialBlockPresence || true;
                    }
                }
            }
        }

        checkStatus =  colorsContiguity && specialBlockPresence;
    }

    bool CheckContiguousBlock(ColorBlock blockToCheck, int x, int y)
    {
        if (grid[x, y].GetComponent<ColorBlock>())
        {
            ColorBlock contiguousBlock = grid[x, y].GetComponent<ColorBlock>();
            if (contiguousBlock.rend.color == blockToCheck.rend.color)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }

    private void Update()
    {
        GenerateMap();
        CheckMap();
    }
}