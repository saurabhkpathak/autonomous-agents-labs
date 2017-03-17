using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileSprite {

	public int movementCost;
	public Tiles tileType;
	public GameObject tilePrefab;
    public bool isWalkable = true;

	public TileSprite() {
		this.movementCost = 0;
		this.tileType = Tiles.Plains;
	}

	public TileSprite(int cost, Tiles type) {
		this.movementCost = cost;
		this.tileType = type;
	}
}
