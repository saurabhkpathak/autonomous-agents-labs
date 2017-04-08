using UnityEngine;
using System.Collections.Generic;
using System;

public class Node {
    public List<Node> neighbours;
    public int x;
	public int y;

    public float gCost;
    public float hCost;
    public Node parent;

    public float getHCost(Node targetNode)
    {
        float dx = Mathf.Abs(x - targetNode.x);
        float dy = Mathf.Abs(y - targetNode.y);
        hCost = (dx + dy) + (1.4f - 2.0f) * Mathf.Min(dx, dy);
        return hCost;
    }

    public float getGCost(Node targetNode, TilingSystem tileMap)
    {
        if(parent == null)
        {
            gCost = 0;
        } else
        {
            gCost = parent.gCost + tileMap.TileSprites[tileMap.tiles[x, y]].movementCost * (Math.Abs(parent.x - x) + Math.Abs(parent.y - y));
        }
        return gCost;
    }

    public float getFCost(Node targetNode)
    {
        hCost = getHCost(targetNode);
        fCost = hCost + gCost;
        return fCost;
    }

    public Node()
    {
        neighbours = new List<Node>();
    }

    //public Node(TileSprite tile, Vector2 targetPosition, Node parent, AStar.SearchType type)
    //{
    //    this.parent = parent;

    //    if (this.parent == null)
    //        this.gCost = 0;
    //    else if (type == AStar.SearchType.ShortestPath)
    //        this.gCost = this.parent.gCost + tile.movementCost * (Math.Abs(this.parent.x - tile.Position.x) + Math.Abs(this.parent.y - tile.Position.y));
    //    else if (type == AStar.SearchType.SensePropogation)
    //        this.gCost = this.parent.gCost + tile.TileAttenuation;

    //    hCost = Math.Abs(targetPosition.x - tile.Position.x) + Math.Abs(targetPosition.y - tile.Position.y);
    //}

    private float fCost;
    public float FCost
    {
        get { return fCost; }
        set { fCost = value; }
    }

 //   public float DistanceTo(Node n) {
	//	return Vector2.Distance (
	//		new Vector2 (x, y),
	//		new Vector2 (n.x, n.y)
	//	);
	//}

    public float getDistance(Node nodeA)
    {
        float dstX = Mathf.Abs(x - nodeA.x);
        float dstY = Math.Abs(y - nodeA.y);

        if(dstX > dstY)
        {
            return 1.4f * dstX + (dstX - dstY);
        } else
        {
            return 1.4f * dstY + (dstY - dstX);
        }
    }
}