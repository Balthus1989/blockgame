using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripeBlock : Block
{
    public override void InitBlock(int x, int y)
    {
        base.InitBlock(x, y);
        minProbability = 95f;
        maxProbability = 100f;

        gameObject.name = "Stripe Block";
        gameObject.tag = "StripeBlock";
    }

    public override void LoadTexture()
    {
        tex = Resources.Load<Texture2D>("Sprites/stripe");
        sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        rend.sprite = sprite;
    }

    public override void DestroyNeighbors(GridMap map, Color theColor, int x, int y)
    {
        if (x > map.xSize || x < 0 || y > map.ySize || y < 0 || map.grid[x, y] == null)
            return;

        for (int row = 0; row < map.xSize; row++)
        {
            Destroy(map.grid[row, y]);
            UpdateDestroyedBlocksNumbber();
        }
    }
}
