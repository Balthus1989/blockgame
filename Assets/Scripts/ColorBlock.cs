using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Color = UnityEngine.Color;

public class ColorBlock : Block
{
    private Color[] colors = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        new Color(1.0f, 0.64f, 0.0f), // orange
        new Color(0.93f, 0.51f, 0.93f) // violet
    };

    public override void InitBlock(int x, int y)
    {
        base.InitBlock(x, y);
        minProbability = 0.0f;
        maxProbability = 95f;

        gameObject.name = "Color Block";
        gameObject.tag = "ColorBlock";

    }

    public override void LoadTexture()
    {
        // Loading texture for color block;
        tex = new Texture2D(100, 100);
        sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        rend.sprite = sprite;

        // Assigning color
        rend.color = colors[Random.Range(0, 6)];
    }

    public override void DestroyNeighbors(GridMap map, Color theColor, int x, int y)
    {
        // Am I within the boundaries of the map?
        if ((x < 0 || x > map.xSize - 1 || y < 0 || y > map.ySize - 1))
        {
            return;
        }
        else
        {
            // Am I checking an object that is null?
            if (map.grid[x, y] == null)
            {
                return;
            }
            else
            {
                // Was the block already visited? Is it a Color Block?
                Block blockComponent = map.grid[x, y].GetComponent<Block>();

                if (blockComponent.visitedBlock || blockComponent.GetComponent<ColorBlock>() == null)
                {
                    return;
                }
                else
                {
                    if (blockComponent.rend.color != theColor)
                    {
                        return;
                    }
                    else
                    {
                        List<GameObject> neighborhood = GetSimilarNeighbors(map, theColor, x, y);
                        if (neighborhood.Count > 0)
                        {
                            blockComponent.visitedBlock = true;

                            foreach(GameObject neighbor in neighborhood)
                            {
                                if (neighbor != null)
                                {
                                    Block blockNeighbor = neighbor.GetComponent<Block>();
                                    blockNeighbor.DestroyNeighbors(map, theColor, blockNeighbor.xPos, blockNeighbor.yPos);
                                }
                            }
                            Destroy(map.grid[x, y]);
                            UpdateDestroyedBlocksNumbber();
                            Debug.Log("Object destroyed at: (x: " + x + ", y: " + y + ")");
                        }
                    }
                }
            }
        }

    }

    private List<GameObject> GetSimilarNeighbors(GridMap map, Color color, int x, int y)
    {
        List<GameObject> neighborhood = new();

        GameObject east = GetNeighbor(x + 1, y, map, color);
        if (east != null)
            neighborhood.Add(east);

        GameObject west = GetNeighbor(x - 1, y, map, color);
        if (west != null)
            neighborhood.Add(west);

        GameObject north = GetNeighbor(x, y + 1, map, color);
        if (north != null)
            neighborhood.Add(north);

        GameObject south = GetNeighbor(x, y - 1, map, color);
        if (south != null)
            neighborhood.Add(south);

        return neighborhood;

    }

    private GameObject GetNeighbor(int x, int y, GridMap map, Color color)
    {
        if ((x >= 0 && x < map.xSize) && (y >= 0 && y < map.ySize))
        {
            if (map.grid[x, y] != null)
            {
                if (map.grid[x, y].GetComponent<Block>().rend.color == color)
                {
                    return map.grid[x, y];
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }
        else
            return null;
    }
}
