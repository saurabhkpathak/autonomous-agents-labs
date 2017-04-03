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

		for (int i = 0; i < MapSizeX; i++) {
			for (int j = 0; j < MapSizeY; j++) {
				if (i > 0)
					grid [i, j].neighbours.Add (grid [i - 1, j]);
				if (i < MapSizeX - 1)
					grid [i, j].neighbours.Add (grid [i + 1, j]);
				if (j > 0)
					grid [i, j].neighbours.Add (grid [i, j - 1]);
				if (j < MapSizeY - 1)
					grid [i, j].neighbours.Add (grid [i, j + 1]);
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
    }

	private void PlaceTile(int x, int y) {
		TileSprite tt = TileSprites[tiles[x, y]];
		GameObject go = Instantiate(tt.tilePrefab, new Vector3(x, y, 0), Quaternion.identity);

		if(tt.tileType == Tiles.Plains)
		{
			clickHandler ch = go.GetComponent<clickHandler>();
			ch.tileX = x;
			ch.tileY = y;
			ch.map = this;
		}
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

    public void MoveUnitTo(int x, int y) {
		//selectedUnit.transform.position = new Vector3 (x, y, 0);
        selectedUnit.GetComponent<Unit>().currentPath = GeneratePathTo(x, y);
    }

	public List<Node> GeneratePathTo(int x, int y) {
		selectedUnit.GetComponent<Unit> ().currentPath = null;

		if( UnitCanEnterTile(x,y) == false ) {
			return null;
		}

		Dictionary<Node, float> dist = new Dictionary<Node, float> ();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node > ();

		List<Node> unvisited = new List<Node> ();

		Node source = grid [selectedUnit.GetComponent<Unit> ().tileX, selectedUnit.GetComponent<Unit> ().tileY];
		Node target = grid [x, y];
		
		dist [source] = 0;
		prev [source] = null;

		foreach (Node v in grid) {
			if (v != source) {
				dist [v] = Mathf.Infinity;
				prev [v] = null;

			}
			unvisited.Add (v);
		}

		while (unvisited.Count > 0) {
			Node u = null;

			foreach (Node possibleU in unvisited) {
				if (u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}

			if (u == target) {
				break;
			}

			unvisited.Remove (u);

			foreach (Node v in u.neighbours) {
				//float alt = dist [u] + u.DistanceTo (v);
				float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
				if (alt < dist [v]) {
					dist [v] = alt;
					prev [v] = u;
				}
			}
		}
		if (prev [target] == null) {
			return null;
		}
		List<Node> currentPath = new List<Node> ();
		Node curr = target;

		while (curr != null) {
			currentPath.Add (curr);
			curr = prev [curr];
		}

		currentPath.Reverse ();

        return currentPath;
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
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;

		DefaultTiles ();
		SetTiles ();
        GeneratePathfindingGraph();
		AddTilesToMap ();
    }

}
