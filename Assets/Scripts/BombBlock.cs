using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public class BombBlock : Block
{
    public override void InitBlock(int x, int y)
    {
        base.InitBlock(x, y);
        minProbability = 95f;
        maxProbability = 100f;

        gameObject.name = "Bomb Block";
        gameObject.tag = "BombBlock";
    }

    public override void LoadTexture()
    {
        tex = Resources.Load<Texture2D>("Sprites/bomb");
        sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        rend.sprite = sprite;
    }

    public override void DestroyNeighbors(GridMap map, UnityEngine.Color theColor, int x, int y)
    {

        if (x + 1 < map.xSize)
        {
            if (y + 1 < map.ySize)
            {
                Destroy(map.grid[x + 1, y + 1]);
                UpdateDestroyedBlocksNumbber();
            }

            if (y - 1 >= 0)
            {
                Destroy(map.grid[x + 1, y - 1]);
                UpdateDestroyedBlocksNumbber();
            }

            Destroy(map.grid[x + 1, y]);
            UpdateDestroyedBlocksNumbber();
        }

        if (x - 1 >= 0)
        {
            if (y - 1 >= 0)
            {
                Destroy(map.grid[x - 1, y - 1]);
                UpdateDestroyedBlocksNumbber();
            }

            if (y + 1 < map.ySize)
            {
                Destroy(map.grid[x - 1, y + 1]);
                UpdateDestroyedBlocksNumbber();
            }

            Destroy(map.grid[x - 1, y]);
            UpdateDestroyedBlocksNumbber();
        }

        if (y - 1 >= 0)
        {
            Destroy(map.grid[x, y - 1]);
            UpdateDestroyedBlocksNumbber();
        }

        if (y + 1 < map.ySize)
        {
            Destroy(map.grid[x, y + 1]);
            UpdateDestroyedBlocksNumbber();
        }
        
        Destroy (map.grid[x, y]);
        UpdateDestroyedBlocksNumbber();
    }
}
