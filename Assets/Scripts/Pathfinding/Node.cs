using UnityEngine;
using System.Collections.Generic;
using System;

public class Node {
	public  List<Node> neighbours;
	public int x;
	public int y;

    public float gCost;
    public float hCost;
    public Node parent;

    public Node () {
		neighbours = new List<Node>();
	}

    public Node(TileSprite tile, Vector2 targetPosition, Node parent, AStar.SearchType type)
    {
        this.parent = parent;

        if (this.parent == null)
            this.gCost = 0;
        else if (type == AStar.SearchType.ShortestPath)
            this.gCost = this.parent.gCost + tile.movementCost * (Math.Abs(this.parent.x - tile.Position.x) + Math.Abs(this.parent.y - tile.Position.y));
        else if (type == AStar.SearchType.SensePropogation)
            this.gCost = this.parent.gCost + tile.TileAttenuation;

        hCost = Math.Abs(targetPosition.x - tile.Position.x) + Math.Abs(targetPosition.y - tile.Position.y);
    }

    public float GetF()
    {
        return gCost + hCost;
    }

    public float DistanceTo(Node n) {
		return Vector2.Distance (
			new Vector2 (x, y),
			new Vector2 (n.x, n.y)
		);
	}
}