using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TilingSystem : MonoBehaviour {

    int[,] tiles;
	public TileSprite[] TileSprites;
    public int MapSizeX, MapSizeY;
	private TileSprite[,] _map;
    public ArrayList locationsList = new ArrayList();

	public Node[,] grid;

	public GameObject selectedUnit;

	void GeneratePathfindingGraph() {
		grid = new Node[MapSizeX, MapSizeY];

		for (int i = 0; i < MapSizeX; i++) {
			for (int j = 0; j < MapSizeY; j++) {
				grid [i, j] = new Node ();
				grid [i, j].x = i;
				grid [i, j].y = j;
			}
		}

		for (int x = 0; x < MapSizeX; x++) {
			for (int y = 0; y < MapSizeY; y++) {
                if (x > 0)
                {
                    grid[x, y].neighbours.Add(grid[x - 1, y]);
                    if (y > 0)
                        grid[x, y].neighbours.Add(grid[x - 1, y - 1]);
                    if (y < MapSizeY - 1)
                        grid[x, y].neighbours.Add(grid[x - 1, y + 1]);
                }

                // Try Right
                if (x < MapSizeX - 1)
                {
                    grid[x, y].neighbours.Add(grid[x + 1, y]);
                    if (y > 0)
                        grid[x, y].neighbours.Add(grid[x + 1, y - 1]);
                    if (y < MapSizeY - 1)
                        grid[x, y].neighbours.Add(grid[x + 1, y + 1]);
                }

                // Try straight up and down
                if (y > 0)
                    grid[x, y].neighbours.Add(grid[x, y - 1]);
                if (y < MapSizeY - 1)
                    grid[x, y].neighbours.Add(grid[x, y + 1]);
            }
		}
	}

	public float getTileSize() {
		return TileSprites[0].tilePrefab.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
	}

	private TileSprite getTile(Tiles tile) {
		foreach(TileSprite tileSprite in TileSprites) {
			if (tileSprite.tileType == tile)
				return tileSprite;
		}
		return TileSprites [0];
	}

	public Vector3 getTilePositionByType(Tiles tile) {
		Vector3 coordinates = new Vector3 ();

		for (int i = 0; i < MapSizeX; i++) {
			for (int j = 0; j < MapSizeY; j++) {
				if (TileSprites [tiles [i, j]].tileType == tile) {
					coordinates.x = i;
					coordinates.y = j;
					break;
				}
			}
		}
		return coordinates;
	}

	private void DefaultTiles() {
		tiles = new int[MapSizeX, MapSizeY];
		for(int x = 0; x < MapSizeX; x++)
		{
			for(int y = 0; y < MapSizeY; y++)
			{
				tiles[x, y] = 5;
			}
		}
	}

	//set the tiles of the map to what is specified in TileSprites
	private void SetTiles() {
		tiles[4, 4] = 3;
		tiles[5, 4] = 3;
		tiles[6, 4] = 3;
		tiles[7, 4] = 3;
		tiles[8, 4] = 3;

		tiles[4, 5] = 3;
		tiles[4, 4] = 3;
		tiles[8, 5] = 3;
		tiles[8, 4] = 3;
		tiles[12, 5] = 3;
		tiles[9, 7] = 3;
		tiles[4, 2] = 3;
		tiles[10, 7] = 3;
		tiles[11, 7] = 3;
		tiles[12, 7] = 3;
		tiles[12, 6] = 3;
		tiles[2, 7] = 3;
		tiles[6, 3] = 3;

		// bank
		tiles[9, 4] = 0;
        locationsList.Add(new Vector2(9, 4));
		//shack
		tiles[3, 4] = 7;
        locationsList.Add(new Vector2(3, 4));
        //gold mine
        tiles[1, 8] = 2;
        locationsList.Add(new Vector2(1, 8));
        //saloon
        tiles[10, 9] = 6;
        locationsList.Add(new Vector2(10, 9));
        //cemetery
        tiles[14, 2] = 1;
        locationsList.Add(new Vector2(14, 2));
        //outlaw camp
        tiles[7, 0] = 4;
        locationsList.Add(new Vector2(7, 0));
		//sheriff's office
		tiles[5, 7] = 8;
		locationsList.Add(new Vector2(3, 4));
		//undertakers
		tiles[9, 1] = 9;
		locationsList.Add(new Vector2(9, 1));
    }

	private void PlaceTile(int x, int y) {
		TileSprite tt = TileSprites[tiles[x, y]];
		GameObject go = Instantiate(tt.tilePrefab, new Vector3(x, y, 0), Quaternion.identity);

		//if(tt.tileType == Tiles.Plains)
		//{
		//	clickHandler ch = go.GetComponent<clickHandler>();
		//	ch.tileX = x;
		//	ch.tileY = y;
		//	ch.map = this;
		//}
	}

	private void AddTilesToMap() {
		for (int x = 0; x < MapSizeX; x++)
		{
			for (int y = 0; y < MapSizeY; y++)
			{
				PlaceTile (x, y);
			}
		}
	}

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

	public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY) {

		TileSprite tt = TileSprites [tiles [targetX, targetY]];

		if(UnitCanEnterTile(targetX, targetY) == false)
			return Mathf.Infinity;

		float cost = tt.movementCost;

		if( sourceX!=targetX && sourceY!=targetY) {
			// We are moving diagonally!  Fudge the cost for tie-breaking
			// Purely a cosmetic thing!
			cost += 0.001f;
		}

		return cost;

	}

	public bool UnitCanEnterTile(int x, int y) {

		// We could test the unit's walk/hover/fly type against various
		// terrain flags here to see if they are allowed to enter the tile.

		return TileSprites [tiles [x, y]].isWalkable;
	}

    public void Start() {
        DefaultTiles ();
		SetTiles ();
        GeneratePathfindingGraph();
		AddTilesToMap ();

        GameObject Bob = GameObject.Find("Bob");
        GameObject Elsa = GameObject.Find("Elsa");
        GameObject Jesse = GameObject.Find("Jesse");
        GameObject Wyatt = GameObject.Find("Wyatt");
        GameObject Undertaker = GameObject.Find("Undertaker");

        AgentManager.AddAgent(Bob.GetComponent<Miner>());
        AgentManager.AddAgent(Elsa.GetComponent<MinersWife>());
        AgentManager.AddAgent(Jesse.GetComponent<Outlaw>());
        AgentManager.AddAgent(Wyatt.GetComponent<Sheriff>());
        AgentManager.AddAgent(Undertaker.GetComponent<Undertaker>());
    }

}
