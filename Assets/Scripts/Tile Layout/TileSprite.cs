using UnityEngine;

[System.Serializable]
public class TileSprite {

	public int movementCost;
	public Tiles tileType;
	public GameObject tilePrefab;
    public bool isWalkable = true;

    private float tileAttenuation;
    public float TileAttenuation
    {
        get { return tileAttenuation; }
        set { tileAttenuation = value; }
    }

    private Vector2 tilePosition;
    public Vector2 Position
    {
        get { return tilePosition; }
        set { tilePosition = value; }
    }

    public TileSprite() {
        movementCost = 1;
        tileType = Tiles.Plains;
	}

	public TileSprite(int cost, Tiles type) {
        movementCost = cost;
        tileType = type;
	}
}
