using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Lean; //from Unity asset "LeanPool" - freely available in the Asset Store; used here for object pooling

public class TilingSystem : MonoBehaviour {

    int[,] tiles;
	public TileSprite[] TileSprites;
    public int MapSizeX, MapSizeY;
	private TileSprite[,] _map;

	Node[,] graph;

	public GameObject selectedUnit;

    void GenerateMapData()
    {
        tiles = new int[MapSizeX, MapSizeY];
        for(int x = 0; x < MapSizeX; x++)
        {
            for(int y = 0; y < MapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }
        tiles[4, 4] = 1;
        tiles[5, 4] = 1;
        tiles[6, 4] = 1;
        tiles[7, 4] = 1;
        tiles[8, 4] = 1;

        tiles[4, 5] = 1;
        tiles[4, 4] = 1;
        tiles[8, 5] = 1;
        tiles[8, 4] = 1;
    }

	void GeneratePathfindingGraph() {
		graph = new Node[MapSizeX, MapSizeY];

		for (int i = 0; i < MapSizeX; i++) {
			for (int j = 0; j < MapSizeY; j++) {
				graph [i, j] = new Node ();
				graph [i, j].x = i;
				graph [i, j].y = j;
			}
		}

		for (int i = 0; i < MapSizeX; i++) {
			for (int j = 0; j < MapSizeY; j++) {
				if (i > 0)
					graph [i, j].neighbours.Add (graph [i - 1, j]);
				if (i < MapSizeX - 1)
					graph [i, j].neighbours.Add (graph [i + 1, j]);
				if (j > 0)
					graph [i, j].neighbours.Add (graph [i, j - 1]);
				if (j < MapSizeY - 1)
					graph [i, j].neighbours.Add (graph [i, j + 1]);
			}
		}
	}

	public float getTileSize() {
		return TileSprites[0].tilePrefab.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
	}

//	public Sprite DefaultImage;
//	public GameObject TileContainerPrefab;
//	public Vector2 CurrentPosition;
//	public Vector2 ViewPortSize;
//	private GameObject _tileContainer;
//	private List<GameObject> _tiles = new List<GameObject>();

	//create a map of size MapSize of unset tiles
	private void DefaultTiles() {
		for (int i = 0; i < MapSizeX; i++) {
			for (int j = 0; j < MapSizeY; j++) {
				_map [i,j] = new TileSprite ();
			}
		}
	}

	private TileSprite getTile(Tiles tile) {
		foreach(TileSprite tileSprite in TileSprites) {
			if (tileSprite.tileType == tile)
				return tileSprite;
		}
		return TileSprites [0];
	}

	//set the tiles of the map to what is specified in TileSprites
	private void SetTiles() {
        _map[0, 1] = new TileSprite(0, Tiles.Mountains);
        _map[0, 4] = new TileSprite(0, Tiles.Mountains);
        _map[1, 2] = new TileSprite(0, Tiles.Mountains);
        _map[1, 1] = new TileSprite(0, Tiles.Mountains);
        _map[2, 5] = new TileSprite(0, Tiles.Mountains);
        _map[2, 1] = new TileSprite(0, Tiles.Mountains);
        _map[3, 3] = new TileSprite(0, Tiles.Mountains);
        _map[3, 5] = new TileSprite(0, Tiles.Mountains);
        _map[4, 1] = new TileSprite(0, Tiles.Mountains);
        _map[4, 4] = new TileSprite(0, Tiles.Mountains);
    }

	private void PlaceTile(TileSprite tileSprite, int x, int y, Vector3 gridStart) {
		GameObject newTile = (GameObject)Instantiate (getTile (tileSprite.tileType).tilePrefab);
		newTile.transform.position = new Vector3 (gridStart.x + (getTileSize() * x), gridStart.y - (getTileSize() * y), 0);
		if (tileSprite.tileType == Tiles.Plains) {
			clickHandler ch = newTile.GetComponent<clickHandler> ();
            //ch.tileX = (int)(gridStart.x + (getTileSize () * x));
            //ch.tileY = (int)(gridStart.y - (getTileSize () * y));
            ch.tileX = x;
            ch.tileY = y;
            ch.map = this;
		}
	}

	private void AddTilesToMap() {
		Vector3 gridStart = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));

		for (int y = 0; y < MapSizeY; y++) {
			for (int x = 0; x < MapSizeX; x++) {
				PlaceTile (_map[x, y], x, y, gridStart);
			}
		}
	}

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public void MoveUnitTo(int x, int y) {
		selectedUnit.transform.position = new Vector3 (x, y, 0);
	}

	public void GeneratePathTo(int x, int y) {
		selectedUnit.GetComponent<Unit> ().currentPath = null;

		Dictionary<Node, float> dist = new Dictionary<Node, float> ();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node > ();

		List<Node> unvisited = new List<Node> ();

		Node source = graph [selectedUnit.GetComponent<Unit> ().tileX, selectedUnit.GetComponent<Unit> ().tileY];
		Node target = graph [x, y];
		
		dist [source] = 0;
		prev [source] = null;

		foreach (Node v in graph) {
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
				float alt = dist [u] + u.DistanceTo (v);
				if (alt < dist [v]) {
					dist [v] = alt;
					prev [v] = u;
				}
			}
		}
		if (prev [target] == null) {
			return;
		}
		List<Node> currentPath = new List<Node> ();
		Node curr = target;

		while (curr != null) {
			currentPath.Add (curr);
			curr = prev [curr];
		}

		currentPath.Reverse ();
		selectedUnit.GetComponent<Unit> ().currentPath = currentPath;
	}

    void GenerateMapVisual()
    {
        for (int x = 0; x < MapSizeX; x++)
        {
            for (int y = 0; y < MapSizeY; y++)
            {
                TileSprite tt = TileSprites[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tilePrefab, new Vector3(x, y, 0), Quaternion.identity);

                if(tt.tileType == Tiles.Plains)
                {
                    clickHandler ch = go.GetComponent<clickHandler>();
                    ch.tileX = x;
                    ch.tileY = y;
                    ch.map = this;
                }
            }
        }
    }

    public void Start() {
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;

        GenerateMapData();
        //_map = new TileSprite[MapSizeX, MapSizeY];
		//DefaultTiles ();
		//SetTiles ();
		//AddTilesToMap ();
        GeneratePathfindingGraph();
        GenerateMapVisual();
    }

}
