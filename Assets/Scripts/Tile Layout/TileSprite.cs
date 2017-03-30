using UnityEngine;

[System.Serializable]
public class TileSprite {

	public int movementCost;
	public Tiles tileType;
	public GameObject tilePrefab;
    public bool isWalkable = true;

	public TileSprite() {
        movementCost = 1;
        tileType = Tiles.Plains;
	}

	public TileSprite(int cost, Tiles type) {
        movementCost = cost;
        tileType = type;
	}
}
