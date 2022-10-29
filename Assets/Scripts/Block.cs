using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Block: MonoBehaviour
{
    public int xPos;
    public int yPos;
    protected float minProbability;
    protected float maxProbability;

    // Sprite components
    protected Texture2D tex;
    protected Sprite sprite;
    public SpriteRenderer rend;
    protected BoxCollider2D coll2D;

    public bool visitedBlock = false;

    public virtual void InitBlock(int x, int y) 
    {
        xPos = x;
        yPos = y;
        rend = gameObject.AddComponent<SpriteRenderer>();
        coll2D = gameObject.AddComponent<BoxCollider2D>();
        coll2D.size = new Vector2(1f, 1f);
    }

    public abstract void LoadTexture();
    public virtual void DestroyNeighbors(GridMap map, Color theColor, int x, int y) { }

    public void UpdateDestroyedBlocksNumbber()
    {
        StatsManager.Instance.destroyedBlocks += 1;
    }
}
