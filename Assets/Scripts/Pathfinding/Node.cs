using UnityEngine;
using System.Collections.Generic;

public class Node {
	public  List<Node> neighbours;
	public int x;
	public int y;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node () {
		neighbours = new List<Node>();
	}

	public float DistanceTo(Node n) {
		return Vector2.Distance (
			new Vector2 (x, y),
			new Vector2 (n.x, n.y)
		);
	}
}